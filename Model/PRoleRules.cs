using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PRoleRules
    {
        public int RoleRules { get; set; }
        public int RoleId { get; set; }
        public int UserTypeId { get; set; }
        public int Access { get; set; }

        public virtual PRole Role { get; set; }
        public virtual PUserType UserType { get; set; }
    }
}
