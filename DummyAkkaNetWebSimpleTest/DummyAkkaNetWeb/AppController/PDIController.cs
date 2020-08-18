using DataAccess.Repository;
using DataModel.DB;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DummyAkkaNetWeb.AppController
{


    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PDIController : Controller
    {

        private readonly ICoilRepo _coilRepo;

        public PDIController(ICoilRepo coilRepo)
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
    }
}
