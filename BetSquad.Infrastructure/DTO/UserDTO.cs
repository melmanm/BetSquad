using System;
using System.Collections.Generic;
using System.Text;

namespace BetSquad.Infrastructure.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public int Score { get; set; }
        public ICollection<BetDTO> FinishedBets { get; set; }
    }
}
