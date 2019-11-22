using PIBNAAPI.Command.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PIBNAAPI.Model;
using System;
using System.Linq;
using AutoMapper;
using PIBNAAPI.Command.Interface;

namespace PIBNAAPI.Command.action
{
    public class PDivisionEvent : IPDivisionEvent
    {
        public async Task<List<PDivisionModel>> GetList(IMapper _mapper, PIBNAContext _context)
        {
            return _mapper.Map<List<PDivisionModel>>(await _context.PDivision
                          .Where(p => p.EndDate == null)
                          .OrderBy(p => p.DivisionName)
                          .Select(p => p).ToListAsync());
        }

        public async Task<PDivisionModel> GetById(int id, IMapper _mapper, PIBNAContext _context)
        {
            return  await _context.PDivision
                          .Where(p=> p.EndDate == null && p.DivisionId == id)
                          .Select(p => _mapper.Map<PDivisionModel>(p)).FirstOrDefaultAsync();

        }

        public async Task PostDivision(PDivisionModel model, IMapper _mapper, PIBNAContext _context)
        {
            PDivisionModel data = model;
            try
            {


                var dta = await (from p in _context.PDivision
                                 where p.EndDate == null && p.DivisionId == data.DivisionId
                                 select p).FirstOrDefaultAsync();

                if (dta == null)
                {
                    PDivision p =  new PDivision();
                    _mapper.Map(data, p);
                    p.FromDate = DateTime.Now;
                    _context.PDivision.Add(p);
                    data.DivisionId = p.DivisionId;
                }
                else
                {
                    _mapper.Map(data, dta);
                }
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
    }
}
