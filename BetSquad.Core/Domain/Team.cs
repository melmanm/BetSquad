using System;

namespace BetSquad.Core.Domain
{
    public enum Group
    {
        A, B, C, D, E, F, G, H
    }
    public class Team : IBaseEntity
    {
        public Group Group { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }

        public Team(Group group, string name)
        {
            Group = group;
            Name = name;
        }

        public Team()
        { }
    }
}