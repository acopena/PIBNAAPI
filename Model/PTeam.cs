using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PTeam
    {
        public PTeam()
        {
            PTeamOfficial = new HashSet<PTeamOfficial>();
            PTeamRoster = new HashSet<PTeamRoster>();
            PTournamentTrx = new HashSet<PTournamentTrx>();
        }

        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamShortName { get; set; }
        public int DivisionId { get; set; }
        public int SeasonId { get; set; }
        public int ClubId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int UserId { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? SubmitForApprovalDate { get; set; }
        public int TeamStatusId { get; set; }

        public virtual PClub Club { get; set; }
        public virtual PDivision Division { get; set; }
        public virtual PSeason Season { get; set; }
        public virtual PTeamStatus TeamStatus { get; set; }
        public virtual PUser User { get; set; }
        public virtual ICollection<PTeamOfficial> PTeamOfficial { get; set; }
        public virtual ICollection<PTeamRoster> PTeamRoster { get; set; }
        public virtual ICollection<PTournamentTrx> PTournamentTrx { get; set; }
    }
}
