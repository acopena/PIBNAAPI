using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PContactUs
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string SentTo { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? ResponseDate { get; set; }
    }
}
