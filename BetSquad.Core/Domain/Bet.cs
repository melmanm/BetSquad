using System;
using System.Collections.Generic;
using System.Text;

namespace BetSquad.Core.Domain
{
    public class Bet : IBaseEntity
    {
       
        public DateTime ExipiryDate { get; private set; }
        public bool IsActive
        {
            get => ExipiryDate > DateTime.Now ? true : false; 
        }
        public bool GameHasResult
        {
            get => Game == null ? false : Game.HasResult;
        }

        public virtual Result Result { get; set; }
        public virtual Game Game { get; set; }
        public virtual Guid Id { get; set; }

        public Bet(Game game, int score1, int score2)
        {
            Validate(game.StartTime);
            ExipiryDate = game.StartTime;
            Game = game;
            SetResult(score1, score2);
          
        }
        public Bet()
        { }
        

        private void Validate(DateTime startTime)
        {
            if (startTime < DateTime.Now)
                throw new Exception("Start time must be greater than current time.");
        }


        public void SetResult(int score1, int score2)
        {
            if (Result == null)
                Result = new Result(score1, score2);
            else
                Result.SetResult(score1, score2);
        }


        
    }
}
