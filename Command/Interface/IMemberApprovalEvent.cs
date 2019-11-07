using AutoMapper;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.Interface
{
    public interface IMemberApprovalEvent
    {
        Task<MemberApprovedModel> getMembersApproval(int teamId, IMapper _mapper, PIBNAContext _context);
        Task<List<MemberApprovalStatusModel>> getApprovedStatus(IMapper _mapper, PIBNAContext _context);
        Task postMembersApproval(List<MemberApprovedList> memberList, IMapper _mapper, PIBNAContext _context);
    }
}
