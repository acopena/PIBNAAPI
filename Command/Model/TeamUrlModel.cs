using System;
using System.Collections.Generic;
using System.Text;

namespace PIBNAAPI.Command.Model
{
    public class TeamUrlModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int DivisionId { get; set; }
        public int SeasonId { get; set; }
        public int UserId { get; set; }
        public int ClubId { get; set; }

        public int TeamOfficialId { get; set; }
        public int TeamOfficialId1 { get; set; }
        public string CoachName { get; set; }
        public string CoachName1 { get; set; }
        public string CoachEmail { get; set; }
        public string CoachEmail1 { get; set; }
        public string CoachPhone { get; set; }
        public string CoachPhone1 { get; set; }
        public string TeamRosters { get; set; }
        public string TeamOfficials { get; set; }
        public int SubmitForApproval { get; set; }
    }
}
