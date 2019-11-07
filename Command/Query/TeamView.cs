using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using PIBNAAPI.Model;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PIBNAAPI.Command.Model;

namespace PIBNAAPI.Command.Query
{
    public static class TeamView_old
    {
        public static async Task<List<TeamModel>> GetTeamListByClub(int clubid, int seasonId, IMapper _mapper, PIBNAContext context)
        {
            List<TeamModel> teamList = new List<TeamModel>();

            var team = await context.PTeam
                .Include(s => s.TeamStatus)
                .Include(s => s.Club)
                .Include(s => s.Division)
                .Include(s => s.User)
                .Where(s => s.ClubId == clubid && s.EndDate == null && s.SeasonId == seasonId)
                .Select(s => s).ToListAsync();

            foreach (var r in team)
            {
                TeamModel model = await GetTeamData(r, _mapper, context);
                teamList.Add(model);
            }


            return teamList;
        }

        public static async Task<PaginatedList<TeamsModel>> GetTeamList(int clubid, int seasonId, int page, int pageSize, PIBNAContext context)
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

        public static System.Linq.IQueryable<T> Sort<T>(this System.Linq.IQueryable<T> source, string sortBy)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (string.IsNullOrEmpty(sortBy))
                throw new ArgumentNullException("sortBy");



            return source;
        }

        public static async Task<TeamModel> GetTeamByTeamId(int TeamId, IMapper _mapper, PIBNAContext context)
        {
            TeamModel model = new TeamModel();

            var team = await context.PTeam
                .Include(s => s.TeamStatus)
                .Include(s => s.Club)
                .Include(s => s.Division)
                .Include(s => s.User)
                .Where(s => s.TeamId == TeamId && s.EndDate == null)
                .Select(s => s).FirstOrDefaultAsync();

            model = await GetTeamData(team, _mapper, context);


            return model;
        }

        public static async Task<List<TeamModel>> GetTeamByStatus(IMapper _mapper, int seasonId, int clubId, int divisionId, int teamStatusId, PIBNAContext context)
        {

            List<TeamModel> teamList = new List<TeamModel>();

            var team = await context.PTeam
                 .Include(s => s.TeamStatus)
                  .Include(s => s.Club)
                 .Include(s => s.Division)
                  .Include(s => s.User)
                  .Where(s => s.EndDate == null && s.SeasonId == seasonId)
                 .Select(s => s).ToListAsync();

            if (clubId > 0)
                team = team.Where(s => s.ClubId == clubId).ToList();
            if (divisionId > 0)
                team = team.Where(s => s.DivisionId == divisionId).ToList();
            if (teamStatusId > 0)
                team = team.Where(s => s.TeamStatusId == teamStatusId).ToList();
            foreach (var r in team)
            {
                TeamModel model = await GetTeamData(r, _mapper, context);
                teamList.Add(model);
            }


            return teamList;
        }


        public static async Task<List<TeamModel>> GetTeamByDivision(IMapper _mapper, int seasonId, int divisionId, int teamStatusId, PIBNAContext context)
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
                {
                    team = team.Where(s => s.TeamStatusId == teamStatusId).ToList();
                }


                foreach (var r in team)
                {
                    TeamModel model = await GetTeamData(r, _mapper, context);
                    teamList.Add(model);
                }


            
            return teamList;
        }



        private static async Task<TeamModel> GetTeamData(PTeam Data, IMapper _mapper, PIBNAContext _context)
        {
            TeamModel model = _mapper.Map<TeamModel>(Data);
            var roster = await _context.PTeamRoster
                .Where(s => s.TeamId == Data.TeamId && s.EndDate == null)
                .Select(s => new TeamRosterModel
                {
                    TeamRosterId = s.TeamRosterId,
                    MemberId = s.MemberId,
                    LastName = s.Member.LastName,
                    FirstName = s.Member.FirstName,
                    MiddleName = s.Member.MiddleName,
                    DateOfBirth = s.Member.DateOfBirth,
                    IsApproved = _context.PMemberApproval.Where(c => c.MemberId == s.MemberId && c.EndDate == null).FirstOrDefault().IsApproved,
                    ApprovedDate = _context.PMemberApproval.Where(c => c.MemberId == s.MemberId && c.EndDate == null).FirstOrDefault().ApprovedDate

                }).ToListAsync();

            model.Rosters = roster;
            model.RosterCount = roster.Count();
            model.RosterApproved = roster.Where(s => s.IsApproved == true).Count();
            model.RosterDisapproved = model.RosterCount - model.RosterApproved;

            var officials = await _context.PTeamOfficial
                .Include(s => s.Position)
                .Where(s => s.EndDate == null && s.TeamId == Data.TeamId)
                .Select(s => new TeamOfficialModel
                {
                    Name = s.Name,
                    Email = s.Email,
                    Phone = s.Phone,
                    TeamOfficialId = s.TeamOfficialId,
                    PositionName = s.Position.PositionDescription,
                    PositionId = s.PositionId

                }).ToListAsync();
            model.Officials = officials;
            return model;
        }
    }


}
