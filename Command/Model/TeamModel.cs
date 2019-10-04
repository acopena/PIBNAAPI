using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.Model
{
    public class TeamModelList
    {
        public List<TeamsModel> data { get; set; }
        public int RecordCount{ get; set; }
        
    }


    public class TeamModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int SeasonId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public int TeamStatusId { get; set; }
        public string TeamStatusDescription { get; set; }
        public int? ApprovedBy { get; set; }
        public int RosterCount { get; set; }
        public int RosterDisapproved { get; set; }
        public int RosterApproved { get; set; }

        public DateTime? ApprovedDate { get; set; }
        public DateTime? SubmitForApprovalDate { get; set; }

        public List<TeamOfficialModel> Officials{ get; set; }
        public List<TeamRosterModel> Rosters { get; set; }
    }

    public class TeamsModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int SeasonId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public int TeamStatusId { get; set; }
        public string TeamStatusDescription { get; set; }
        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }
        public DateTime? SubmitForApprovalDate { get; set; }
        public int RosterCount { get; set; }
        public string CoachName { get; set; }

                
    }




    public class TeamRosterModel
    {
        public int TeamRosterId { get; set; }
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }


    }

    public class TeamOfficialModel
    {
        public int TeamOfficialId{ get; set; }
        
        public string Name{ get; set; }
        public string Email{ get; set; }
        public string Phone{ get; set; }
        public int PositionId{ get; set; }
        public string PositionName { get; set; }
    }
}
