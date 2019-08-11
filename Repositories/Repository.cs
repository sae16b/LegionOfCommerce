using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Contracts.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly IdentityDbContext Context;
		protected readonly DbSet<T> Set;

		public Repository(IdentityDbContext context)
		{
			Context = context;
			Set = Context.Set<T>();
		}

		public void Add(T entity)
		{
			Set.Add(entity);
		}

		public void AddRange(IEnumerable<T> entities)
		{
			Set.AddRange(entities);
		}

		public IEnumerable<T> Find(Expression<Func<T, bool>> query)
		{
			return Set.Where(query);
		}

		public T Get(int id)
		{
			return Set.Find(id);
		}

		public IEnumerable<T> GetAll()
		{
			return Set.ToList();
		}

		public void Remove(T entity)
		{
			Set.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			Set.RemoveRange(entities);
		}

		public void Update(T entity)
		{
			Set.Update(entity);
		}
	}
}
