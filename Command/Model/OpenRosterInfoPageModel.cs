using System;
using System.Collections.Generic;
using System.Text;

namespace PIBNAAPI.Command.Model
{
    public class OpenRosterInfoPageModel
    {
        public List<AvailableRosterModel> data { get; set; }
        public int RecordCount { get; set; }
    }
}
