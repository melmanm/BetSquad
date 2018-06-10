using BetSquad.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BetSquad.Infrastructure.Services
{
    public interface IAdminService
    {
        Task UpdateResult(Guid gameId, int score1, int score2);
        Task AddGame(DateTime gameStartTime, Guid team1Id, Guid team2Id, GameType gameType, string gameDescription);
        Task RemoveGame(Guid gameId);
    }
}
