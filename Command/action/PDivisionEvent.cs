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
        public async Task<List<DivisionModel>> GetList(IMapper _mapper, PIBNAContext _context)
        {
            return _mapper.Map<List<DivisionModel>>(await _context.PDivision
                          .Where(p => p.EndDate == null)
                          .OrderBy(p => p.DivisionName)
                          .Select(p => p).ToListAsync());
        }

        public async Task<DivisionModel> GetById(int id, IMapper _mapper, PIBNAContext _context)
        {
            return  await _context.PDivision
                          .Where(p=> p.EndDate == null && p.DivisionId == id)
                          .Select(p => _mapper.Map<DivisionModel>(p)).FirstOrDefaultAsync();

        }

        public async Task PostDivision(DivisionModel model, IMapper _mapper, PIBNAContext _context)
        {
            DivisionModel data = model;
            try
            {


                var dta = await (from p in _context.PDivision
                                 where p.EndDate == null && p.DivisionId == data.DivisionId
                                 select p).FirstOrDefaultAsync();

                if (dta == null)
                {
                    PDivision p = new PDivision();
                    p.DivisionName = data.DivisionName;
                    p.DivisionShortName = data.DivisionShortName;
                    p.MinAge = data.MinAge;
                    p.MaxAge = data.MaxAge;
                    p.FromDate = DateTime.Now;
                    p.MaxHeightRequired = data.MaxHeightRequired;
                    p.AgeGroup = data.AgeGroup;
                    _context.PDivision.Add(p);
                    data.DivisionId = p.DivisionId;
                }
                else
                {
                    dta.DivisionName = data.DivisionName;
                    dta.DivisionShortName = data.DivisionShortName;
                    dta.MinAge = data.MinAge;
                    dta.MaxAge = data.MaxAge;
                    dta.FromDate = DateTime.Now;
                    dta.MaxHeightRequired = data.MaxHeightRequired;
                    dta.AgeGroup = data.AgeGroup;
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
