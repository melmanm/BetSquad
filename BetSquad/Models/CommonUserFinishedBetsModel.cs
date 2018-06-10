using BetSquad.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetSquad.Models
{
    public class CommonUserFinishedBetsModel : ModelResult
    {
        public ICollection<FinishedBetDTO> FinishedBets { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
    }
}
