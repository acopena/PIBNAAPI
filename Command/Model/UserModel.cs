using System;
using System.Collections.Generic;
using System.Text;

namespace PIBNAAPI.Command.Model
{
   
    public class UserInfoModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string ClubName { get; set; }
        public string Name { get; set; }
        public int ClubId { get; set; }
        public string CurrentRole { get; set; }
        public int CurrentRoleId { get; set; }
        public Boolean IsBlock { get; set; }
        public List<UserRoleModel> UserRoleList { get; set; }
        public Boolean IsHostCity { get; set; }
        public HostCityModel HostCity { get; set; }
    }

    public class UserInfoPageModel
    {
        public List<UserInfoModel> data { get; set; }
        public int RecordCount { get; set; }
    }


    public class UserUrlModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string ClubName { get; set; }
        public int ClubId { get; set; }
        public Boolean IsBlock { get; set; }
        public string UserRoleList { get; set; }
    }

    public class UserRegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ClubId { get; set; }
        public bool Coach { get; set; }
        public bool Player { get; set; }
        public bool Parent { get; set; }
    }

    public class UserRoleModel
    {
        public int UserRoleId { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public Boolean IsRole { get; set; }
    }

}
