using Contracts.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Extensions;
using System.Linq.Expressions;
using System.Text;

namespace Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(LegionContext context)
			: base(context)
		{
		}

		public User GetByUserNameOrEmail(string userNameOrEmail)
		{
			if (userNameOrEmail.IsEmail())
			{
				return GetByEmail(userNameOrEmail);
			}
			else
			{
				return GetByUserName(userNameOrEmail);
			}
		}

		public User GetByUserName(string userName)
		{
			List<User> users = Find(user => user.UserName == userName).ToList();
			return users.FirstOrDefault();
		}

		public User GetByEmail(string email)
		{
			List<User> users = Find(user => user.Email == email).ToList();
			return users.FirstOrDefault();
		}
	}
}
