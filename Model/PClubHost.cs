using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PClubHost
    {
        public int HostId { get; set; }
        public int ClubId { get; set; }
        public int SeasonId { get; set; }
        public bool? IsCurrentHost { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual PClub Club { get; set; }
        public virtual PSeason Season { get; set; }
    }
}
