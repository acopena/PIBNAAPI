using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PDivision
    {
        public PDivision()
        {
            PTeam = new HashSet<PTeam>();
            PTournament = new HashSet<PTournament>();
        }

        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string DivisionShortName { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool IsHeightRequired { get; set; }
        public decimal MaxHeightRequired { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? AgeGroup { get; set; }

        public virtual ICollection<PTeam> PTeam { get; set; }
        public virtual ICollection<PTournament> PTournament { get; set; }
    }
}
