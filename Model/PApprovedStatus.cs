using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PApprovedStatus
    {
        public PApprovedStatus()
        {
            PMemberApproval = new HashSet<PMemberApproval>();
        }

        public int ApprovedStatusId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PMemberApproval> PMemberApproval { get; set; }
    }
}
