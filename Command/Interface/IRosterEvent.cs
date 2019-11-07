using AutoMapper;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.Interface
{
    public interface IRosterEvent
    {
        Task<MemberSearchReturn> GetMemberInfo(string firstname, string lastname, string middlename, DateTime bod, int clubId, IMapper _mapper, PIBNAContext _context);
        Task<MemberInfoModel> GetMemberById(int Id, IMapper _mapper, PIBNAContext _context);
    }
}
