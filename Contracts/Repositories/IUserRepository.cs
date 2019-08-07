using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;

namespace Contracts.Repositories
{
	public interface IUserRepository : IRepository<User>
	{

		User GetByUserNameOrEmail(string userNameOrEmail);
		User GetByUserName(string userName);
		User GetByEmail(string email);
	}
}
