using BetSquad.Core.Domain;
using BetSquad.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BetSquad.Infrastructure.Extension
{
    public static class RepositoryExtension
    {
        public static async Task<T> GetOrThrow<T>(this IRepository<T> repository, Guid id)
        {
            var entity = await repository.Get(id);
            if (entity == null)
            {
                throw new Exception("Entity not found in database. ;(");
            }
            return entity;
        }


      
    }
}
