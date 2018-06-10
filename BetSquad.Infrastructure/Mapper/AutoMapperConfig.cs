using AutoMapper;
using BetSquad.Core.Domain;
using BetSquad.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetSquad.Infrastructure.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<ApplicationUser, UserDTO>();
                c.CreateMap<Bet, BetDTO>();
                c.CreateMap<FinishedBet, FinishedBetDTO>();
                c.CreateMap<Team, TeamDTO>();
                c.CreateMap<Result, ResultDTO>();
                c.CreateMap<Game, GameDTO>()
                .ForMember(d => d.Team1, opt => opt.MapFrom(s => s.Team1))
                .ForMember(d => d.Team2, opt => opt.MapFrom(s => s.Team2))
                .ForMember(d => d.Result, opt => opt.MapFrom(s => s.Result));

            });
            return config.CreateMapper();
        }
    }
}
