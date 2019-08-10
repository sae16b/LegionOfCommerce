using Entities.Models;
using System.Threading.Tasks;

namespace Contracts.Services
{
	public interface IUserService
	{
		Task<User> Authenticate(UserLoginModel userLoginModel);
		string CreateUserToken(User user);
	}
}
