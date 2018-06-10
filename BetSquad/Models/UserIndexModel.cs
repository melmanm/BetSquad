using BetSquad.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetSquad.Models
{
    public  class UserIndexModel : ModelResult
    {
        public ICollection<BetDTO> Bets { get; set; }
        public ICollection<GameDTO> Games { get; set; }
    }
}
