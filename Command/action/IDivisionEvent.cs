using PIBNAAPI.Command.Model;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.action
{
    public interface IDivisionEvent
    {
        Task<List<PDivision>> GetList();
        Task<PDivision> GetById(int id);
        void PostDivision(DivisionModel model);

    }
}
