using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PIBNAAPI.Command.Interface;

namespace PIBNAAPI.Command.action
{
    public class PSearchEvent : IPSearchEvent
    {
        public async Task<List<SearchPibnaModel>> GetSearch(string search, IMapper _mapper, PIBNAContext _context)
        {
            List<SearchPibnaModel> model = new List<SearchPibnaModel>();
            using (var ctx = new PIBNAContext())
            {
                var data = await (from p in ctx.PMember
                                  where (p.FirstName.Contains(search) || p.LastName.Contains(search))
                                  select p).ToListAsync();
                foreach (var r in data)
                {
                    SearchPibnaModel m = new SearchPibnaModel();
                    m.Id = r.MemberId;
                    m.Name = r.FirstName + ' ' + r.MiddleName + ' ' + r.LastName;
                    m.Type = "Player";
                    m.ClubName = ctx.PClub.Where(s => s.ClubId == r.ClubId).Select(s => s.ClubName).FirstOrDefault();
                    m.Notes = "Current Age:" + (DateTime.Now.Year - r.DateOfBirth.Year).ToString() + "yrs old";
                    m.Notes += " | Date Registered:" + r.FromDate.ToShortDateString();
                    var team = ctx.PTeamRoster.Where(s => s.MemberId == r.MemberId).Select(s => s).OrderByDescending(s => s.FromDate).FirstOrDefault();
                    if (team != null)
                    {
                        var info = ctx.PTeam.Where(s => s.TeamId == team.TeamId).Select(s => s).FirstOrDefault();
                        m.Notes += " | Season:" + info.SeasonId;
                        m.Notes += " | Team : " + info.TeamName;
                    }
                    model.Add(m);
                }

                var officials = await (from p in ctx.PClubOfficial
                                       where p.EndDate == null
                                       && p.Name.Contains(search)
                                       select p).ToListAsync();
                foreach (var r in officials)
                {
                    SearchPibnaModel m = new SearchPibnaModel();
                    m.Id = r.ClubId;
                    m.Name = r.Name;
                    m.Type = "Club Official";
                    m.ClubName = ctx.PClub.Where(s => s.ClubId == r.ClubId).Select(s => s.ClubName).FirstOrDefault();
                    m.Notes = "Email :" + r.Email;
                    m.Notes += " | Phone :" + r.Phone;
                    m.Notes += " | Position :" + r.PositionId;
                    model.Add(m);
                }

                var Coach = await (from p in ctx.PTeamOfficial
                                   where p.EndDate == null
                                   && p.Name.Contains(search)
                                   select p).ToListAsync();
                foreach (var r in Coach)
                {
                    SearchPibnaModel m = new SearchPibnaModel();
                    m.Id = r.TeamId;
                    m.Name = r.Name;
                    m.Type = "Coach";

                    var info = ctx.PTeam.Where(s => s.TeamId == r.TeamId).Select(s => s).OrderByDescending(s => s.FromDate).FirstOrDefault();

                    m.ClubName = ctx.PClub.Where(s => s.ClubId == info.ClubId).Select(s => s.ClubName).FirstOrDefault();
                    m.Notes = "Email :" + r.Email;
                    m.Notes += " | Phone :" + r.Phone;
                    m.Notes += " | Position :" + r.PositionId;
                    m.Notes += " | Season:" + info.SeasonId;
                    m.Notes += " | Team : " + info.TeamName;
                    model.Add(m);
                }


                var club = await (from p in ctx.PClub
                                  where p.EndDate == null
                                  && p.ClubName.Contains(search)
                                  select p).ToListAsync();
                foreach (var r in club)
                {
                    SearchPibnaModel m = new SearchPibnaModel();
                    m.Id = r.ClubId;
                    m.Name = r.ClubName;
                    m.Type = "Club";
                    m.ClubName = r.ClubName;
                    m.Notes = "City:" + r.City + " | State: " + r.State;
                    model.Add(m);
                }

                var user = await (from p in ctx.PUser
                                  where p.EndDate == null
                                  && p.FirstName.Contains(search) || p.LastName.Contains(search)
                                  select p).ToListAsync();
                foreach (var r in user)
                {
                    SearchPibnaModel m = new SearchPibnaModel();
                    m.Id = r.UserId;
                    m.Name = r.FirstName + ' ' + r.LastName;
                    m.Type = "User";
                    m.ClubName = ctx.PClub.Where(s => s.ClubId == r.ClubId).Select(s => s.ClubName).FirstOrDefault();
                    m.Notes = "Email:" + r.UserName;
                    model.Add(m);
                }


            }
            return model;
        }
    }
}
