using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AutoMapper;
using PIBNAAPI.Command.Interface;

namespace PIBNAAPI.Command.action
{
    public class HostCityEvent : IHostCityEvent
    {
        public async Task<HostCityModel> GetHostCity(IMapper _mapper, PIBNAContext _context)
        {
            HostCityModel data = new HostCityModel();

            var dta = await (from p in _context.PClubHost
                             where p.EndDate == null && p.SeasonId == DateTime.Now.Year
                             select p).FirstOrDefaultAsync();

            if (dta != null)
            {
                data.ClubId = dta.ClubId;
                data.SeasonId = dta.SeasonId;
                data.ClubName = _context.PClub.Where(s => s.ClubId == dta.ClubId).Select(s => s.ClubName).FirstOrDefault();
            }
            else
            {
                data.ClubId = 0;
                data.SeasonId = DateTime.Now.Year;
                data.ClubName = "";
            }

            return data;
        }

        public async Task<List<HostCityModel>> GetHostCityList(IMapper _mapper, PIBNAContext _context)
        {
            List<HostCityModel> data = new List<HostCityModel>();

            var dta = await (from p in _context.PClubHost
                             where p.EndDate == null
                             orderby p.SeasonId descending
                             select p).ToListAsync();

            foreach (var r in dta)
            {
                HostCityModel idata = new HostCityModel();
                idata.ClubId = r.ClubId;
                idata.SeasonId = r.SeasonId;
                idata.ClubName = _context.PClub.Where(s => s.ClubId == r.ClubId).Select(s => s.ClubName).FirstOrDefault();
                data.Add(idata);
            }



            return data;
        }
    }
}
