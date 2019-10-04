using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PMemberApproval
    {
        public int MemberApprovalId { get; set; }
        public int MemberId { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int UserId { get; set; }
        public int ApprovedStatusId { get; set; }
        public string Notes { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsApproved { get; set; }

        public virtual PApprovedStatus ApprovedStatus { get; set; }
        public virtual PMember Member { get; set; }
        public virtual PUser User { get; set; }
    }
}
