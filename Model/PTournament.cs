using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PTournament
    {
        public PTournament()
        {
            PTournamentTrx = new HashSet<PTournamentTrx>();
        }

        public int TournamentId { get; set; }
        public int DivisionId { get; set; }
        public int UserId { get; set; }
        public int HostCity { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual PDivision Division { get; set; }
        public virtual PClub HostCityNavigation { get; set; }
        public virtual PUser User { get; set; }
        public virtual ICollection<PTournamentTrx> PTournamentTrx { get; set; }
    }
}
