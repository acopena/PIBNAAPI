using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PIBNAAPI.Command.Interface;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.action
{
    public class PTeamEvent : IPTeamEvent
    {
        public async Task<List<TeamModel>> GetTeamListByClub(int clubid, int seasonId, IMapper _mapper, PIBNAContext context)
        {
            List<TeamModel> teamList = new List<TeamModel>();
            var team = await context.PTeam
                .Include(s => s.TeamStatus)
                .Include(s => s.Club)
                .Include(s => s.Division)
                .Include(s => s.User)
                .Where(s => s.ClubId == clubid && s.EndDate == null && s.SeasonId == seasonId)
                .Select(s => s).ToListAsync();
            return await GetTeamData(team, _mapper, context);
        }

        public async Task<PaginatedList<TeamsModel>> GetTeamList(int clubid, int seasonId, int page, int pageSize, PIBNAContext context)
        {
            var teams = context.PTeam
                 .Include(s => s.TeamStatus)
                 .Include(s => s.Club)
                 .Include(s => s.Division)
                 .Include(s => s.PTeamRoster)
                 .Include(s => s.PTeamOfficial)
                 .Include(s => s.User)
                 .Where(s => s.ClubId == clubid && s.EndDate == null && s.SeasonId == seasonId && s.EndDate == null)
                 .Select(s => new TeamsModel()
                 {
                     TeamId = s.TeamId,
                     TeamName = s.TeamName,
                     DivisionId = s.DivisionId,
                     DivisionName = s.Division.DivisionName,
                     SeasonId = s.SeasonId,
                     UserId = s.UserId,
                     UserName = s.User.UserName,
                     ClubId = s.ClubId,
                     ClubName = s.Club.ClubName,
                     TeamStatusId = s.TeamStatusId,
                     TeamStatusDescription = s.TeamStatus.TeamStatusDescription,
                     ApprovedBy = s.ApprovedBy,
                     ApprovedDate = s.ApprovedDate,
                     SubmitForApprovalDate = s.SubmitForApprovalDate,
                     RosterCount = s.PTeamRoster.Where(x => x.EndDate == null).Count(),
                     CoachName = s.PTeamOfficial.Where(x => x.EndDate == null).FirstOrDefault().Name
                 });
            return await PaginatedList<TeamsModel>.CreateAsync(teams.AsNoTracking(), page, pageSize);
        }



        public async Task<TeamModel> GetTeamByTeamId(int TeamId, IMapper _mapper, PIBNAContext context)
        {
            TeamModel model = new TeamModel();
            var team = await context.PTeam
                .Include(s => s.TeamStatus)
                .Include(s => s.Club)
                .Include(s => s.Division)
                .Include(s => s.User)
                .Where(s => s.TeamId == TeamId && s.EndDate == null)
                .Select(s => s).FirstOrDefaultAsync();

            List<PTeam> teamList = new List<PTeam>();
            teamList.Add(team);
            var data = await GetTeamData(teamList, _mapper, context);
            return data[0];
        }

        public async Task<List<TeamModel>> GetTeamByStatus(IMapper _mapper, int seasonId, int clubId, int divisionId, int teamStatusId, PIBNAContext context)
        {
            List<TeamModel> teamList = new List<TeamModel>();
            var team = context.PTeam
                 .Include(s => s.TeamStatus)
                 .Include(s => s.Club)
                 .Include(s => s.Division)
                 .Include(s => s.User)
                 .Where(s => s.EndDate == null && s.SeasonId == seasonId)
                 .Select(s => s);

            if (clubId > 0)
                team = team.Where(s => s.ClubId == clubId);
            if (divisionId > 0)
                team = team.Where(s => s.DivisionId == divisionId);
            if (teamStatusId > 0)
                team = team.Where(s => s.TeamStatusId == teamStatusId);
            var teams = await team.ToListAsync();
            return await GetTeamData(teams, _mapper, context);
        }


        public async Task<List<TeamModel>> GetTeamByDivision(IMapper _mapper, int seasonId, int divisionId, int teamStatusId, PIBNAContext context)
        {
            List<TeamModel> teamList = new List<TeamModel>();
            var team = await context.PTeam
                .Include(s => s.TeamStatus)
                 .Include(s => s.Club)
                .Include(s => s.Division)
                 .Include(s => s.User)
                 .Where(s => s.EndDate == null && s.SeasonId == seasonId && s.DivisionId == divisionId)
                .Select(s => s).ToListAsync();

            if (teamStatusId > 0)
                team = team.Where(s => s.TeamStatusId == teamStatusId).ToList();

            return await GetTeamData(team, _mapper, context);
        }

        private async Task<List<TeamModel>> GetTeamData(List<PTeam> data, IMapper _mapper, PIBNAContext context)
        {

            List<TeamModel> teamList = new List<TeamModel>();
            if (data.Count > 0)
            {
                try
                {
                    var teamIdList = data.Select(s => s.TeamId).ToArray();

                    var rosters = await context.PTeamRoster
                       .Where(s => teamIdList.Contains(s.TeamId) && s.EndDate == null)
                       .Select(s => new TeamRosterModel
                       {
                           TeamId = s.TeamId,
                           TeamRosterId = s.TeamRosterId,
                           MemberId = s.MemberId,
                           LastName = s.Member.LastName,
                           FirstName = s.Member.FirstName,
                           MiddleName = s.Member.MiddleName,
                           DateOfBirth = s.Member.DateOfBirth,
                           IsApproved = false, // getApprovalStatus(context.PMemberApproval.Where(c => c.MemberId == s.MemberId && c.EndDate == null)),
                           ApprovedDate = context.PMemberApproval.Where(c => c.MemberId == s.MemberId && c.EndDate == null).FirstOrDefault().ApprovedDate
                       }).ToListAsync();

                    var memberList = rosters.Select(s => s.MemberId).ToArray();

                    var memberApprovalList = context.PMemberApproval.Where(c => memberList.Contains(c.MemberId) && c.EndDate == null)
                            .Select(s => new MemberApproval
                            {
                                MemberId = s.MemberId,
                                IsApproved = s.IsApproved,
                                Notes = s.Notes
                            }).ToList();

                    foreach(var rs in rosters)
                    {
                        var approvedStatus = memberApprovalList.Where(s => s.MemberId == rs.MemberId).FirstOrDefault();
                        rs.IsApproved = approvedStatus != null? approvedStatus.IsApproved : false ;
                        
                    }



                    //context.PMemberApproval.Where(c => c.MemberId == s.MemberId && c.EndDate == null).FirstOrDefault().IsApproved,

                    var officials = await context.PTeamOfficial
                      .Include(s => s.Position)
                       .Where(s => teamIdList.Contains(s.TeamId) && s.EndDate == null)
                      .Select(s => new TeamOfficialModel
                      {
                          TeamId = s.TeamId,
                          Name = s.Name,
                          Email = s.Email,
                          Phone = s.Phone,
                          TeamOfficialId = s.TeamOfficialId,
                          PositionName = s.Position.PositionDescription,
                          PositionId = s.PositionId
                      }).ToListAsync();

                    foreach (var r in data)
                    {
                        var teamRosters = rosters.Where(s => s.TeamId == r.TeamId).ToList();
                        var teamOfficials = officials.Where(s => s.TeamId == r.TeamId).ToList();

                        TeamModel model = _mapper.Map<TeamModel>(r);

                        model.Rosters = teamRosters;
                        model.RosterCount = teamRosters.Count();
                        model.RosterApproved = teamRosters.Where(s => s.IsApproved == true).Count();
                        model.RosterDisapproved = model.RosterCount - model.RosterApproved;
                        model.Officials = teamOfficials;
                        teamList.Add(model);
                    }
                }
                catch (Exception err)
                {
                    string errmsg = err.Message;
                }
            }
            return teamList;

        }

        static bool getApprovalStatus(PMemberApproval? pApproval)
        {
            bool isApproved = false;
            if (pApproval != null)
            {
                isApproved = pApproval.IsApproved;
            }
            return isApproved;


        }

        public async Task<OpenRosterInfoPageModel> GetOpenPlayers(string searchKey, string sortKey,
           int clubid, int divisionid, int teamid, int pageSize, int page, PIBNAContext context)
        {
            int sCurrent = DateTime.Now.Year;
            OpenRosterInfoPageModel model = new OpenRosterInfoPageModel();
            var GetAllTeamInDivision = await (from p in context.PTeam
                                              where
  p.EndDate == null &&
  p.ClubId == clubid &&
  p.DivisionId == divisionid &&
  p.SeasonId == sCurrent
                                              select p).ToListAsync();

            var teamInDivisionList = GetAllTeamInDivision.Select(s => s.TeamId).ToArray();
            var PlayerInTeam = await (from p in context.PTeamRoster
                                      join team in context.PTeam on p.TeamId equals team.TeamId
                                      where p.EndDate == null &&
                                      teamInDivisionList.Contains(p.TeamId)
                                      && team.ClubId == clubid
                                      select p).ToListAsync();

            var pTeamArray = PlayerInTeam.Select(s => s.MemberId).ToArray();

            //Get division Min and MaxAge
            var dInfo = await context.PDivision.Where(s => s.DivisionId == divisionid).Select(s => s).FirstOrDefaultAsync();

            int StartYear = DateTime.Now.Year - dInfo.MaxAge;
            int EndYear = DateTime.Now.Year - dInfo.MinAge;

            //Get all the member in the club that is in this age group
            var memberList = (from p in context.PMember
                              where p.ClubId == clubid
                              && !pTeamArray.Contains(p.MemberId)
                              && p.DateOfBirth.Year >= StartYear && p.DateOfBirth.Year <= EndYear
                              && p.EndDate == null
                              select new AvailableRosterModel
                              {
                                  MemberId = p.MemberId,
                                  FirstName = p.FirstName,
                                  LastName = p.LastName,
                                  MiddleName = p.MiddleName,
                                  DateOfBirth = p.DateOfBirth
                              });

            if (!String.IsNullOrEmpty(searchKey))
                memberList = memberList.Where(s => s.FirstName.Contains(searchKey) || s.LastName.Contains(searchKey));

            var paginatedList = await PaginatedList<AvailableRosterModel>.CreateAsync(memberList.AsNoTracking(), page, pageSize);
            var userTypeList = await context.PUserType.Select(s => s).ToListAsync();
            int seasonId = DateTime.Now.Year;

            var iList = paginatedList;
            model.RecordCount = paginatedList.TotalRecord;
            model.data = paginatedList;
            return model;
        }

        public async Task<List<AvailableRosterModel>> GetAvailablePlayer(int clubid, int divisionid, int teamid, PIBNAContext context)
        {
            List<AvailableRosterModel> model = new List<AvailableRosterModel>();
            int cSeasonId = DateTime.Now.Year;

            var GetAllTeamInDivision = await (from p in context.PTeam
                                              where
                          p.EndDate == null &&
                          p.ClubId == clubid &&
                          p.DivisionId == divisionid &&
                          p.SeasonId == cSeasonId
                                              select p).ToListAsync();

            var teamInDivisionList = GetAllTeamInDivision.Select(s => s.TeamId).ToArray();


            var PlayerInTeam = await (from p in context.PTeamRoster
                                      join team in context.PTeam on p.TeamId equals team.TeamId
                                      where p.EndDate == null &&
                                      teamInDivisionList.Contains(p.TeamId)
                                      && team.ClubId == clubid
                                      select p).ToListAsync();

            var pTeamArray = PlayerInTeam.Select(s => s.MemberId).ToArray();

            //Get division Min and MaxAge
            var dInfo = await context.PDivision.Where(s => s.DivisionId == divisionid).Select(s => s).FirstOrDefaultAsync();

            int StartYear = DateTime.Now.Year - dInfo.MaxAge;
            int EndYear = DateTime.Now.Year - dInfo.MinAge;
            DateTime sBirth = new DateTime(StartYear, 11, 1);
            DateTime eBirth = new DateTime(EndYear, 11, 3);


            //Get all the member in the club that is in this age group
            var memberList = await (from p in context.PMember
                                    where p.ClubId == clubid
                                    && !pTeamArray.Contains(p.MemberId)
                                    && p.DateOfBirth >= sBirth && p.DateOfBirth <= eBirth
                                    && p.EndDate == null
                                    select p).ToListAsync();

            foreach (var t in memberList)
            {
                AvailableRosterModel m = new AvailableRosterModel();
                m.MemberId = t.MemberId;
                m.FirstName = t.FirstName;
                m.LastName = t.LastName;
                m.MiddleName = t.MiddleName;
                m.DateOfBirth = t.DateOfBirth;
                model.Add(m);
            }
            return model.OrderBy(s => s.FirstName).ToList();
        }

        public async Task<List<PTeamStatus>> GetTeamStatusList(PIBNAContext context)
        {
            List<PTeamStatus> model = new List<PTeamStatus>();
            return await context.PTeamStatus.Select(s => s).ToListAsync();
        }

        public async Task<int> SaveTeam(TeamModel model, PIBNAContext context)
        {
            int TeamId = 0;
            try
            {
                model.TeamId = await SaveUpdateTeam(context, model);
                await SaveTeamOfficials(context, model);
                await SaveTeamRosters(context, model);
            }
            catch (Exception err)
            {
                string msg = err.Message;
            }
            return TeamId;
        }

        private async Task SaveTeamRosters(PIBNAContext ctx, TeamModel model)
        {
            //Check if the Member is remove on the current list
            var cRoster = await (ctx.PTeamRoster.Where(s => s.TeamId == model.TeamId && s.EndDate == null).Select(p => p)).ToListAsync();
            foreach (var r in cRoster)
            {
                var isExist = model.Rosters.Where(s => s.TeamRosterId == r.TeamRosterId).Select(s => s).FirstOrDefault();
                if (isExist == null)
                {
                    r.EndDate = DateTime.Now;
                }
            }
            await ctx.SaveChangesAsync();
            var NewRoster = model.Rosters.Where(s => s.TeamRosterId == 0).Select(s => s).ToList();
            foreach (var r in NewRoster)
            {
                if (r.MemberId <= 0)
                {
                    PMember m = new PMember();
                    m.FirstName = r.FirstName;
                    m.LastName = r.LastName;
                    m.MiddleName = r.MiddleName;
                    m.DateOfBirth = r.DateOfBirth;
                    m.IsBlockListed = false;
                    m.FromDate = DateTime.Now;
                    m.ClubId = model.ClubId;
                    m.UserId = model.UserId;
                    await ctx.PMember.AddAsync(m);
                    await ctx.SaveChangesAsync();
                    r.MemberId = m.MemberId;
                }

                if (r.TeamRosterId <= 0)
                {
                    PTeamRoster roster = new PTeamRoster();
                    roster.TeamId = model.TeamId;
                    roster.MemberId = r.MemberId;
                    roster.FromDate = DateTime.Now;
                    await ctx.PTeamRoster.AddAsync(roster);
                }
            }
            await ctx.SaveChangesAsync();
        }

        private async Task<int> SaveUpdateTeam(PIBNAContext ctx, TeamModel model)
        {
            int teamId = 0;
            try
            {
                string TeamName = model.ClubName;
                TeamName += "-" + model.DivisionName;
                TeamName += "-" + model.Officials[0].Name;
                model.TeamName = TeamName;
                int tempSeasonID = DateTime.Now.Year;
                var team = await (from p in ctx.PTeam
                                  where p.TeamId == model.TeamId && p.EndDate == null
                                  select p).FirstOrDefaultAsync();
                if (team == null)
                {
                    //Add team Here
                    PTeam t = new PTeam();
                    t.ClubId = model.ClubId;
                    t.DivisionId = model.DivisionId;
                    t.SeasonId = tempSeasonID;
                    t.TeamName = model.TeamName;
                    t.UserId = model.UserId;
                    t.FromDate = DateTime.Now;
                    t.TeamStatusId = model.TeamStatusId;
                    if (model.TeamStatusId == 2)
                    {
                        t.SubmitForApprovalDate = DateTime.Now;
                    }

                    await ctx.PTeam.AddAsync(t);
                    await ctx.SaveChangesAsync();
                    model.TeamId = t.TeamId;
                }
                else
                {
                    team.ClubId = model.ClubId;
                    team.DivisionId = model.DivisionId;
                    team.UserId = model.UserId;
                    team.TeamName = model.TeamName;
                    team.TeamStatusId = model.TeamStatusId;
                    if (model.TeamStatusId == 1)
                    {
                        team.SubmitForApprovalDate = DateTime.Now;
                    }
                    team.FromDate = DateTime.Now;
                    await ctx.SaveChangesAsync();

                }

                teamId = model.TeamId;
            }
            catch (Exception ex)
            {
                var r = ex.Message;
            }
            return teamId;
        }

        private async Task SaveTeamOfficials(PIBNAContext ctx, TeamModel model)
        {
            foreach (var o in model.Officials)
            {
                var official = await ctx.PTeamOfficial.Where(s => s.TeamOfficialId == o.TeamOfficialId).Select(s => s).FirstOrDefaultAsync();
                if (official == null)
                {
                    PTeamOfficial tOfficial = new PTeamOfficial();
                    tOfficial.TeamId = model.TeamId;
                    tOfficial.Name = o.Name;
                    tOfficial.Email = o.Email;
                    tOfficial.Phone = o.Phone;
                    tOfficial.PositionId = o.PositionId; //head coach
                    tOfficial.FromDate = DateTime.Now;
                    await ctx.PTeamOfficial.AddAsync(tOfficial);
                }
                else
                {
                    official.Name = o.Name;
                    official.Email = o.Email;
                    official.Phone = o.Phone;
                }
            }
            await ctx.SaveChangesAsync();
        }


        public async Task SaveTeamStatus(TeamHostStatus value, PIBNAContext context)
        {
            var team = await (from p in context.PTeam
                              where p.TeamId == value.TeamId
                              select p).FirstOrDefaultAsync();
            if (team != null)
            {
                team.TeamStatusId = value.teamStatusId;
                team.ApprovedDate = DateTime.Now;
                team.ApprovedBy = value.UserId;
                await context.SaveChangesAsync();
            }
        }
    }


    public class MemberApproval
    {
        public int MemberId { get; set; }
        public bool IsApproved { get; set; }
        public string Notes { get; set; }
    }
}
