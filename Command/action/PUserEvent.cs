using PIBNAAPI.Command.Interface;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;

namespace PIBNAAPI.Command.action
{
    public class PUserEvent : IPUserEvent
    {
        public async Task<UserInfoPageModel> GetList(string searchKey, string sortKey, int clubId, int roleId, int pageSize, int page, PIBNAContext context)
        {
            var model = new UserInfoPageModel();
            List<UserInfoModel> data = new List<UserInfoModel>();


            var users = context.PUser
                .Include(s => s.Club)
                .Where(s => s.EndDate == null)
                .Select(s => new UserInfoModel()
                {
                    UserId = s.UserId,
                    UserName = s.UserName,
                    LastName = s.LastName,
                    FirstName = s.FirstName,
                    Name = s.FirstName + ' ' + s.LastName,

                    ClubId = s.ClubId,
                    ClubName = context.PClub.Where(c => c.ClubId == s.ClubId).Select(c => c.ClubName).FirstOrDefault(),
                    UserRoleList = new List<UserRoleModel>(),
                    IsHostCity = false,

                });

            if (!String.IsNullOrEmpty(searchKey))
            {
                users = users.Where(s => s.UserName.Contains(searchKey) || s.FirstName.Contains(searchKey) || s.LastName.Contains(searchKey) || s.ClubName.Contains(searchKey));
            }
            if (roleId == 3)
            {
                users = users.Where(s => s.ClubId == clubId);
            }

            var paginatedList = await PaginatedList<UserInfoModel>.CreateAsync(users.AsNoTracking(), page, pageSize);
            var userTypeList = context.PUserType.Select(s => s).ToList();
            int seasonId = DateTime.Now.Year;
            var iList = paginatedList;

            //foreach(var i in paginatedList)
            //{
            //    var Host = ctx.PClubHost.Where(s => s.SeasonId == seasonId && s.ClubId == i.ClubId).Select(s => s).FirstOrDefault();
            //    if (Host != null)
            //    {
            //        i.IsHostCity = true;
            //    }

            //    i.HostCity = HostCityController.GetHostCity();

            //    foreach (var r in userTypeList)
            //    {
            //        UserRoleModel rm = new UserRoleModel();

            //        var role = ctx.PUserRole.Where(s => s.UserId == i.UserId && s.UserTypeId == r.UserTypeId).FirstOrDefault();
            //        if (role != null)
            //        {
            //            if (role.IsActive.Value)
            //            {
            //                i.CurrentRole = r.Description;
            //                i.CurrentRoleId = role.UserTypeId;
            //            }
            //            rm.UserRoleId = role.UserRoleId;
            //            rm.IsRole = role.EndDate == null ? true : false;
            //        }
            //        else
            //        {
            //            rm.IsRole = false;
            //        }
            //        rm.UserTypeId = r.UserTypeId;
            //        rm.UserTypeName = r.Description;
            //        i.UserRoleList.Add(rm);
            //    }
            //}

            model.RecordCount = paginatedList.TotalRecord;
            model.data = paginatedList;

            return model;
        }

        public async Task<UserInfoModel> GetAuthorized(string UserName, string password, IMapper mapper, PIBNAContext context)
        {
            UserInfoModel model = new UserInfoModel();

            var data = await (from p in context.PUser
                              where p.EndDate == null
                              && p.UserName == UserName && p.AccessCode == password
                              select p).FirstOrDefaultAsync();
            if (data != null)
            {
                model = await GetUserInfo(data, mapper, context);
                model.UserRoleList = model.UserRoleList.Where(s => s.IsRole == true).Select(s => s).ToList();
            }
            else
            {
                model.UserId = 0;
                model.UserName = string.Empty;
                model.UserRoleList = null;
            }

            return model;
        }


        public async Task<UserInfoModel> GetUserById(int id, IMapper mapper, PIBNAContext context)
        {

            UserInfoModel model = new UserInfoModel();

            var data = await (from p in context.PUser
                              where p.EndDate == null
                              && p.UserId == id
                              select p).FirstOrDefaultAsync();
            if (model != null)
            {
                model = await GetUserInfo(data, mapper, context);

            }
            else
            {
                model.UserId = 0;
                model.UserName = string.Empty;

            }

            return model;
        }

        public async Task<UserInfoModel> GetUserEmail(string email, IMapper mapper, PIBNAContext context)
        {
            UserInfoModel model = new UserInfoModel();

            var data = await (from p in context.PUser
                              where p.EndDate == null
                              && p.UserName == email
                              select p).FirstOrDefaultAsync();
            if (model != null)
            {
                model = await GetUserInfo(data, mapper, context);
            }
            return model;
        }

        public async Task<List<PUserType>> GetUserRuleList(PIBNAContext context)
        {
            return await (from p in context.PUserType
                          select p).ToListAsync();
        }

