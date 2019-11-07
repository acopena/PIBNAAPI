using AutoMapper;
using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.Interface
{
    public interface IPUserEvent
    {
        Task<UserInfoPageModel> GetList(string searchKey, string sortKey, int clubId, int roleId, int pageSize, int page, PIBNAContext context);

        Task<UserInfoModel> GetAuthorized(string UserName, string password, IMapper mapper, PIBNAContext context);

        Task<UserInfoModel> GetUserById(int id, IMapper mapper, PIBNAContext context);

        Task<UserInfoModel> GetUserEmail(string email, IMapper mapper, PIBNAContext context);

        Task<List<PUserType>> GetUserRuleList(PIBNAContext context);

        Task<UserInfoModel> GetUserNewRole(int id, int newroleid, IMapper mapper, PIBNAContext context);
        Task SaveUser(UserUrlModel model, IMapper mapper, PIBNAContext context);
        Task SaveNewUser(UserUrlModel model, IMapper mapper, PIBNAContext context);
        Task<UserInfoModel> SaveRegisterUser(UserRegisterModel value, IMapper mapper, PIBNAContext context);
    }
}
