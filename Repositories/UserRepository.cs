using Contracts.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
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

		public void SayHiToUser(User user)
		{
			Console.WriteLine("hi {0}", user.UserName);
		}
	}
}
