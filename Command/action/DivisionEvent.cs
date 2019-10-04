using PIBNAAPI.Command.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PIBNAAPI.Model;
using System;
using System.Linq;

namespace PIBNAAPI.Command.action
{
    public class DivisionEvent: IDivisionEvent
    {
        public async Task<List<PDivision>> GetList()
        {
            List<PDivision> data = new List<PDivision>();
            using (var ctx = new PIBNAContext())
            {
                data = await (from p in ctx.PDivision
                              where p.EndDate == null
                              orderby p.DivisionName
                              select p).ToListAsync();

            }
            return data;
        }

        public async Task<PDivision> GetById(int id)
        {
            PDivision data = new PDivision();
            using (var ctx = new PIBNAContext())
            {
                
                data = await (from p in ctx.PDivision
                              where p.EndDate == null && p.DivisionId == id
                              select p).FirstOrDefaultAsync();

            }
            return data;


        }

        public void PostDivision(DivisionModel model)
        {
            DivisionModel data = model;
            try
            {

                using (var ctx = new PIBNAContext())
                {
                    var dta = (from p in ctx.PDivision
                               where p.EndDate == null && p.DivisionId == data.DivisionId
                               select p).FirstOrDefault();

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
                        ctx.PDivision.Add(p);
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
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
    }
}
