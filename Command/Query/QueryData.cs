using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PIBNAAPI.Model;

namespace PIBNAAPI.Command.Query
{
    public class QueryData
    {
        public static IEnumerable<PTeam> getTeamList(int clubid, int seasonId)
        {
            
            using (var ctx = new PIBNAContext())
            {
                return ctx.PTeam
                    .Include(s => s.TeamStatus)
                    .Include(s => s.Club)
                    .Include(s => s.Division)
                    .Include(s => s.User)
                    .Where(s => s.ClubId == clubid && s.EndDate == null && s.SeasonId == seasonId)
                    .Select(s => s)
                    .AsQueryable();

                
            }
            
        }
    }
}
