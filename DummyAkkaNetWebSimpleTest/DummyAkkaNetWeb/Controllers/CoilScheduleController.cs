using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repository;
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
            var items = _coilRepository.GetAllCoilSchedule();

            //var d = JsonConvert.SerializeObject(items);

            var coilScheduleViewModel = new CoilScheduleViewModel
            {
                CoilSchedules = items,
                CoilScheduleTotal = items.Count()
            };

            return View(coilScheduleViewModel);
        }
    }
}
