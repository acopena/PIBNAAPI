using AutoMapper;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.Interface
{
    public interface IPDivisionEvent
    {
        Task<List<DivisionModel>> GetList(IMapper _mapper, PIBNAContext _context);
        Task<DivisionModel> GetById(int id, IMapper _mapper, PIBNAContext _context);
        Task PostDivision(DivisionModel model, IMapper _mapper, PIBNAContext _context);

    }
}
