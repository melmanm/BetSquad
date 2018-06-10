using System;
using System.Collections.Generic;
using System.Text;

namespace BetSquad.Core.Domain
{
    public class Result : IBaseEntity
    {
    
        public int Score1 { get; set; }
        public int Score2 { get; set; }
        public Guid Id { get; set; }

        public Result(int score1, int score2)
        {
            SetResult(score1, score2);
        }
        public Result()
        { }

        public void SetResult(int score1, int score2)
        {
            if (score1 < 0 || score2 < 0)
                throw new Exception("Scpre must be greater than 0");
            Score1 = score1;
            Score2 = score2;
        }

    }
}
