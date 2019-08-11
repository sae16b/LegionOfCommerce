using Contracts.Services;
using Entities.Models;
using Entities;
using Repositories;
using Contracts.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LegionOfCommerce.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Contracts;

namespace LegionOfCommerce.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> _userManager;

		private readonly LegionContext _context;
		private readonly IUnitOfWork _unitOfWork;

		private readonly AppSettings _appSettings;
		private readonly TokenValidationParameters _tokenValidationParameters;

		public UserService(LegionContext context, UserManager<User> userManager, IOptions<AppSettings> appSettings, TokenValidationParameters tokenValidationParameters)
		{
			_appSettings = appSettings.Value;
			_userManager = userManager;
			_context = context;
			_unitOfWork = new UnitOfWork(context);
			_tokenValidationParameters = tokenValidationParameters;
		}

		// Checks username and password
		async public Task<User> AuthenticateAsync(UserLoginModel userLoginModel)
		{
			User user = _unitOfWork.User.GetByUserNameOrEmail(userLoginModel.EmailOrUserName);
			bool isUser = await _userManager.CheckPasswordAsync(user, userLoginModel.Password);


			return isUser ? user : null;
		}

		public async Task<AuthResult> RefreshUserTokenAsync(string token, string refreshToken)
		{
			var validatedToken = GetPrincipalFromToken(token);

			if (validatedToken == null)
			{
				return new AuthResult { Errors = new[] { "Invalid token" } };
			}


			var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

			var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
					.AddSeconds(expiryDateUnix);

			if (expiryDateTimeUtc > DateTime.UtcNow)
			{
				return new AuthResult { Errors = new[] { "This token hasn't expired yet" } };
			}

			var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

			var storedRefreshToken = _unitOfWork.RefreshToken.Find(x => x.Token == refreshToken).Single();

			if (storedRefreshToken == null)
			{
				return new AuthResult { Errors = new[] { "This refresh token doesn't exist" } };
			}

			if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
			{
				return new AuthResult { Errors = new[] { "This refresh token has expired" } };
			}

			if (storedRefreshToken.Invalidated)
			{
				return new AuthResult { Errors = new[] { "This refresh token has been invalidated" } };
			}

			if (storedRefreshToken.Used)
			{
				return new AuthResult { Errors = new[] { "This refresh token has been used" } };
			}

			if (storedRefreshToken.JwtId != jti)
			{
				return new AuthResult { Errors = new[] { "This refresh token does not match jwt" } };
			}

			storedRefreshToken.Used = true;
			_unitOfWork.RefreshToken.Update(storedRefreshToken);
			_unitOfWork.Complete();

			var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "UserId").Value);
			return GenerateAuthResultForUser(user);
		}
		private ClaimsPrincipal GetPrincipalFromToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			try
			{
				_tokenValidationParameters.ValidateLifetime = false;
				var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
				_tokenValidationParameters.ValidateLifetime = true;
				if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
				{
					return null;
				}
				else
				{
					return principal;
				}
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
		{
			return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
				jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
				StringComparison.InvariantCultureIgnoreCase);
		}

		public AuthResult GenerateAuthResultForUser(User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = EncodingHelper.GetEncodedJWTKey(_appSettings);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new Claim("UserId", user.Id.ToString())
				}),
				Expires = DateTime.UtcNow.Add(_appSettings.TokenLifetime),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var securityToken = tokenHandler.CreateToken(tokenDescriptor);
			var token = tokenHandler.WriteToken(securityToken);

			var refreshToken = new RefreshToken
			{
				JwtId = securityToken.Id,
				UserId = user.Id,
				CreationDate = DateTime.UtcNow,
				ExpiryDate = DateTime.UtcNow.AddMonths(6)
			};

			_unitOfWork.RefreshToken.Add(refreshToken);
			_unitOfWork.Complete();

			return new AuthResult
			{
				Success = true,
				Token = token,
				RefreshToken = refreshToken.Token
			};
		}
	}
}
