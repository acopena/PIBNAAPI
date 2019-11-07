using AutoMapper;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;

namespace PIBNAAPI.Command.PIBNAAPI.Mapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<PDivision, DivisionModel>();

            CreateMap<PTeam, TeamModel>()
                .ForMember(dest => dest.TeamStatusDescription, opt => opt.MapFrom(src => src.TeamStatus.TeamStatusDescription))
                .ForMember(dest => dest.ClubName, opt => opt.MapFrom(src => src.Club.ClubName))
                .ForMember(dest => dest.DivisionName, opt => opt.MapFrom(src => src.Division.DivisionName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ReverseMap();


            CreateMap<PTeamRoster, TeamRosterModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Member.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Member.LastName))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Member.DateOfBirth))
                .ReverseMap();

            CreateMap<WebContentParamModel, PWebContent>()
             .ForMember(dest => dest.PublishStartDate, opt => opt.MapFrom(src => DateTime.Parse(src.PublishStartDate)))
             .ForMember(dest => dest.PublishedEndDate, opt => opt.MapFrom(src => DateTime.Parse(src.PublishEndDate)))
             .ReverseMap();

            CreateMap<PWebContent, WebContentModel>();
                



        }
    }
}
