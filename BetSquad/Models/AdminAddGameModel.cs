using BetSquad.Core.Domain;
using BetSquad.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BetSquad.Models
{
    public class AdminAddGameModel : ModelResult
    {
        public ICollection<TeamDTO> Teams { get; set; }
        [Required]
        public Guid Team1Id { get; set; }
        [Required]
        public Guid Team2Id { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime GameStartTime { get; set; }
        [Required]
        public GameType GameType { get; set; }

    }
}
