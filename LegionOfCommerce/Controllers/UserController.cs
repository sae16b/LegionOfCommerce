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
using MySql.Data.MySqlClient;

namespace LegionOfCommerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private UserManager<User> _userManager;
		private SignInManager<User> _signInManager;
		private IUserService _userService;


		public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService, IOptions<AppSettings> appSettings)
		{

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
				return Unauthorized("Could not verify username/password");
			}

			var token = _userService.CreateUserToken(user);

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
		public async Task<IActionResult> RegisterUser(UserRegistrationModel model)
		{
			var user = new User()
			{
				Fname = model.FirstName,
				Lname = model.LastName,
				UserName = model.UserName,
				Email = model.Email
			};

			try
			{
				var result = await _userManager.CreateAsync(user, model.Password);
				if (!result.Succeeded)
				{
					List<IdentityError> resultErrors = result.Errors.ToList();
					var outputErrors = new List<string>();
					for (int i = 0; i < resultErrors.Count(); ++i)
					{
						outputErrors.Add(resultErrors[i].Description);
					}
					return BadRequest(outputErrors);
				}
				else
				{
					return Ok(new { msg = "User created!" });
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Could not create user" + ex);
			}
		}

	}
}
