using BetSquad.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BetSquad.Infrastructure.DTO
{
    public class FinishedBetDTO
    {
        public virtual BetDTO Bet { get; set; }
        public int AchivedScore { get; set; }
    }
}