using DataModel.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CoilDummyRepo : ICoilRepo
    {
        private IEnumerable<CoilSchedule> _coilSchedules = new List<CoilSchedule>()
        {
            new CoilSchedule{Id="CM201230010000", SeqNo = 1, SortNo = 4, UpdateSource = "1", CreateTime = new DateTime() },
            new CoilSchedule{Id="CM201230020000", SeqNo = 2, SortNo = 2, UpdateSource = "1", CreateTime = new DateTime() },
            new CoilSchedule{Id="CM201230030000", SeqNo = 3, SortNo = 1, UpdateSource = "1", CreateTime = new DateTime() },
            new CoilSchedule{Id="CM201230040000", SeqNo = 4, SortNo = 3, UpdateSource = "1", CreateTime = new DateTime() },
            new CoilSchedule{Id="CM201230050000", SeqNo = 5, SortNo = 5, UpdateSource = "1", CreateTime = new DateTime() },
        };

        public IEnumerable<CoilSchedule> GetAllCoilSchedule()
        {
            return _coilSchedules;
        }

        [HttpGet("{id}")]
        public Task<List<CoilSchedule>> GetCoilSchedule()
        {
            return Task.FromResult(_coilSchedules.ToList());
        }

        public Task<CoilSchedule> GetCoilScheduleById(string id)
        {
            return Task.FromResult(_coilSchedules.Where(x => x.Id.Equals(id)).FirstOrDefault());
        }

        public Task<CoilSchedule> NewCoilShcedule(CoilSchedule schedule)
        {
            _coilSchedules.ToList().Add(schedule);
            return Task.FromResult(_coilSchedules.Where(x => x.Id.Equals(schedule.Id)).FirstOrDefault());
        }

        public bool SaveAllCoilSchedule(IList<CoilSchedule> list)
        {
            throw new NotImplementedException();
        }
    }
}
