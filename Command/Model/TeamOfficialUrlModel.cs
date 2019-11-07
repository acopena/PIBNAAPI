using System;
using System.Collections.Generic;
using System.Text;

namespace PIBNAAPI.Command.Model
{
    public class TeamOfficialUrlModel
    {
        public int TeamOfficialId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int PositionId { get; set; }
    }
}
