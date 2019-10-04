using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PTeamRoster
    {
        public int TeamRosterId { get; set; }
        public int TeamId { get; set; }
        public int MemberId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual PMember Member { get; set; }
        public virtual PTeam Team { get; set; }
    }
}
