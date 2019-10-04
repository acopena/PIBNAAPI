using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PTeamOfficial
    {
        public int TeamOfficialId { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int PositionId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual PPosition Position { get; set; }
        public virtual PTeam Team { get; set; }
    }
}
