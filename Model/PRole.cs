using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PRole
    {
        public PRole()
        {
            PRoleRules = new HashSet<PRoleRules>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<PRoleRules> PRoleRules { get; set; }
    }
}
