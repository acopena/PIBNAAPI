using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PMember
    {
        public PMember()
        {
            PMemberApproval = new HashSet<PMemberApproval>();
            PTeamRoster = new HashSet<PTeamRoster>();
        }

        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClubId { get; set; }
        public int UserId { get; set; }
        public bool IsBlockListed { get; set; }
        public DateTime? DateBlockListed { get; set; }
        public string NotesBlockListed { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string MiddleName { get; set; }

        public virtual PClub Club { get; set; }
        public virtual ICollection<PMemberApproval> PMemberApproval { get; set; }
        public virtual ICollection<PTeamRoster> PTeamRoster { get; set; }
    }
}
