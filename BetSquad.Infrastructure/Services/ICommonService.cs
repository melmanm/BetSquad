using BetSquad.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BetSquad.Infrastructure.Services
{
    public interface ICommonService
    {
        Task<GameDTO> GetGame(Guid gameId);
        Task<ICollection<GameDTO>> GetGames();
        Task<ICollection<TeamDTO>> GetTeams();
        Task<ICollection<FinishedBetDTO>> GetFinishedBetDTO(Guid userId);
        Task<ICollection<UserDTO>> GetUsersDTO();
        Task<UserDTO> GetUserDTO(Guid id);
    }
}
