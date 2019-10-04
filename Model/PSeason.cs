using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PSeason
    {
        public PSeason()
        {
            PClubHost = new HashSet<PClubHost>();
            PTeam = new HashSet<PTeam>();
        }

        public int SeasonId { get; set; }
        public string SeasonName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<PClubHost> PClubHost { get; set; }
        public virtual ICollection<PTeam> PTeam { get; set; }
    }
}
