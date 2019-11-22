using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PIBNAAPI.Command.Interface;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.action
{
    public class ClubEvent : IClubEvent
    {
        public async Task<TeamDashboardModel> GetTeamSummary(int? seasonId, ILogger logger, IMapper mapper, IMediator mediator, PIBNAContext context)
        {
            TeamDashboardModel model = new TeamDashboardModel();
            TeamSummaryModel header = new TeamSummaryModel();
            TeamSummaryModel footer = new TeamSummaryModel();

            if (seasonId == null)
            {
                

                var CurrentSeason = await context.PSeason
                    .Where(s => s.IsActive == true)
                    .Select(s => s).FirstOrDefaultAsync();
                if (CurrentSeason != null)
                    seasonId = CurrentSeason.SeasonId;

                logger.LogWarning("Season is null will set to current season " + seasonId);

            }
            var club = await (from p in context.PClub
                              where p.EndDate == null
                              select p).OrderBy(s => s.ClubName).ToListAsync();

            header = new TeamSummaryModel();
            header.ClubId = 0;
            header.ClubName = " Club";
            header.DivisionInfo = new List<DivisionSummaryModel>();
            header.DivisionInfo = await (from p in context.PDivision
                                         where p.EndDate == null
                                         orderby p.DivisionShortName
                                         select new DivisionSummaryModel
                                         {
                                             DivisionId = p.DivisionId,
                                             DivisionName = p.DivisionName,
                                             TeamCount = 0
                                         }).ToListAsync();


            DivisionSummaryModel dsTotal = new DivisionSummaryModel();
            dsTotal.DivisionId = 0;
            dsTotal.DivisionName = "Total";
            dsTotal.TeamCount = 0;
            header.DivisionInfo.Add(dsTotal);

            model.Header = header;

            model.Data = new List<TeamSummaryModel>();
            var teams = await context.PTeam.Where(s => s.SeasonId == seasonId.Value && s.EndDate == null).ToListAsync();

            foreach (var r in club)
            {
                TeamSummaryModel t = new TeamSummaryModel();
                t.ClubId = r.ClubId;
                t.ClubName = r.ClubName;
                t.DivisionInfo = new List<DivisionSummaryModel>();
                int DivisionTotal = 0;

                var clubTeam = teams.Where(s => s.ClubId == r.ClubId).ToList();


                foreach (var d in header.DivisionInfo.Where(s => s.DivisionId > 0))
                {
                    DivisionSummaryModel ds = new DivisionSummaryModel();
                    ds.DivisionId = d.DivisionId;
                    ds.DivisionName = d.DivisionName;
                    ds.TeamCount = clubTeam.Where(s => s.DivisionId == d.DivisionId).Select(s => s).Count();

                    DivisionTotal += ds.TeamCount;
                    t.DivisionInfo.Add(ds);
                }

                dsTotal = new DivisionSummaryModel();
                dsTotal.DivisionId = 0;
                dsTotal.DivisionName = "Total";
                dsTotal.TeamCount = DivisionTotal;
                t.DivisionInfo.Add(dsTotal);

                if (DivisionTotal > 0)
                {
                    model.Data.Add(t);
                }
            }

            int SummaryDivisionTotal = 0;
            footer = new TeamSummaryModel();
            footer.ClubId = 0;
            footer.ClubName = " Club";
            footer.DivisionInfo = new List<DivisionSummaryModel>();
            footer.DivisionInfo = (from p in header.DivisionInfo.Where(s => s.DivisionId > 0)
                                   select new DivisionSummaryModel
                                   {
                                       DivisionId = p.DivisionId,
                                       DivisionName = p.DivisionName,
                                       TeamCount = teams.Where(s => s.DivisionId == p.DivisionId).Select(s => s).Count()
                                   }).ToList();
            SummaryDivisionTotal = footer.DivisionInfo.Select(s => s.TeamCount).Sum();
            dsTotal = new DivisionSummaryModel();
            dsTotal.DivisionId = 0;
            dsTotal.DivisionName = "Total";
            dsTotal.TeamCount = SummaryDivisionTotal;
            footer.DivisionInfo.Add(dsTotal);

            model.Footer = footer;
            return model;
        }

        public async Task<List<ClubModel>> GetList(ILogger logger, IMapper mapper, PIBNAContext context)
        {
            List<ClubModel> data = new List<ClubModel>();
            try
            {

                data = await context.PClub
                    .Include(s => s.PClubOfficial)
                    .Where(s => s.EndDate == null)
                    .Select(s => mapper.Map<ClubModel>(s)).ToListAsync();
                foreach (var i in data)
                {
                    i.WebSite = String.IsNullOrEmpty(i.WebSite) ? string.Empty : i.WebSite;

                    i.OfficialList = new List<ClubOfficialModel>();
                    var oList = await context.PClubOfficial.Where(s => s.ClubId == i.ClubId && s.EndDate == null).ToListAsync();
                    foreach (var o in oList)
                    {
                        ClubOfficialModel oModel = new ClubOfficialModel();
                        oModel.ClubOfficialId = o.ClubOfficialId;
                        oModel.Name = o.Name;
                        oModel.Email = o.Email;
                        oModel.Phone = o.Phone;
                        oModel.PositionId = o.PositionId;
                        i.OfficialList.Add(oModel);
                    }

                }

            }
            catch (Exception ex)
            {
                var error = ex;
                logger.LogError(ex.InnerException.ToString());
            }
            return data.OrderBy(s => s.ClubName).ToList();
        }

        public async Task<ClubPageModel> GetListByPage(int pageSize, int page, ILogger logger, IMapper mapper, PIBNAContext context)
        {
            var model = new ClubPageModel();

            var clubs = from p in context.PClub
                        where p.EndDate == null
                        select mapper.Map<ClubModel>(p);

            var data = await PaginatedList<ClubModel>.CreateAsync(clubs.AsNoTracking(), page, pageSize);

            model.RecordCount = data.TotalRecord;
            model.data = data;

            return model;

        }

        public async Task<ClubModel> GetById(int id, ILogger logger, IMapper mapper, PIBNAContext context)
        {
            ClubModel data = new ClubModel();
            var dta = await (from p in context.PClub
                             where p.EndDate == null && p.ClubId == id
                             select p).FirstOrDefaultAsync();

            if (dta != null)
            {
                mapper.Map(dta, data);
                data.OfficialList = new List<ClubOfficialModel>();
                var officials = await context.PClubOfficial.Where(s => s.ClubId == id && s.EndDate == null).Select(s => s).ToListAsync();
                foreach (var i in officials)
                {
                    ClubOfficialModel o = new ClubOfficialModel();
                    o.ClubOfficialId = i.ClubOfficialId;
                    o.Email = i.Email;
                    o.Name = i.Name;
                    o.Phone = i.Phone;
                    o.PositionId = i.PositionId;
                    // o.PositionName = await ctx.PPosition.Where(s => s.PositionId == i.PositionId).Select(s => s.PositionDescription).FirstOrDefaultAsync();
                    data.OfficialList.Add(o);
                }
            }
            else
            {
                logger.LogWarning("Club id not found " + id.ToString());
            }
            return data;


        }

        public async Task PostClub(ClubModel data, ILogger logger, IMapper mapper, PIBNAContext context)
        {


            var dta = await (from p in context.PClub
                             where p.EndDate == null && p.ClubId == data.ClubId
                             select p).FirstOrDefaultAsync();

            if (dta == null)
            {
                PClub p = new PClub();
                p.ClubName = data.ClubName;
                p.City = data.City;
                p.State = data.State;
                p.Country = data.Country;
                p.FromDate = DateTime.Now;
                p.UserId = 2;
                await context.PClub.AddAsync(p);
                await context.SaveChangesAsync();
                data.ClubId = p.ClubId;
            }
            else
            {
                dta.ClubName = data.ClubName;
                dta.City = data.City;
                dta.State = data.State;
                dta.Country = data.Country;
                await context.SaveChangesAsync();
            }

            var existingOfficer = await context.PClubOfficial.Where(s => s.ClubId == data.ClubId).Select(p => p).ToListAsync();

            int countOfficialList = existingOfficer.Count();
            int countNewOfficialList = data.OfficialList.Where(s => s.ClubOfficialId > 0).Count();

            // Remove Officers first
            foreach (var x in existingOfficer)
            {
                var e = data.OfficialList.Where(s => s.ClubOfficialId == x.ClubOfficialId).Select(s => s).FirstOrDefault();
                if (e == null)
                {
                    x.EndDate = DateTime.Now;
                }
                else
                {
                    x.Name = e.Name;
                    x.Phone = e.Phone;
                    x.Email = e.Email;
                    x.PositionId = e.PositionId;
                }
            }
            //Add the new list
            var newOfficialList = data.OfficialList.Where(s => s.ClubOfficialId <= 0).Select(s => s).ToList();
            foreach (var i in newOfficialList)
            {
                PClubOfficial c = new PClubOfficial();
                c.Name = i.Name;
                c.Email = i.Email;
                c.Phone = i.Phone;
                c.PositionId = 1;
                c.FromDate = DateTime.Now;
                c.ClubId = data.ClubId;
                await context.PClubOfficial.AddAsync(c);
            }
            await context.SaveChangesAsync();




        }

        public async Task PostOfficialEnd(string id, ILogger logger, IMapper mapper, PIBNAContext context)
        {
            ClubModel data = new ClubModel();
            var dta = await (from p in context.PClubOfficial
                             where p.ClubOfficialId == int.Parse(id)
                             select p).FirstOrDefaultAsync();

            if (data != null)
            {
                dta.EndDate = DateTime.Now;
            }
            else
            {
                logger.LogWarning("Official ID not found " + id);
            }
            await context.SaveChangesAsync();

        }

        public async Task DeleteOfficial(int id, ILogger logger, IMapper mapper, PIBNAContext context)
        {
            ClubModel data = new ClubModel();
            var dta = await (from p in context.PClubOfficial
                             where p.ClubOfficialId == id
                             select p).FirstOrDefaultAsync();

            if (data != null)
            {
                dta.EndDate = DateTime.Now;
                await context.SaveChangesAsync();
            }
         }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if(_context)
            }
        }
    }
}
