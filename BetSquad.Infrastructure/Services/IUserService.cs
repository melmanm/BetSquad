using BetSquad.Core.Domain;
using BetSquad.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BetSquad.Infrastructure.Services
{
    public interface IUserService
    {
        Task<ICollection<BetDTO>> GetBetsDTO(Guid userId);
        Task<ICollection<BetDTO>> GetWaitingBetsDTO(Guid userId);
        Task<ICollection<FinishedBetDTO>> GetFinishedBetDTO(Guid userId);
        Task<BetDTO> GetBetDTO(Guid userId, Guid betId);
        Task<ICollection<GameDTO>> GetAvlblGamesDTO(Guid userId);
        Task EditBet(Guid userId, Guid betId, int Score1, int Score2);
        Task CreateBet(Guid userId, Guid gameId, int score1, int score2);
        Task RemoveBet(Guid userId, Guid betId);
        Task UpdateResult(Guid gameId, int score1, int score2);
    }
}
