using BetSquad.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetSquad.Infrastructure.DTO
{
    public class GameDTO
    {
        public DateTime StartTime { get; set; }
        public TeamDTO Team1 { get; set; }
        public TeamDTO Team2 { get; set; }
        public GameType GameType { get; set; }
        public string Description { get; set; }
        public ResultDTO Result { get; set; }
        public bool HasResult{ get; set; }
        public Guid Id { get; set; }

        public override string ToString()
        {
            var vs = $"{Team1.Name} vs {Team2.Name} ";
            if (GameType == GameType.Group)
                vs += $"group: {Team1.Group}";
            return vs;
        }
        public string GetGameDetails()
        {
            if(Team1!=null)
            return GameType == GameType.Group ? $"group: {Team1.Group}" : GameType.ToString();
            return "-";
        }
        public string Details { get => this.GetGameDetails(); }
        public string Result_str
        {
            get => HasResult ? Result.Result_str : " - ";
        }
    }
}
