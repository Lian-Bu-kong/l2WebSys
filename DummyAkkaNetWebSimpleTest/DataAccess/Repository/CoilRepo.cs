using DataModel.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


        // Web API
        public Task<List<CoilSchedule>> GetCoilSchedule()
        {
            return _appDbContext.CoilSchedules.ToListAsync();
        }

        public async Task<CoilSchedule> NewCoilShcedule(CoilSchedule schedule)
        {
            _appDbContext.CoilSchedules.Add(schedule);
            await _appDbContext.SaveChangesAsync();
            return _appDbContext.CoilSchedules.Find(schedule.Id);
        }

        [HttpGet("{id}")]
        public Task<CoilSchedule> GetCoilScheduleById(string id)
        {
            return _appDbContext.CoilSchedules.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
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
