using DataModel.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool SaveAllCoilSchedule(IList<CoilSchedule> list)
        {
            return TryFun(
                () => {
                    list.ToList().ForEach(data => {
                        var updateData = _appDbContext.CoilSchedules.Single(x => x.Id == data.Id);

                        _appDbContext.Entry(updateData).CurrentValues.SetValues(data);
                        _appDbContext.Entry(updateData).Property(x => x.UpdateSource).IsModified = false;
                        _appDbContext.Entry(updateData).Property(x => x.CreateTime).IsModified = false;
                        _appDbContext.SaveChanges();
                    });

                    return true;
                }
            );
        }

        private T TryFun<T>(Func<T> func)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ex Message : {ex.Message}");
                Console.WriteLine($"Ex StackTrace : {ex.StackTrace}");

                return default;
            }
        }
    }
}
