using AutoMapper;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.Interface
{
    public interface IPTeamEvent
    {
        Task<List<TeamModel>> GetTeamListByClub(int clubid, int seasonId, IMapper _mapper, PIBNAContext context);
        Task<PaginatedList<TeamsModel>> GetTeamList(int clubid, int seasonId, int page, int pageSize, PIBNAContext context);
        Task<TeamModel> GetTeamByTeamId(int TeamId, IMapper _mapper, PIBNAContext context);
        Task<List<TeamModel>> GetTeamByStatus(IMapper _mapper, int seasonId, int clubId, int divisionId, int teamStatusId, PIBNAContext context);
        Task<List<TeamModel>> GetTeamByDivision(IMapper _mapper, int seasonId, int divisionId, int teamStatusId, PIBNAContext context);


        Task<OpenRosterInfoPageModel> GetOpenPlayers(string searchKey, string sortKey,
           int clubid, int divisionid, int teamid, int pageSize, int page, PIBNAContext context);

        Task<List<AvailableRosterModel>> GetAvailablePlayer(int clubid, int divisionid, int teamid, PIBNAContext context);
        Task<List<PTeamStatus>> GetTeamStatusList(PIBNAContext context);


        Task<int> SaveTeam(TeamModel model, PIBNAContext context);

        Task SaveTeamStatus(TeamHostStatus value, PIBNAContext context);

    }
}
