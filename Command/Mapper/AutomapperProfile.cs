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
            CreateMap<PClub, ClubModel>();
            CreateMap<PDivision, PDivisionModel>();

            CreateMap<PDivisionModel, PDivision>()
                 .ForMember(dest => dest.DivisionId, opt => opt.Ignore())
                .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => DateTime.Now));

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
            CreateMap<PUser, UserInfoModel>()
                .ForMember(dest => dest.ClubName, opt => opt.MapFrom(src => src.Club.ClubName))
                .ForMember(dest => dest.ClubId, opt => opt.MapFrom(src => src.Club.ClubId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + ' ' + src.LastName));

        }
    }
}
