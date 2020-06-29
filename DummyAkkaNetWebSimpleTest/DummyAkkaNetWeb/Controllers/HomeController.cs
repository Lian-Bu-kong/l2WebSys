using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DummyAkkaNetWeb.Models;
using DummyAkkaNetWeb.Actor;
using Akka.Actor;

namespace DummyAkkaNetWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ActorManager _actorManager;

        public HomeController(ILogger<HomeController> logger, ActorManager actorManager)
        {
            _logger = logger;
            _actorManager = actorManager;
        }

        public IActionResult Index()
        {
            _actorManager.CommActor.Tell("Web Home Click");
            return View();
            
        }

        public IActionResult Privacy()
        {
            _actorManager.CommActor.Tell("Web Privacy Click");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _actorManager.CommActor.Tell("Web Error Click");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
