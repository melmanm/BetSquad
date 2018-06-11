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
    public class UserService : IUserService
    {
        private readonly IRepository<Bet> _betRepository;
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<Bet> betRepository, IRepository<Game> gameRepository, IRepository<ApplicationUser> userRepository, IMapper mapper)
        {
            _betRepository = betRepository;
            _gameRepository = gameRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserService(IRepository<Bet> betRepository, IRepository<ApplicationUser> userRepository, IMapper mapper)
        {
            _betRepository = betRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task CreateBet(Guid userId, Guid gameId, int score1, int score2)
        {
            var user = await _userRepository.GetOrThrow(userId);
            var game = await _gameRepository.GetOrThrow(gameId);
            if (user.Bets.Any(x=>x.Game.Id == gameId))
            {
                throw new Exception("Bet exists. You cant create it again.");
            }

            var bet = new Bet(game, score1, score2);
            user.AddBet(bet);

            //await _betRepository.Insert(bet);
            
            await _userRepository.SaveChanges();
            await _betRepository.SaveChanges();
        }

        public async Task RemoveBet(Guid userId, Guid betId)
        {
            var user = await _userRepository.GetOrThrow<ApplicationUser>(userId);
            user.RemoveBet(user.Bets.FirstOrDefault(x => x.Id == betId));
            await _userRepository.Update(user);
        }

        public async Task EditBet(Guid userId, Guid betId, int score1, int score2)
        {
            //var user = await _userRepository.Get(userId);
            var bet = await _betRepository.Get(betId);
            if (bet.GameHasResult)
            {
                throw new Exception("Administrator updated result for this game. You cant modify this bet.");
            }
            if (bet.Game.HasBegan)
            {
                throw new Exception("Game in progress. You can't modify bet.");
            }
            bet.SetResult(score1, score2);
            await _betRepository.Update(bet);
        }

        public async Task<ICollection<BetDTO>> GetBetsDTO(Guid userId)
        {
            var user = await _userRepository.Get(userId);

            return _mapper.Map<ICollection<Bet>, ICollection<BetDTO>>(user.Bets.Where(x=>!x.Game.HasBegan).OrderBy(x => x.ExipiryDate).ToList());
        }
        public async Task<ICollection<FinishedBetDTO>> GetFinishedBetDTO(Guid userId)
        {
            var user = await _userRepository.Get(userId);

            return _mapper.Map<ICollection<FinishedBet>, ICollection<FinishedBetDTO>>(user.FinihedBets.OrderByDescending(x => x.Bet.ExipiryDate).ToList());
        }


        public async Task<ICollection<GameDTO>> GetAvlblGamesDTO(Guid userId)
        {
            var user = await _userRepository.Get(userId);
            var games = await _gameRepository.GetAll();
            var betedGames = user.Bets.Select(x => x.Game);
            var toreturn = games.Where(x => betedGames?.Contains(x)==false && !x.HasBegan && !x.HasResult).OrderBy(x => x.StartTime).ToList();

            return _mapper.Map<ICollection<Game>, ICollection<GameDTO>>(toreturn as ICollection<Game>);
        }

        public async Task<BetDTO> GetBetDTO(Guid userId, Guid betId)
        {
            var user = await _userRepository.GetOrThrow(userId);
            var bet = await _betRepository.GetOrThrow(betId);
            return _mapper.Map<BetDTO>(bet);
        }

        public async Task UpdateResult(Guid gameId, int score1, int score2)
        {
            var games = await _gameRepository.GetAll();
            var game = games.First();
            game.SetResult(score1, score2);

            await _gameRepository.Update(game);

        }

        public  async Task<ICollection<BetDTO>> GetWaitingBetsDTO(Guid userId)
        {
            var user = await _userRepository.Get(userId);

            return _mapper.Map<ICollection<Bet>, ICollection<BetDTO>>(user.Bets.Where(x => x.Game.HasBegan).OrderBy(x => x.ExipiryDate).ToList());
        }
    }

}
