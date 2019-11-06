using AutoMapper;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.action
{
    public interface IWebContentEvent
    {
        Task<WebContentPageModel> GetList(int page, int pageSize);
        Task<List<WebContentModel>> GetWebContentListByPage(int id);
        Task<List<PWebContentType>> GetWebContentTypeList();
        Task<WebContentModel> GetContentById(int Id);
        Task PostWebContent(WebContentParamModel value, IMapper _mapper);
    }
}
