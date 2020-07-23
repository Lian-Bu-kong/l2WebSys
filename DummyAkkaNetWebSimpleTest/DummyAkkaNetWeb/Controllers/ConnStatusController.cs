using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DummyAkkaNetWeb.Controllers
{
    public class ConnStatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
