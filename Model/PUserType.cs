using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PUserType
    {
        public PUserType()
        {
            PRoleRules = new HashSet<PRoleRules>();
            PUserRole = new HashSet<PUserRole>();
        }

        public int UserTypeId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PRoleRules> PRoleRules { get; set; }
        public virtual ICollection<PUserRole> PUserRole { get; set; }
    }
}
