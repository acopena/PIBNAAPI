using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PUser
    {
        public PUser()
        {
            PMemberApproval = new HashSet<PMemberApproval>();
            PTeam = new HashSet<PTeam>();
            PTournament = new HashSet<PTournament>();
            PUserRole = new HashSet<PUserRole>();
            PWebContent = new HashSet<PWebContent>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccessCode { get; set; }
        public int ClubId { get; set; }
        public bool IsBlockAccount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DateUpdated { get; set; }

        public virtual PClub Club { get; set; }
        public virtual ICollection<PMemberApproval> PMemberApproval { get; set; }
        public virtual ICollection<PTeam> PTeam { get; set; }
        public virtual ICollection<PTournament> PTournament { get; set; }
        public virtual ICollection<PUserRole> PUserRole { get; set; }
        public virtual ICollection<PWebContent> PWebContent { get; set; }
    }
}
