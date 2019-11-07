using System;
using System.Collections.Generic;
using System.Text;

namespace PIBNAAPI.Command.Model
{
    public class AvailableRosterModel
    {
        public int MemberId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
