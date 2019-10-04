using Microsoft.EntityFrameworkCore;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.action
{
    public class MemberApprovalEvent : IMemberApprovalEvent
    {
        public async Task<MemberApprovedModel> getMembersApproval(int teamId)
        {
            MemberApprovedModel teamInfo = new MemberApprovedModel();
            using (var ctx = new PIBNAContext())
            {
                teamInfo = await ctx.PTeam.Where(s => s.TeamId == teamId)
                    .Select(s => new MemberApprovedModel
                    {
                        TeamId = s.TeamId,
                        TeamName = s.TeamName
                    }
                    ).FirstOrDefaultAsync();


                teamInfo.Rosters = new List<MemberApprovedRosterModel>();
                teamInfo.Officials = new List<MemberApprovedOfficialModel>();

                teamInfo.Officials = await ctx.PTeamOfficial
                    .Include(s => s.Position)
                    .Where(s => s.TeamId == teamId && s.EndDate == null)
                    .Select(s => new MemberApprovedOfficialModel
                    {
                        OfficialName = s.Name,
                        Email = s.Email,
                        Phone = s.Phone,
                        PositionDescription = s.Position.PositionDescription
                    }).ToListAsync();

                teamInfo.Rosters = await ctx.PTeamRoster
                    .Include(s => s.Member)
                    .Where(s => s.TeamId == teamId && s.EndDate == null)
                    .Select(s => new MemberApprovedRosterModel
                    {
                        MemberId = s.MemberId,
                        MemberName = s.Member.LastName + ", " + s.Member.FirstName + " " + s.Member.MiddleName,
                        BirthDate = s.Member.DateOfBirth,
                        MemberApprovedId = 0,
                        ApprovedDate = DateTime.Now,
                        UserId = 0,
                        UserName = "",
                        ApprovedStatusId = 0,
                        IsApproved = false
                    }).OrderBy(s => s.MemberName).ToListAsync();

                var arrayMembers = teamInfo.Rosters.Select(x => x.MemberId).ToArray();


                var approvalData = await (from p in ctx.PMemberApproval
                                          where arrayMembers.Contains(p.MemberId)
                                          select p).ToListAsync();

                foreach (var data in teamInfo.Rosters)
                {
                    var approval = approvalData

                        .Where(s => s.MemberId == data.MemberId).FirstOrDefault();
                    if (approval != null)
                    {
                        data.MemberApprovedId = approval.MemberApprovalId;
                        data.ApprovedDate = approval.ApprovedDate;
                        data.UserId = approval.UserId;
                        data.UserName = "";
                        data.ApprovedStatusId = approval.ApprovedStatusId;
                        data.IsApproved = approval.IsApproved;
                    }

                }
            }
            return teamInfo;
        }


        public async Task<List<MemberApprovalStatusModel>> getApprovedStatus(int teamId)
        {
            List<MemberApprovalStatusModel> statusList = new List<MemberApprovalStatusModel>();
            using (var ctx = new PIBNAContext())
            {
                statusList = await ctx.PApprovedStatus
                    .Select(s => new MemberApprovalStatusModel
                    {
                        Id = s.ApprovedStatusId,
                        Description = s.Description
                    }).ToListAsync();
            }
            return statusList;
        }

        public async Task postMembersApproval(List<MemberApprovedList> memberList)
        {
            using (var ctx = new PIBNAContext())
            {
                foreach (var m in memberList)
                {
                    var idata = ctx.PMemberApproval.Where(x => x.MemberId == m.MemberId).Select(x => x).FirstOrDefault();
                    if (idata == null)
                    {
                        PMemberApproval mApproval = new PMemberApproval();
                        mApproval.MemberId = m.MemberId;
                        mApproval.ApprovedStatusId = 1;// m.ApprovalStatusId;
                        mApproval.ApprovedDate = DateTime.Now;
                        mApproval.IsApproved = m.IsApproved;
                        mApproval.UserId = m.UserId;
                        mApproval.FromDate = DateTime.Now;
                        mApproval.Notes = m.Notes;
                        await ctx.PMemberApproval.AddAsync(mApproval);
                    }
                    else
                    {
                        idata.IsApproved = m.IsApproved;
                        idata.ApprovedDate = DateTime.Now;
                        idata.UserId = m.UserId;
                    }
                }
                await ctx.SaveChangesAsync();
            }
        }
    }
}
