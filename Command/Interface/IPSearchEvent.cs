using AutoMapper;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.Interface
{
    public interface IPSearchEvent
    {
        Task<List<SearchPibnaModel>> GetSearch(string search, IMapper _mapper, PIBNAContext _context);
    }
}
