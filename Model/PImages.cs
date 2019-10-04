using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PImages
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public long ImageSize { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ClubId { get; set; }
        public int SeasonId { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public virtual PClub Club { get; set; }
    }
}
