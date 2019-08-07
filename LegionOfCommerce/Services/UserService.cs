using Contracts.Services;
using Entities.Models;
using Entities;
using Repositories;
using Contracts.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LegionOfCommerce.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> _userManager;

		private readonly LegionContext _context;
		private readonly IUserRepository _userRepo;

		public UserService(LegionContext context, UserManager<User> userManager)
		{
			_userManager = userManager;
			_context = context;
			_userRepo = new UserRepository(context);
		}

		// Checks username and password and sends back JWT token
		async public Task<User> Authenticate(UserLoginModel userLoginModel)
		{
			User user = _userRepo.GetByUserNameOrEmail(userLoginModel.EmailOrUserName);
			bool isUser = await _userManager.CheckPasswordAsync(user, userLoginModel.Password);


			return isUser ? user : null;
		}
	}
}
