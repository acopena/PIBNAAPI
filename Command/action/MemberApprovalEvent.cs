using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PIBNAAPI.Command.Interface;
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
        public async Task<MemberApprovedModel> getMembersApproval(int teamId, IMapper mapper, PIBNAContext context)
        {
            MemberApprovedModel teamInfo = new MemberApprovedModel();

            teamInfo = await context.PTeam.Where(s => s.TeamId == teamId)
                .Select(s => new MemberApprovedModel()
                {
                    TeamId = s.TeamId,
                    TeamName = s.TeamName
                }
                ).FirstOrDefaultAsync();


            teamInfo.Rosters = new List<MemberApprovedRosterModel>();
            teamInfo.Officials = new List<MemberApprovedOfficialModel>();

            teamInfo.Officials = await context.PTeamOfficial
                .Include(s => s.Position)
                .Where(s => s.TeamId == teamId && s.EndDate == null)
                .Select(s => new MemberApprovedOfficialModel()
                {
                    OfficialName = s.Name,
                    Email = s.Email,
                    Phone = s.Phone,
                    PositionDescription = s.Position.PositionDescription
                }).ToListAsync();

            // String.Join(s.Member.LastName, ", ", s.Member.FirstName),

            var rosters = await context.PTeamRoster
                .Include(s =>s.Member)
              .Where(s => s.TeamId == teamId && s.EndDate == null)
              .Select(s =>s).ToListAsync();

            foreach(var r in rosters)
            {
                MemberApprovedRosterModel roster = new MemberApprovedRosterModel()
                {
                    MemberId = r.MemberId,
                    MemberName = String.Join(", ",r.Member.LastName, r.Member.FirstName),
                    BirthDate = r.Member.DateOfBirth,
                    MemberApprovedId = 0,
                    ApprovedDate = DateTime.Now,
                    UserId = 0,
                    UserName = "",
                    ApprovedStatusId = 0,
                    IsApproved = false
                };
                teamInfo.Rosters.Add(roster);
            }

            var arrayMembers = teamInfo.Rosters.Select(x => x.MemberId).ToArray();


            var approvalData = await (from p in context.PMemberApproval
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
            return teamInfo;
        }


        public async Task<List<MemberApprovalStatusModel>> getApprovedStatus(IMapper _mapper, PIBNAContext _context)
        {
            List<MemberApprovalStatusModel> statusList = new List<MemberApprovalStatusModel>();
            statusList = await _context.PApprovedStatus
                .Select(s => new MemberApprovalStatusModel()
                {
                    Id = s.ApprovedStatusId,
                    Description = s.Description
                }).ToListAsync();
            return statusList;
        }

        public async Task postMembersApproval(List<MemberApprovedList> memberList, IMapper mapper, PIBNAContext context)
        {

            foreach (var m in memberList)
            {
                var idata = await context.PMemberApproval.Where(x => x.MemberId == m.MemberId).Select(x => x).FirstOrDefaultAsync();
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
                    await context.PMemberApproval.AddAsync(mApproval);
                }
                else
                {
                    idata.IsApproved = m.IsApproved;
                    idata.ApprovedDate = DateTime.Now;
                    idata.UserId = m.UserId;
                }
            }
            await context.SaveChangesAsync();

        }
    }
}
