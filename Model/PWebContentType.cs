using System;
using System.Collections.Generic;

namespace PIBNAAPI.Model
{
    public partial class PWebContentType
    {
        public PWebContentType()
        {
            PWebContent = new HashSet<PWebContent>();
        }

        public int WebContentTypeId { get; set; }
        public string WebContentTypeName { get; set; }

        public virtual ICollection<PWebContent> PWebContent { get; set; }
    }
}
