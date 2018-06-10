using System;
using System.Collections.Generic;
using System.Text;

namespace BetSquad.Infrastructure.DTO
{
    public class BetDTO
    {
        public DateTime ExipiryDate { get; set; }
        public bool IsActive { get; set; }
        public ResultDTO Result { get; set; }
        public GameDTO Game { get; set; }
        public Guid Id { get; set; }
        public bool GameHasResult { get; set; }
    }
}
