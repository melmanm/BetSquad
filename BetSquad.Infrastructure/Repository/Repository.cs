using BetSquad.Core.Domain;
using BetSquad.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BetSquad.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();

        }
        public async Task<ICollection<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> Get(Guid id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await entities.AddAsync(entity);
            context.SaveChanges();
        }

        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await Task.Run(()=>entities.Remove(entity));
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task<T> Get(Guid id, params Expression<Func<T, object>>[] paths)
        {
            var result = entities.Include(paths.First());
            foreach (var path in paths.Skip(1))
            {
                result = result.Include(path);
            }
            return await result.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<ICollection<T>> GetAll(params Expression<Func<T, object>>[] paths)
        {
            var result = entities.Include(paths.First());
            foreach (var path in paths.Skip(1))
            {
                result = result.Include(path);
            }
            return await result.ToListAsync();
        }
    }
}
