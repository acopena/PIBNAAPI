using PIBNAAPI.Command.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.action
{
    public interface IPSearchEvent
    {
        Task<List<SearchPibnaModel>> GetSearch(string search);
    }
}
