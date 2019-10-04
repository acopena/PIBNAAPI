using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PClubOfficial
    {
        public int ClubOfficialId { get; set; }
        public int ClubId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PositionId { get; set; }

        public virtual PClub Club { get; set; }
        public virtual PPosition Position { get; set; }
    }
}
