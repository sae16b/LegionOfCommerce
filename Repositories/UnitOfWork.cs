using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using Contracts.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly LegionContext Context;
		public UnitOfWork(LegionContext context)
		{
			Context = context;
			User = new UserRepository(context);
		}

		public IUserRepository User { get; private set; }

		public int Complete()
		{
			return Context.SaveChanges();
		}

		public void Dispose()
		{
			Context.Dispose();
		}

	}
}
