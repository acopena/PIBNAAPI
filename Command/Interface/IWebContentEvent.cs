using AutoMapper;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.Interface
{
    public interface IWebContentEvent
    {
        Task<WebContentPageModel> GetList(int page, int pageSize, IMapper _mapper, PIBNAContext _context);
        Task<List<WebContentModel>> GetWebContentListByPage(int id, IMapper _mapper, PIBNAContext _context);
        Task<List<PWebContentType>> GetWebContentTypeList(IMapper _mapper, PIBNAContext _context);
        Task<WebContentModel> GetContentById(int Id, IMapper _mapper, PIBNAContext _context);
        Task PostWebContent(WebContentParamModel value, IMapper _mapper, PIBNAContext _context);
    }
}
