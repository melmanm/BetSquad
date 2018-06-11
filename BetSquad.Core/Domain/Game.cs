using System;
using System.Collections.Generic;
using System.Text;

namespace BetSquad.Core.Domain
{
    public enum GameType
    {
        Group, Quater, Semi, Final, SmallFinal,Finals_1_8
    }
    
    public class Game : IBaseEntity
    {
        public DateTime StartTime { get; set; }
        public virtual Team Team1 { get; set; }
        public virtual Team Team2 { get; set; }
        public GameType GameType { get; set; }
        public string Description { get; set; }
        public virtual Result Result { get; set; }
        public bool HasResult
        {
            get => Result != null ? true : false; 
        }

        public bool HasBegan
        {
            get => StartTime < DateTime.Now ? true : false;
        }
        public Guid Id { get; set; }

        public Game(DateTime startTime, Team team1, Team team2, GameType gameType, string gameDescription = "")
        {
            SetStartTime(startTime);
            Team1 = team1;
            Team2 = team2;
            GameType = gameType;
            Description = "";
        }
        public Game()
        {
        }

        private void SetStartTime(DateTime startTime)
        {
            if (startTime < DateTime.Now)
                throw new Exception("Start time must be greater than current time.");
            StartTime = startTime;
        }

        public void SetResult(int score1, int score2)
        {
            if (this.Result == null)
                Result = new Result(score1, score2);
            else
                Result.SetResult(score1, score2);
        }
    }
}
