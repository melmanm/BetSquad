using BetSquad.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BetSquad.Infrastructure.DTO
{
    public class TeamDTO
    {
        public Group Group { get; set; }
        [Display(Name = "Team Name")]
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}
