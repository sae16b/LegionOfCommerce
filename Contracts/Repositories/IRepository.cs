﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Contracts.Repositories
{
    public interface IRepository<T> where T: class
    {
		T Get(int id);
		IEnumerable<T> GetAll();
		IEnumerable<T> Find(Expression<Func<T, bool>> query);

		void Add(T entity);
		void AddRange(IEnumerable<T> entities);

		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
    }
}