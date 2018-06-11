using AutoMapper;
using BetSquad.Core.Domain;
using BetSquad.Core.Repository;
using BetSquad.Infrastructure.DTO;
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
        private readonly IMapper _mapper;

        public DbSet<T> GetEntities()
        {
            return entities;
        }

        public Repository(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;

            entities = context.Set<T>();
            _mapper = mapper;
        }
        public async Task<ICollection<T>> GetAll()
        {
            var incluses = GetIncludes();
            return GetIncludes();
        }
        public async Task<ICollection<T>> GetAllFlat()
        {
           return await entities.ToListAsync();
        }


        public async Task<T> Get(Guid id)
        {
            return  GetIncludes().Single(s => s.Id == id);
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

        public async Task UpdateUsers(Game game)
        {

            //foreach (var user in  (entities as DbSet<ApplicationUser>)
            //    .Include(x => x.Bets)
            //    .Include(x => x.Bets).ThenInclude(x=>x.Result)
            //    .Include(x => x.Bets).ThenInclude(x => x.Game).ThenInclude(x => x.Result)
            //    .Include(x => x.Bets).ThenInclude(x => x.Game).ThenInclude(x => x.Team1)
            //    .Include(x => x.Bets).ThenInclude(x => x.Game).ThenInclude(x => x.Team2)
            //    .Include(x => x.FinihedBets)
            //    .Include(x => x.FinihedBets).ThenInclude(x => x.Bet).ThenInclude(x => x.Result)
            //    .Include(x => x.FinihedBets).ThenInclude(x => x.Bet).ThenInclude(x => x.Game).ThenInclude(x => x.Result)
            //    .Include(x => x.FinihedBets).ThenInclude(x => x.Bet).ThenInclude(x => x.Game).ThenInclude(x => x.Team1)
            //    .Include(x => x.FinihedBets).ThenInclude(x => x.Bet).ThenInclude(x => x.Game).ThenInclude(x => x.Team2))
            //{
            //    user.GameResultChangedEventHandler(game.Id);
            //    user.Score = user.FinihedBets.Sum(x=>x.AchivedScore);
            //}

            //await context.SaveChangesAsync();
        }

        private List<T> GetIncludes()
        {
            if (typeof(T) == typeof(Game))
            {
                return (entities as DbSet<Game>)
                    .Include(x => x.Team1)
                    .Include(x => x.Team2)
                    .Include(x => x.Result).ToList() as List<T>;

            }
            if (typeof(T) == typeof(FinishedBet))
            {
                return (entities as DbSet<FinishedBet>)
                .Include(x => x.Bet)
                .Include(x => x.Bet).ThenInclude(x => x.Result)
                .Include(x => x.Bet).ThenInclude(x => x.Game).ThenInclude(x => x.Result)
                .Include(x => x.Bet).ThenInclude(x => x.Game).ThenInclude(x => x.Team1)
                .Include(x => x.Bet).ThenInclude(x => x.Game).ThenInclude(x => x.Team2)
                .ToList()
                as List<T>;
            }
            if (typeof(T) == typeof(Bet))
            {
                return (entities as DbSet<Bet>)
                .Include(x => x.Result)
                .Include(x => x.Game).ThenInclude(x => x.Result)
                .Include(x => x.Game).ThenInclude(x => x.Team1)
                .Include(x => x.Game).ThenInclude(x => x.Team2)
                .ToList()
                as List<T>;
            }
            if (typeof(T) == typeof(Bet))
            {
                return (entities as DbSet<Bet>)
                .Include(x => x.Result)
                .Include(x => x.Game).ThenInclude(x => x.Result)
                .Include(x => x.Game).ThenInclude(x => x.Team1)
                .Include(x => x.Game).ThenInclude(x => x.Team2)
                .ToList()
                as List<T>;
            }
            if (typeof(T) == typeof(ApplicationUser))
            {
                var user = (entities as DbSet<ApplicationUser>)
                .Include(x => x.Bets)
                .ThenInclude(x => x.Result)
                .Include(x => x.Bets).ThenInclude(x => x.Game).ThenInclude(x => x.Result)
                .Include(x => x.Bets).ThenInclude(x => x.Game).ThenInclude(x => x.Team1)
                .Include(x => x.Bets).ThenInclude(x => x.Game).ThenInclude(x => x.Team2)
                .Include(x => x.FinihedBets).ThenInclude(x => x.Bet).ThenInclude(x => x.Result)
                .Include(x => x.FinihedBets).ThenInclude(x => x.Bet).ThenInclude(x => x.Game).ThenInclude(x => x.Result)
                .Include(x => x.FinihedBets).ThenInclude(x => x.Bet).ThenInclude(x => x.Game).ThenInclude(x => x.Team1)
                .Include(x => x.FinihedBets).ThenInclude(x => x.Bet).ThenInclude(x => x.Game).ThenInclude(x => x.Team2)
                .ToList()
                 as List<T>;
                return user;
            }
            else return entities.ToList();
        }
    }
}
