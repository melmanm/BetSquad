using System;
using System.Collections.Generic;
using System.Text;

namespace BetSquad.Core.Domain
{
    public class FinishedBet : IBaseEntity
    {
        public virtual Bet Bet { get; set; }
        public int AchivedScore { get; set; }
        public Guid Id { get; set; }

        public FinishedBet(Bet bet, int achivedScore)
        {
            Bet = bet;
            AchivedScore = achivedScore;
        }

        public FinishedBet()
        { }
    }
}
