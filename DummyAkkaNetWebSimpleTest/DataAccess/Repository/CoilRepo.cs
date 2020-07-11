using DataModel.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository
{
    public class CoilRepo : ICoilRepo
    {
        private readonly ApplicationDbContext _appDbContext;
        public CoilRepo(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<CoilSchedule> GetAllCoilSchedule()
        {
            return _appDbContext.CoilSchedules;
        }

    }
}
