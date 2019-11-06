using PIBNAAPI.Command.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.action
{
    public interface IMemberApprovalEvent
    {
        Task<MemberApprovedModel> getMembersApproval(int teamId);
        Task<List<MemberApprovalStatusModel>> getApprovedStatus(int teamId);
        Task postMembersApproval(List<MemberApprovedList> memberList);
    }
}
