using BetSquad.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BetSquad.Models
{
    public class UserEditBetModel : ModelResult
    {
        public GameDTO GameDTO { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value equal or bigger than 0")]
        public int score1 { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value equal or bigger than 0")]
        public int score2 { get; set; }
        public Guid BetId { get; set; }
    } 
}
