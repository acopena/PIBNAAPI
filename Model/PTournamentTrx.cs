using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PTournamentTrx
    {
        public int TournamentTrx { get; set; }
        public int TournamentId { get; set; }
        public int TeamId { get; set; }
        public bool? IsHomeCourt { get; set; }
        public int Score { get; set; }
        public int TournamentStatusId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual PTeam Team { get; set; }
        public virtual PTournament Tournament { get; set; }
    }
}
