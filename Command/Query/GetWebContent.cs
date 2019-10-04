using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;

namespace PIBNAAPI.Command.Query
{
    public class GetWebContent
    {
        public static async Task<PaginatedList<WebContentModel>> GetWebContentList(int page, int pageSize)
        {
            using (var ctx = new PIBNAContext())
            {
                var modelList = ctx.PWebContent
                    .Include(s => s.WebContentType)
                    .Where(s => s.EndDate == null)
                    .Select(s => new WebContentModel
                    {
                        PostedById = s.PostedById,
                        PublishedEndDate = s.PublishedEndDate.Value,
                        PublishStartDate = s.PublishStartDate,
                        WebContent = s.WebContent,
                        WebContentId = s.WebContentId,
                        WebContentTypeId = s.WebContentTypeId,
                        webContentTypeName = s.WebContentType.WebContentTypeName,
                        WebTitle = s.WebTitle

                    });
                return await PaginatedList<WebContentModel>.CreateAsync(modelList.AsNoTracking(), page, pageSize); 
            }
            
        }

        public static async Task<List<WebContentModel>> GetWebContentListByPage(int pageid)
        {
            List<WebContentModel> modelList = new List<WebContentModel>();
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month,DateTime.Now.Day,0,0,0);
            //&& s.PublishStartDate >= today && s.PublishedEndDate <= today
            try
            {


                using (var ctx = new PIBNAContext())
                {
                    modelList = await ctx.PWebContent
                        .Include(s => s.WebContentType)
                        .Where(s => s.EndDate == null && s.WebContentTypeId == pageid
                        && (s.PublishStartDate <= today && s.PublishedEndDate > today))
                        .Select(s => new WebContentModel
                        {
                            PostedById = s.PostedById,
                            PublishedEndDate = s.PublishedEndDate.Value,
                            PublishStartDate = s.PublishStartDate,
                            WebContent = s.WebContent,
                            WebContentId = s.WebContentId,
                            WebContentTypeId = s.WebContentTypeId,
                            webContentTypeName = s.WebContentType.WebContentTypeName,
                            WebTitle = s.WebTitle

                        }).ToListAsync();
                }
            }
            catch (Exception err)
            {
                var errmsg = err.Message;
            }
            return modelList;
        }

        public static async Task<WebContentModel> GetWebContentById(int id)
        {
            WebContentModel model = new WebContentModel();
            using (var ctx = new PIBNAContext())
            {
                model = await ctx.PWebContent
                    .Include(s => s.WebContentType)
                    .Where(s => s.EndDate == null && s.WebContentId == id)
                      .Select(s => new WebContentModel
                      {
                          PostedById = s.PostedById,
                          PublishedEndDate =  s.PublishedEndDate.Value,
                          PublishStartDate = s.PublishStartDate,
                          WebContent = s.WebContent,
                          WebContentId = s.WebContentId,
                          WebContentTypeId = s.WebContentTypeId,
                          webContentTypeName = s.WebContentType.WebContentTypeName,
                          WebTitle = s.WebTitle

                      }).FirstOrDefaultAsync();
            }
            return model;
        }

        public static async Task<List<PWebContentType>> GetWebContentTypeList()
        {
            List<PWebContentType> modelList = new List<PWebContentType>();
            using (var ctx = new PIBNAContext())
            {
                modelList = await ctx.PWebContentType
                    .Select(s => s).ToListAsync();
            }
            return modelList;
        }
    }
}
