﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WebApiAM.Repository
{
    public interface IRepository<T>
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

        T Get(int id);
        T Get(Expression<Func<T, bool>> predicate);

        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate);
    }
}
