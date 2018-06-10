using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BetSquad.Core.Domain;
using BetSquad.Core.Repository;
using BetSquad.Infrastructure.DTO;
using BetSquad.Infrastructure.Extension;

namespace BetSquad.Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Bet> _betRepository;
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<Result> _resultRepository;
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IMapper _mapper;

        public AdminService(IRepository<Bet> betRepository, IRepository<Game> gameRepository, IRepository<Result> resultRepository, 
            IRepository<Team> teamRepository, IRepository<ApplicationUser> userRepository, IMapper mapper)
        {
            _betRepository = betRepository;
            _gameRepository = gameRepository;
            _resultRepository = resultRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task AddGame(DateTime gameStartTime, Guid team1Id, Guid team2Id, GameType gameType, string gameDescription)
        {
            if(team1Id == team2Id)
            {
                throw new Exception("Vs teams must be diffrent");
            }
            var team1 = await _teamRepository.GetOrThrow(team1Id);
            var team2 = await _teamRepository.GetOrThrow(team2Id);
            await _gameRepository.Insert(new Game(gameStartTime, team1, team2, gameType, gameDescription));
            await _gameRepository.SaveChanges();
        }

        public async Task RemoveGame(Guid gameId)
        {
            var game = await _gameRepository.GetOrThrow(gameId);
            await _gameRepository.Remove(game);
            await _gameRepository.SaveChanges();
        }

        public async Task UpdateResult(Guid gameId, int score1, int score2)
        {
            var game = await _gameRepository.Get(gameId);
            game.SetResult(score1, score2);
           
            await _gameRepository.Update(game);
            var bets = (await _betRepository.GetAll()).Where(x => x.Game == game).ToList();
            var users = await _userRepository.GetAll(); 
            foreach(var user in users)
            {
                foreach(var bet in bets)
                {
                    if(user.Bets.Any(x => x.Id == bet.Id) || user.FinihedBets.Any(x => x.Bet.Id == bet.Id))
                    {
                        user.GameResultChangedEventHandler(bet);
                        await _userRepository.Update(user);
                    }
                }
            }
            
            
        }

    }
}
