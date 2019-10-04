using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PUserRole
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual PUser User { get; set; }
        public virtual PUserType UserType { get; set; }
    }
}
