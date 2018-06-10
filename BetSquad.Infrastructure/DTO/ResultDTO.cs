using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BetSquad.Infrastructure.DTO
{
    public class ResultDTO
    {
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value equal or bigger than {0}")]
        [Display(Name = "Team 1 score")]
        public int Score1 { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value equal or bigger than {0}")]
        [Display(Name = "Team 2 score")]
        public int Score2 { get; set; }
        public Guid Id { get; set; }
        public override string ToString()
        {
            return $"{Score1} : {Score2}";
        }
        public string Result_str{ get => this.ToString(); }

    }
}
