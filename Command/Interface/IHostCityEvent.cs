using AutoMapper;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.Interface
{
    public interface IHostCityEvent
    {
        Task<HostCityModel> GetHostCity(IMapper _mapper, PIBNAContext _context);
        Task<List<HostCityModel>> GetHostCityList(IMapper _mapper, PIBNAContext _context);
    }
}
