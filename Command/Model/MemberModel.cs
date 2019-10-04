using System;
using System.Collections.Generic;
using System.Text;

namespace PIBNAAPI.Command.Model
{
    public class MemberModel
    {

    }


    public class MemberSearchReturn
    {
        public int MemberId { get; set; }
        public string Message { get; set; }
        public int clubId { get; set; }
        public bool IsValid { get; set; }
    }

    public class MemberInfoModel
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ClubName { get; set; }
        public List<MemberTeamModel> TeamList { get; set; }

    }

    public class MemberTeamModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string DivisionName { get; set; }
        public int SeasonId { get; set; }
        public string ClubName { get; set; }

    }
}
