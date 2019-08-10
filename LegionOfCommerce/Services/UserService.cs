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

namespace LegionOfCommerce.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> _userManager;

		private readonly LegionContext _context;
		private readonly IUserRepository _userRepo;

		private readonly AppSettings _appSettings;

		public UserService(LegionContext context, UserManager<User> userManager, IOptions<AppSettings> appSettings)
		{
			_appSettings = appSettings.Value;
			_userManager = userManager;
			_context = context;
			_userRepo = new UserRepository(context);
		}

		// Checks username and password
		async public Task<User> Authenticate(UserLoginModel userLoginModel)
		{
			User user = _userRepo.GetByUserNameOrEmail(userLoginModel.EmailOrUserName);
			bool isUser = await _userManager.CheckPasswordAsync(user, userLoginModel.Password);


			return isUser ? user : null;
		}

		// Returns user token
		public string CreateUserToken(User user)
		{
			var key = EncodingHelper.GetEncodedJWTKey(_appSettings);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim("UserId", user.Id.ToString())
				}),
				Expires = DateTime.UtcNow.AddDays(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var securityToken = tokenHandler.CreateToken(tokenDescriptor);
			var token = tokenHandler.WriteToken(securityToken);

			return token;
		}
	}
}
