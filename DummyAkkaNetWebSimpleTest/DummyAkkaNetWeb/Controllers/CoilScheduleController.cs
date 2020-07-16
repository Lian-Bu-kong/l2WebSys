using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repository;
using DataModel.DB;
using DummyAkkaNetWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DummyAkkaNetWeb.Controllers
{
    public class CoilScheduleController : Controller
    {
        private readonly ICoilRepo _coilRepository;

        public CoilScheduleController(ICoilRepo coilRepository)
        {
            _coilRepository = coilRepository;
        }

        public IActionResult Index()
        {
            //var items = _coilRepository.GetAllCoilSchedule().OrderBy(x => x.SortNo);

            ////var d = JsonConvert.SerializeObject(items);

            //var coilScheduleViewModel = new CoilScheduleViewModel
            //{
            //    CoilSchedules = items,
            //    CoilScheduleTotal = items.Count()
            //};

            //return View(coilScheduleViewModel);

            return View(new CoilScheduleViewModel());
        }

        public IActionResult Schedule()
        {
            var items = _coilRepository.GetAllCoilSchedule().OrderBy(x => x.SortNo);
            var coilScheduleViewModel = new CoilScheduleViewModel
            {
                CoilSchedules = items,
                CoilScheduleTotal = items.Count()
            };

            return PartialView("_Schedule", coilScheduleViewModel);
        }

        [HttpPost]
        public IActionResult Update(string jsonStr)
        {
            var list = JsonConvert.DeserializeObject<IList<CoilSchedule>>(jsonStr);

            _coilRepository.SaveAllCoilSchedule(list);

            return Json("updated");
        }
    }
}
