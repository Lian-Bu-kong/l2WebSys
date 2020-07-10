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
using Microsoft.AspNetCore.SignalR;
using DummyAkkaNetWeb.Hubs;
using AkkaBase;

namespace DummyAkkaNetWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<ChatHub> _hubContext;
        //private ActorManager _actorManager;

        public HomeController(ILogger<HomeController> logger, IHubContext<ChatHub> hubContext, ISysAkkaManager akkaManager)
        {
            _logger = logger;
            //_actorManager = actorManager;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            //_actorManager.CommActor.Tell("Web Home Click");
            //await _hubContext.Clients.All.SendAsync("ReceiveMessage", "555", "123");
            return View();
            
        }

        public IActionResult Privacy()
        {
            //_actorManager.CommActor.Tell("Web Privacy Click");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //_actorManager.CommActor.Tell("Web Error Click");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
