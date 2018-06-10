using BetSquad.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BetSquad.Infrastructure.Repository
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {

            //var poland = new Team(Group.A, "Poland");
            //var russia = new Team(Group.A, "Russia");
            //context.TeamsSet.Add(poland);
            //context.TeamsSet.Add(russia);
            //context.GamesSet.Add(new Game(DateTime.Parse("2018-05-11"), poland, russia, GameType.Group, "Testowy"));
        }
    }
}
