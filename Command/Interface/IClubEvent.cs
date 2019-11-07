using AutoMapper;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.Interface
{
    public interface IClubEvent
    {
        Task<TeamDashboardModel> GetTeamSummary(int? seasonId, IMapper mapper, PIBNAContext context);
        Task<List<ClubModel>> GetList(IMapper mapper, PIBNAContext context);
        Task<ClubPageModel> GetListByPage(int pageSize, int page, IMapper mapper, PIBNAContext context);
        Task<ClubModel> GetById(int id, IMapper mapper, PIBNAContext context);
        Task PostClub(ClubModel data, IMapper mapper, PIBNAContext context);
        Task PostOfficialEnd(string id, IMapper mapper, PIBNAContext context);
        Task DeleteOfficial(int id, IMapper mapper, PIBNAContext context);
    }
}
