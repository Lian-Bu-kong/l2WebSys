using DataModel.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICoilRepo
    {
        IEnumerable<CoilSchedule> GetAllCoilSchedule();

        bool SaveAllCoilSchedule(IList<CoilSchedule> list);


        // Web API使用
        Task<List<CoilSchedule>> GetCoilSchedule();
    }
}
