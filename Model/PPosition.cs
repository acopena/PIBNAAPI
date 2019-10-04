using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PPosition
    {
        public PPosition()
        {
            PClubOfficial = new HashSet<PClubOfficial>();
            PTeamOfficial = new HashSet<PTeamOfficial>();
        }

        public int PositionId { get; set; }
        public string PositionDescription { get; set; }

        public virtual ICollection<PClubOfficial> PClubOfficial { get; set; }
        public virtual ICollection<PTeamOfficial> PTeamOfficial { get; set; }
    }
}
