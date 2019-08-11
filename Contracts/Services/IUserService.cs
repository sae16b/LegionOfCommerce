using Entities.Models;
using System.Threading.Tasks;

namespace Contracts.Services
{
	public interface IUserService
	{
		Task<User> AuthenticateAsync(UserLoginModel userLoginModel);
		Task<AuthResult> RefreshUserTokenAsync(string token, string refreshToken);
		AuthResult GenerateAuthResultForUser(User user);
	}
}
