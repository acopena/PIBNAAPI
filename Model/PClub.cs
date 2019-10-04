using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PClub
    {
        public PClub()
        {
            PClubHost = new HashSet<PClubHost>();
            PClubOfficial = new HashSet<PClubOfficial>();
            PImages = new HashSet<PImages>();
            PMember = new HashSet<PMember>();
            PTeam = new HashSet<PTeam>();
            PTournament = new HashSet<PTournament>();
            PUser = new HashSet<PUser>();
        }

        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public string ClubShortName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int UserId { get; set; }
        public string WebSite { get; set; }

        public virtual ICollection<PClubHost> PClubHost { get; set; }
        public virtual ICollection<PClubOfficial> PClubOfficial { get; set; }
        public virtual ICollection<PImages> PImages { get; set; }
        public virtual ICollection<PMember> PMember { get; set; }
        public virtual ICollection<PTeam> PTeam { get; set; }
        public virtual ICollection<PTournament> PTournament { get; set; }
        public virtual ICollection<PUser> PUser { get; set; }
    }
}
