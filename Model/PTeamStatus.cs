using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PTeamStatus
    {
        public PTeamStatus()
        {
            PTeam = new HashSet<PTeam>();
        }

        public int TeamStatusId { get; set; }
        public string TeamStatusDescription { get; set; }

        public virtual ICollection<PTeam> PTeam { get; set; }
    }
}
