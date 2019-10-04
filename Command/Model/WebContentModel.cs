using System;
using System.Collections.Generic;

namespace PIBNAAPI.Command.Model
{

    public class WebContentPageModel
    {
        public List<WebContentModel> data { get; set; }
        public int RecordCount { get; set; }

    }
    public class WebContentModel
    {
        public int WebContentId { get; set; }
        public string WebTitle { get; set; }
        public string WebContent { get; set; }
        public int PostedById { get; set; }
        public DateTime PublishStartDate { get; set; }
        public DateTime PublishedEndDate { get; set; }
        public int WebContentTypeId { get; set; }
        public string webContentTypeName { get; set; }
    }

    public class WebContentParamModel
    {
        public int WebContentId { get; set; }
        public string WebTitle { get; set; }
        public string WebContent { get; set; }
        public int PostedById { get; set; }
        public string PublishStartDate{ get; set; }
        public string PublishEndDate { get; set; }
        //public int PublishStartYear { get; set; }
        //public int PublishStartMonth{ get; set; }
        //public int PublishStartDay { get; set; }

        //public int PublishedEndYear { get; set; }
        //public int PublishedEndMonth{ get; set; }
        //public int PublishedEndDay{ get; set; }

        public int WebContentTypeId { get; set; }
    }
}

