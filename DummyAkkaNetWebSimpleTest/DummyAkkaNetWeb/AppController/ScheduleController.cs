using DataAccess.Repository;
using DataModel.DB;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DummyAkkaNetWeb.AppController
{


    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScheduleController : Controller
    {

        private readonly ICoilRepo _coilRepo;

        public ScheduleController(ICoilRepo coilRepo)
        {
            _coilRepo = coilRepo;
        }


        [HttpGet]
        public async Task<ActionResult<List<CoilSchedule>>> GetCoilSchedule()
        {
            var items = await _coilRepo.GetCoilSchedule();

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }




        [HttpPost]
        public async Task<ActionResult<CoilSchedule>> NewCoilSchedule(CoilSchedule coilSchedule)
        {
            var items = await _coilRepo.NewCoilShcedule(coilSchedule);
            return CreatedAtAction(nameof(GetCoilSchedule), new { id = coilSchedule.Id }, coilSchedule);
        }


        [HttpGet]
        public async Task<ActionResult<CoilSchedule>> GetCoilScheduleById(string id)
        {
            var items = await _coilRepo.GetCoilScheduleById(id);

            if (items == null)
            {
                return NotFound();
            }
            return items;
        }


    }
}
