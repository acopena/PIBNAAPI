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
        Task<List<PDivisionModel>> GetList(IMapper _mapper, PIBNAContext _context);
        Task<PDivisionModel> GetById(int id, IMapper _mapper, PIBNAContext _context);
        Task PostDivision(PDivisionModel model, IMapper _mapper, PIBNAContext _context);

    }
}
