using AutoMapper;
using Microsoft.Extensions.Logging;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.Interface
{
    public interface IClubEvent
    {
        Task<TeamDashboardModel> GetTeamSummary(int? seasonId,ILogger logger, IMapper mapper,  PIBNAContext context);
        Task<List<ClubModel>> GetList(ILogger logger, IMapper mapper, PIBNAContext context);
        Task<ClubPageModel> GetListByPage(int pageSize, int page, ILogger logger, IMapper mapper, PIBNAContext context);
        Task<ClubModel> GetById(int id, ILogger logger, IMapper mapper, PIBNAContext context);
        Task PostClub(ClubModel data, ILogger logger, IMapper mapper, PIBNAContext context);
        Task PostOfficialEnd(string id, ILogger logger, IMapper mapper, PIBNAContext context);
        Task DeleteOfficial(int id, ILogger logger, IMapper mapper, PIBNAContext context);
    }
}