        public async Task<UserInfoModel> GetUserNewRole(int id, int newroleid, IMapper mapper, PIBNAContext context)
        {
            UserInfoModel model = new UserInfoModel();

            var irole = await (from p in context.PUserRole
                               where p.EndDate == null && p.UserId == id && p.IsActive.Value == true && p.UserTypeId != newroleid
                               select p).FirstOrDefaultAsync();
            if (irole != null)
            {
                irole.IsActive = false;
                context.SaveChanges();
            }

            var inewrole = await (from p in context.PUserRole
                                  where p.EndDate == null && p.UserId == id && p.UserTypeId == newroleid && p.IsActive.Value == false
                                  select p).FirstOrDefaultAsync();
            if (inewrole != null)
            {
                inewrole.IsActive = true;
                context.SaveChanges();
            }

            model = await GetUserById(id, mapper, context);
            return model;
        }


        public async Task SaveUser(UserUrlModel model, IMapper mapper, PIBNAContext context)
        {
            UserInfoModel data = new UserInfoModel();
            List<UserRoleModel> _UserRoleList = JsonConvert.DeserializeObject<List<UserRoleModel>>(model.UserRoleList);

            var dta = await (from p in context.PUser
                             where p.EndDate == null && p.UserId == model.UserId
                             select p).FirstOrDefaultAsync();

            if (dta == null)
            {
                PUser p = new PUser();
                p.UserName = model.UserName;
                p.FirstName = model.FirstName;
                p.LastName = model.LastName;
                p.IsBlockAccount = model.IsBlock;
                p.ClubId = model.ClubId;
                p.FromDate = DateTime.Now;
                await context.PUser.AddAsync(p);
                await context.SaveChangesAsync();
                model.UserId = p.UserId;
            }
            else
            {
                dta.UserName = model.UserName;
                dta.FirstName = model.FirstName;
                dta.LastName = model.LastName;
                dta.IsBlockAccount = model.IsBlock;
                dta.ClubId = model.ClubId;
                await context.SaveChangesAsync();
            }

            var CityDirector = await context.PClubOfficial
                .Where(s => s.Email == model.UserName && s.EndDate == null)
                .Select(s => s).FirstOrDefaultAsync();

            if (CityDirector != null)
            {
                PUserRole u = new PUserRole();
                u.UserId = model.UserId;
                u.UserTypeId = 3; // for city director only;
                u.IsActive = false;
                u.FromDate = DateTime.Now;
                await context.PUserRole.AddAsync(u);
            }

            foreach (var r in _UserRoleList)
            {
                var uData = context.PUserRole.Where(s => s.UserId == model.UserId && s.UserTypeId == r.UserTypeId).Select(s => s).FirstOrDefault();
                if (uData == null)
                {
                    if (r.IsRole)
                    {
                        PUserRole u = new PUserRole();
                        u.UserId = model.UserId;
                        u.UserTypeId = r.UserTypeId;
                        u.IsActive = false;
                        u.FromDate = DateTime.Now;
                        await context.PUserRole.AddAsync(u);
                    }
                }
                else
                {
                    if (r.IsRole)
                    {
                        uData.EndDate = null;
                    }
                    else
                    {
                        uData.EndDate = DateTime.Now;
                    }
                }
            }
            await context.SaveChangesAsync();
        }


        public async Task SaveNewUser(UserUrlModel value, IMapper mapper, PIBNAContext context)
        {
            UserInfoModel data = new UserInfoModel();
            List<UserRoleModel> _UserRoleList = JsonConvert.DeserializeObject<List<UserRoleModel>>(value.UserRoleList);

            var dta = await (from p in context.PUser
                             where p.EndDate == null && p.UserName == value.UserName
                             select p).FirstOrDefaultAsync();

            if (dta == null)
            {
                PUser p = new PUser();
                p.UserName = value.UserName;
                p.FirstName = value.FirstName;
                p.LastName = value.LastName;
                p.IsBlockAccount = false;
                p.ClubId = value.ClubId;
                p.FromDate = DateTime.Now;

                await context.PUser.AddAsync(p);
                await context.SaveChangesAsync();
                value.UserId = p.UserId;

            }
            else
            {
                dta.UserName = value.UserName;
                dta.FirstName = value.FirstName;
                dta.LastName = value.LastName;
                dta.IsBlockAccount = value.IsBlock;
                dta.ClubId = value.ClubId;
                await context.SaveChangesAsync();
            }

            foreach (var r in _UserRoleList)
            {
                var uData = await context.PUserRole.Where(s => s.UserId == value.UserId && s.UserTypeId == r.UserTypeId).Select(s => s).FirstOrDefaultAsync();
                if (uData == null)
                {
                    if (r.IsRole)
                    {
                        PUserRole u = new PUserRole();
                        u.UserId = value.UserId;
                        u.UserTypeId = r.UserTypeId;
                        u.IsActive = false;
                        u.FromDate = DateTime.Now;
                        await context.PUserRole.AddAsync(u);
                    }
                }
                else
                {
                    if (r.IsRole)
                    {
                        uData.EndDate = null;
                    }
                    else
                    {
                        uData.EndDate = DateTime.Now;
                    }
                }
            }
            await context.SaveChangesAsync();

        }

