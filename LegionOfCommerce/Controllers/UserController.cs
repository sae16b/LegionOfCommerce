using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Services;
using Entities;
using Entities.Models;
using LegionOfCommerce.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LegionOfCommerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private UserManager<User> _userManager;
		private SignInManager<User> _signInManager;
		private IUserService _userService;
		private readonly AppSettings _appSettings;

		public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService, IOptions<AppSettings> appSettings)
		{
			_appSettings = appSettings.Value;
			_userManager = userManager;
			_signInManager = signInManager;
			_userService = userService;
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> LoginUser(UserLoginModel model)
		{
			User user = await _userService.Authenticate(model);
			if (user == null)
			{
				return BadRequest("Could not verify username/password");
			}

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

			return Ok(new { token });
		}

		[HttpGet]
		[Route("Info")]
		[Authorize]
		public async Task<Object> GetUserInfo()
		{
			string userId = User.Claims.First(c => c.Type == "UserId").Value;
			var user = await _userManager.FindByIdAsync(userId);

			return new { user };
		}

		[HttpPost]
		[Route("Register")]
		public async Task<Object> RegisterUser(UserRegistrationModel model)
		{
			var user = new User()
			{
				UserName = model.UserName,
				Email = model.Email
			};

			try
			{
				var result = await _userManager.CreateAsync(user, model.Password);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}
