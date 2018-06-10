﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BetSquad.Core.Repository
{
    public interface IRepository<T>
    {
        Task<ICollection<T>> GetAll();
        Task<ICollection<T>> GetAll(params Expression<Func<T, object>>[] paths);
        Task<T> Get(Guid id);
        Task<T> Get(Guid id, params Expression<Func<T, object>>[] paths);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task Remove(T entity);
        Task SaveChanges();

    }
}
