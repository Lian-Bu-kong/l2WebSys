using DataModel.DB;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface ICoilRepo
    {
        IEnumerable<CoilSchedule> GetAllCoilSchedule();
    }
}
