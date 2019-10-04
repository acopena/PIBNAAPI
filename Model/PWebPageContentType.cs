using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PWebPageContentType
    {
        public PWebPageContentType()
        {
            PWebContent = new HashSet<PWebContent>();
        }

        public int WebPageContentTypeId { get; set; }
        public string WebPageContentTypeName { get; set; }

        public virtual ICollection<PWebContent> PWebContent { get; set; }
    }
}
