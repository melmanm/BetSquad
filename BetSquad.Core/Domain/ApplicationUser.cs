using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetSquad.Core.Domain
{
    public class ApplicationUser : IdentityUser<Guid>, IBaseEntity
    {
        public string NickName { get; set; }
        public virtual IList<Bet> Bets { get; set; }
        public virtual IList<FinishedBet> FinihedBets { get; set; }
        public int Score { get; set; } = 0;
        public string Status { get; set; } = "Good luck!";

        public ApplicationUser()
        {
            Bets = new List<Bet>();
            FinihedBets = new List<FinishedBet>();
        }
        public void AddBet(Bet bet)
        {
            Bets.Add(bet);
        }

        public void GameResultChangedEventHandler(Bet bet)
        {
            if(Bets.Any(x => x.Id == bet.Id) || FinihedBets.Any(x => x.Bet.Id == bet.Id))
            UpdateScore(bet, ScoreProvider.GetScore(bet.Game.Result, bet.Result));
        }

        private void UpdateScore(Bet bet, int scorePlus)
        {
            if (FinihedBets.Any(x=>x.Bet.Id == bet.Id))
            {
                var finihed = FinihedBets.First(x => x.Bet.Id == bet.Id);
                Score -= finihed.AchivedScore;
                FinihedBets.Remove(finihed);
            }
            Score += scorePlus;
            FinihedBets.Add(new FinishedBet( bet, scorePlus));

            if (Bets.Contains(bet))
            {
                Bets.Remove(bet);
            }
        }

        public void RemoveBet(Bet bet)
        {
            if(FinihedBets.Any(x => x.Bet.Id == bet.Id))
            {
                throw new Exception("Can not remove completed bet");
            }
            if (Bets.Contains(bet))
            {
                Bets.Remove(bet);
            }
        }
    }

    
}
