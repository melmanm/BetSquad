using System;
using System.Collections.Generic;
using System.Text;

namespace BetSquad.Core.Domain
{
    public static class ScoreProvider
    {
        public static int GetScore(Result real, Result bet)
        {
            if ((real.Score1 > real.Score2 && bet.Score1 > bet.Score2)
                || (real.Score1 < real.Score2 && bet.Score1 < bet.Score2)
                )
            {
                var result = 10 - Math.Abs(real.Score1-bet.Score1) - Math.Abs(real.Score2-bet.Score2);
                if (result < 0) return 0;
                else return result;
            }
            else if (real.Score1 == real.Score2 && bet.Score1 == bet.Score2)
            {
                if (real.Score1 == bet.Score1) return 10;
                var result = 10 - (Math.Abs(real.Score1 - bet.Score1)*2);
                if (result < 0) return 0;
                else return result;
            }
            else
            {
                var result = 5 - Math.Abs(real.Score1 - bet.Score1) - Math.Abs(real.Score2 - bet.Score2);
                if (result < 0) return 0;
                else return result;
            }
            return 0;
        }
    }
}
