﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PIBNAAPI.Command.Model
{
    public class SearchPageModel
    {
        public List<SearchPibnaModel> data { get; set; }
        public int RecordCount { get; set; }

    }

    public class SearchPibnaModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ClubName { get; set; }
        public string Notes { get; set; }
    }
}
