using PIBNAAPI.Command.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.action
{
    public interface IMemberEvent
    {
        Task<MemberSearchReturn> GetMemberInfo(string firstname, string lastname, string middlename, DateTime bod, int clubId);
        Task<MemberInfoModel> GetMemberById(int Id);
    }
}
