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
        public int Score {
            get; set;
        }
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

        public void GameResultChangedEventHandler(Guid gameid)
        {
            var fbet = FinihedBets.FirstOrDefault(x => x.Bet.Game.Id == gameid);
            if (fbet != null)
            {
                UpdateScore(fbet.Bet, ScoreProvider.GetScore(fbet.Bet.Game.Result, fbet.Bet.Result));
            }
            var bet = Bets.FirstOrDefault(x => x.Game.Id == gameid);
            if (bet != null)
            {
                UpdateScore(bet, ScoreProvider.GetScore(bet.Game.Result, bet.Result));
            }
           
     
        }



        private void UpdateScore(Bet bet, int scorePlus)
        {
            var betslikethis = FinihedBets.Where(x => x.Bet.Game.Id == bet.Game.Id).ToList();
            if (betslikethis.Count() > 1)
            {
                FinihedBets.Remove(betslikethis[1]);
                Score -= betslikethis[1].AchivedScore;
            }
            if (FinihedBets.Any(x=>x.Bet.Id == bet.Id))
            {
                var finihed = FinihedBets.Where(x => x.Bet.Id == bet.Id).ToList();
                foreach (var finish in finihed)
                {
                    Score -= finish.AchivedScore;
                    FinihedBets.Remove(finish);
                }
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
