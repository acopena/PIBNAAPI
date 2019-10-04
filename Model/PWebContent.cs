using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PWebContent
    {
        public int WebContentId { get; set; }
        public string WebTitle { get; set; }
        public string WebContent { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PostedById { get; set; }
        public DateTime PublishStartDate { get; set; }
        public DateTime? PublishedEndDate { get; set; }
        public bool IsExpire { get; set; }
        public int WebPageContentId { get; set; }
        public int WebContentTypeId { get; set; }
        public int Sortkey { get; set; }

        public virtual PUser PostedBy { get; set; }
        public virtual PWebContentType WebContentType { get; set; }
        public virtual PWebPageContentType WebContentTypeNavigation { get; set; }
    }
}
