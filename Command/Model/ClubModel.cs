using System;
using System.Collections.Generic;
using System.Text;

namespace PIBNAAPI.Command.Model
{
    public class ClubModel
    {
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string WebSite { get; set; }
        public List<ClubOfficialModel> OfficialList { get; set; }
    }

    public class ClubOfficialModel
    {
        public int ClubOfficialId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int PositionId { get; set; }

    }

    public class ClubPageModel
    {
        public List<ClubModel> data { get; set; }
        public int RecordCount { get; set; }
    }

    public class UserModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class TeamDashboardModel
    {
        public TeamSummaryModel Header { get; set; }
        public List<TeamSummaryModel> Data { get; set; }
        public TeamSummaryModel Footer { get; set; }
    }
    public class TeamSummaryModel
    {
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public List<DivisionSummaryModel> DivisionInfo { get; set; }


    }
    public class DivisionSummaryModel
    {
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int TeamCount { get; set; }
    }


}
