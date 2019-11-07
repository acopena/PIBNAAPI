using System;
using System.Collections.Generic;
using System.Text;

namespace PIBNAAPI.Command.Model
{
    public class TeamListModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int SeasonId { get; set; }
        public int UserId { get; set; }
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public string CoachName { get; set; }
        public string CoachEmail { get; set; }
        public int TotalRoster { get; set; }
        public int? ApprovedBy { get; set; }
        public int TeamStatusId { get; set; }
        public string TeamStatusDescription { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? SubmitForApprovalDate { get; set; }
    }
}