        public async Task<UserInfoModel> SaveRegisterUser(UserRegisterModel value, IMapper mapper, PIBNAContext context)
        {
            UserInfoModel model = new UserInfoModel();
            int userId = 0;

            var dta = await (from p in context.PUser
                             where p.UserName.ToLower().Trim() == value.UserName.ToLower().Trim() && p.EndDate == null
                             select p).FirstOrDefaultAsync();


            if (dta == null)
            {
                PUser p = new PUser();
                p.UserName = value.UserName;
                p.FirstName = value.FirstName;
                p.LastName = value.LastName;
                p.IsBlockAccount = false;
                p.ClubId = value.ClubId;
                p.AccessCode = value.Password;
                p.FromDate = DateTime.Now;
                await context.PUser.AddAsync(p);
                await context.SaveChangesAsync();
                userId = p.UserId;



                var CityDirector = await context.PClubOfficial
                .Where(s => s.Email == value.UserName && s.EndDate == null)
                .Select(s => s).FirstOrDefaultAsync();
                bool isActive = true;

                if (CityDirector != null)
                {
                    PUserRole u = new PUserRole();
                    u.UserId = userId;
                    u.UserTypeId = 3; // for city director only;
                    u.IsActive = isActive;
                    u.FromDate = DateTime.Now;
                    await context.PUserRole.AddAsync(u);
                    isActive = false;
                }

                if (value.Coach)
                {
                    PUserRole r = new PUserRole();
                    r.UserId = userId;
                    r.UserTypeId = 4; //coach
                    r.IsActive = isActive;
                    r.FromDate = DateTime.Now;
                    isActive = false;
                    await context.PUserRole.AddAsync(r);
                }
                if (value.Player)
                {
                    PUserRole r = new PUserRole();
                    r.UserId = userId;
                    r.UserTypeId = 5; //Parent
                    r.IsActive = isActive;
                    r.FromDate = DateTime.Now;
                    isActive = false;
                    await context.PUserRole.AddAsync(r);
                }
                if (value.Parent)
                {
                    PUserRole r = new PUserRole();
                    r.UserId = userId;
                    r.UserTypeId = 6; //Parent
                    r.IsActive = isActive;
                    r.FromDate = DateTime.Now;
                    isActive = false;
                    await context.PUserRole.AddAsync(r);
                }
            }
            context.SaveChanges();

            model = await GetUserById(dta.UserId, mapper, context);
            var activeRole = model.UserRoleList.Where(s => s.IsRole == true).ToList();
            model.UserRoleList = activeRole;
            return model;

        }

        async Task<UserInfoModel> GetUserInfo(PUser data, IMapper mapper, PIBNAContext ctx)
        {
            UserInfoModel model = new UserInfoModel();
            model.UserId = data.UserId;
            model.UserName = data.UserName;
            model.LastName = data.LastName;
            model.FirstName = data.FirstName;
            model.Name = data.FirstName + ' ' + data.LastName;

            model.ClubId = data.ClubId;
            model.ClubName = await ctx.PClub.Where(s => s.ClubId == data.ClubId).Select(s => s.ClubName).FirstOrDefaultAsync();
            model.UserRoleList = new List<UserRoleModel>();
            model.IsHostCity = false;

            var userTypeList = ctx.PUserType.Select(s => s).ToList();

            int seasonId = DateTime.Now.Year;
            //Check if the user is the current host city
            var Host = await ctx.PClubHost.Where(s => s.SeasonId == seasonId && s.ClubId == model.ClubId).Select(s => s).FirstOrDefaultAsync();
            if (Host != null)
            {
                model.IsHostCity = true;
            }

            IHostCityEvent hostCityEvent = new HostCityEvent();
            model.HostCity = await hostCityEvent.GetHostCity(mapper, ctx);

            foreach (var r in userTypeList)
            {
                UserRoleModel rm = new UserRoleModel();

                var role = await ctx.PUserRole.Where(s => s.UserId == data.UserId && s.UserTypeId == r.UserTypeId).FirstOrDefaultAsync();
                if (role != null)
                {
                    if (role.IsActive.Value)
                    {
                        model.CurrentRole = r.Description;
                        model.CurrentRoleId = role.UserTypeId;
                    }

                    rm.UserRoleId = role.UserRoleId;
                    if (role.EndDate == null)
                    {
                        rm.IsRole = true;

                    }
                    else
                    {
                        rm.IsRole = false;
                    }
                }
                else
                {
                    rm.IsRole = false;
                }
                rm.UserTypeId = r.UserTypeId;
                rm.UserTypeName = r.Description;
                model.UserRoleList.Add(rm);
            }
            return model;
        }
    }
}
