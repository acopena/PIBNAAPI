using System;
using System.Collections.Generic;
using System.Text;

namespace PIBNAAPI.Command.Model
{
    public class DivisionModel
    {
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string DivisionShortName { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool IsHeightRequired { get; set; }
        public decimal MaxHeightRequired { get; set; }
        public int? AgeGroup { get; set; }
    }
}
