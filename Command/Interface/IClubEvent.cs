﻿using PIBNAAPI.Command.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.action
{
    public interface IClubEvent
    {
        Task<TeamDashboardModel> GetTeamSummary(int? seasonId);
        Task<List<ClubModel>> GetList();
        Task<ClubPageModel> GetListByPage(int pageSize, int page);
        Task<ClubModel> GetById(int id);
        Task PostClub(ClubModel data);
        Task PostOfficialEnd(string id);
        Task DeleteOfficial(int id);
    }
}