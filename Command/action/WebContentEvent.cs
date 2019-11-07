using PIBNAAPI.Command.Model;
using PIBNAAPI.Command.Query;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PIBNAAPI.Command.Interface;

namespace PIBNAAPI.Command.action
{
    public class WebContentEvent : IWebContentEvent
    {
        public async Task<WebContentPageModel> GetList(int page, int pageSize, IMapper _mapper, PIBNAContext _context)
        {
            //List<WebContentModel> model = await GetWebContent.GetWebContentList(page, pageSize);
            var model = new WebContentPageModel();
            var content = await GetWebContent.GetWebContentList(page, pageSize, _context);

            model.RecordCount = content.TotalRecord;
            model.data = content;

            return model;
        }

      
        public async Task<List<WebContentModel>> GetWebContentListByPage(int id, IMapper _mapper, PIBNAContext _context)
        {
            List<WebContentModel> model = await GetWebContent.GetWebContentListByPage(id, _context);
            return model;
        }
        public async Task<List<PWebContentType>> GetWebContentTypeList(IMapper _mapper, PIBNAContext _context)
        {
            List<PWebContentType> model = await GetWebContent.GetWebContentTypeList(_context);
            return model;
        }
        public async Task<WebContentModel> GetContentById(int Id, IMapper _mapper, PIBNAContext _context)
        {
            WebContentModel model = await GetWebContent.GetWebContentById(Id,_context);
            return model;
        }
        public async Task PostWebContent(WebContentParamModel value, IMapper _mapper, PIBNAContext _context)
        {
            using (var ctx = new PIBNAContext())
            {
                var dta = await (from p in ctx.PWebContent
                                 where p.EndDate == null && p.WebContentId == value.WebContentId
                                 select p).FirstOrDefaultAsync();

                if (dta == null)
                {
                    PWebContent data = _mapper.Map<PWebContent>(value);
                    value.WebContentId = data.WebContentId;
                    data.FromDate = DateTime.Now;
                    data.IsExpire = false;
                    data.WebPageContentId = 1;
                    data.WebContentTypeId = value.WebContentTypeId;
                    await ctx.PWebContent.AddAsync(data);
                    await ctx.SaveChangesAsync();
                    value.WebContentId = data.WebContentId;
                }
                else
                {
                    var idate = DateTime.Parse(value.PublishEndDate);
                    dta.WebTitle = value.WebTitle;
                    dta.WebContent = value.WebContent;
                    dta.WebContentTypeId = value.WebContentTypeId;
                    dta.PublishedEndDate = DateTime.Parse(value.PublishEndDate);
                    dta.PublishStartDate = DateTime.Parse(value.PublishStartDate);
                    dta.PostedById = value.PostedById;
                    await ctx.SaveChangesAsync();
                }
            }
        }
    }
}
