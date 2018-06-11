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
    public class CommonService : ICommonService
    {
        private readonly IRepository<Bet> _betRepository;
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CommonService(IRepository<Bet> betRepository, IRepository<Game> gameRepository, 
            IRepository<Team> teamRepository, 
            IRepository<ApplicationUser> userRepository, IUserService userService, IMapper mapper)
        {
            _betRepository = betRepository;
            _gameRepository = gameRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<GameDTO> GetGame(Guid gameId)
        {
            var game = await _gameRepository.GetOrThrow(gameId);
            return _mapper.Map<GameDTO>(game);

        }

        public async Task<ICollection<GameDTO>> GetGames()
        {
            var games = await _gameRepository.GetAll();
            return _mapper.Map<ICollection<GameDTO>>(games.OrderBy(x=>x.StartTime).ToList());
        }

        public async Task<ICollection<TeamDTO>> GetTeams()
        {
            var teams = await _teamRepository.GetAll();
            return _mapper.Map<ICollection<TeamDTO>>(teams);
        }

        public async Task<ICollection<UserDTO>> GetUsersDTO()
        {
             var users = (await _userRepository.GetAllFlat()).OrderByDescending(x=>x.Score);
             return _mapper.Map<ICollection<UserDTO>>(users);
        }
        public async Task<ICollection<FinishedBetDTO>> GetFinishedBetDTO(Guid userId)
        {
            var user = await _userRepository.Get(userId);

            return _mapper.Map<ICollection<FinishedBet>, ICollection<FinishedBetDTO>>(user.FinihedBets.OrderBy(x=>x.Bet.ExipiryDate).ToList());
        }

        public async Task<UserDTO> GetUserDTO(Guid id)
        {
            var users = await _userRepository.GetOrThrow(id);
            return _mapper.Map<UserDTO>(users);
        }
    }
}
