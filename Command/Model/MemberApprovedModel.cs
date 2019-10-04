using System;
using System.Collections.Generic;
using System.Text;

namespace PIBNAAPI.Command.Model
{
    public class MemberApprovedModel
    {

        public int TeamId{ get; set; }
        public string TeamName{ get; set; }
        public List<MemberApprovedRosterModel> Rosters  { get; set; }
        public List<MemberApprovedOfficialModel> Officials { get; set; }
    }

    public class MemberApprovedRosterModel
    {
        public int? MemberApprovedId { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public int? ApprovedStatusId { get; set; }
        public bool? IsApproved { get; set; }
    }

    public class MemberApprovedOfficialModel
    {
        public string OfficialName{ get; set; }
        public string Email{ get; set; }
        public string Phone{ get; set; }
        public string PositionDescription{ get; set; }
    }

    public class MemberApprovalStatusModel
    {
        public int Id{ get; set; }
        public string Description{ get; set; }
    }

    public class MemberApprovedList
    {
        public int MemberId { get; set; }
        public int ApprovalStatusId { get; set; }
        public int UserId { get; set; }
        public string Notes { get; set; }
        public bool IsApproved { get; set; }
    }
}
