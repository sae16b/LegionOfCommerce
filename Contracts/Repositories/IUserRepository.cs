using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;

namespace Contracts.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
		void SayHiToUser(User user);
    }
}
