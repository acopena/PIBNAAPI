using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PIBNAAPI.Command.action
{
    public class MemberEvent : IMemberEvent
    {
        public async Task<MemberSearchReturn> GetMemberInfo(string firstname, string lastname, string middlename, DateTime bod, int clubId)
        {
            if (middlename == null)
            {
                middlename = string.Empty;
            }

            MemberSearchReturn model = new MemberSearchReturn();
            using (var ctx = new PIBNAContext())
            {
                var data = await (from p in ctx.PMember
                                  where p.EndDate == null &&
                                  p.FirstName.ToUpper() == firstname.ToUpper() &&
                                  p.LastName.ToUpper() == lastname.ToUpper() &&
                                  p.MiddleName.ToUpper() == middlename.ToUpper() &&
                                  p.DateOfBirth.Day == bod.Day &&
                                  p.DateOfBirth.Month == bod.Month &&
                                  p.DateOfBirth.Year == bod.Year
                                  select p).FirstOrDefaultAsync();
                if (data != null)
                {
                    model.clubId = data.ClubId;
                    model.MemberId = data.MemberId;
                    model.Message = "";

                    if (data.ClubId != clubId)
                    {
                        model.Message = "This player is currently register to another club";
                        model.IsValid = false;
                    }
                    else
                    {
                        model.IsValid = true;
                        model.Message = "";
                    }
                }
                else
                {
                    model.clubId = clubId;
                    model.MemberId = 0;
                    model.Message = "New";
                    model.IsValid = true;
                }
            }
            return model;
        }

        public async Task<MemberInfoModel> GetMemberById(int Id)
        {
            MemberInfoModel model = new MemberInfoModel();

            using (var ctx = new PIBNAContext())
            {
                model = await ctx.PMember
                    .Include(s => s.PTeamRoster)
                    .Include(s => s.Club)
                    .Where(s => s.MemberId == Id)
                    .Select(s => new MemberInfoModel()
                    {
                        MemberId = s.MemberId,
                        Name = s.FirstName + ' ' + s.MiddleName + ' ' + s.LastName,
                        DateOfBirth = s.DateOfBirth
                        ,
                        ClubName = s.Club.ClubName,

                    }).FirstOrDefaultAsync();

                model.TeamList = new List<MemberTeamModel>();
                model.TeamList = await ctx.PTeamRoster
                    .Include(s => s.Team)
                    .Where(s => s.MemberId == Id)
                    .Select(s => new MemberTeamModel()
                    {
                        TeamId = s.TeamId,
                        TeamName = s.Team.TeamName,
                        DivisionName = s.Team.Division.DivisionName,
                        SeasonId = s.Team.SeasonId,
                        ClubName = s.Team.Club.ClubName

                    }).ToListAsync();


            }
            return model;
        }
    }
}
