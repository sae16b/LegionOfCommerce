using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LegionOfCommerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private UserManager<User> _userManager;
		private SignInManager<User> _signInManager;
		public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpPost]
		[Route("Register")]
		public async Task<Object> PostUser(UserRegistrationModel model)
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
