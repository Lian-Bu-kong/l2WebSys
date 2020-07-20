using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaBase;
using AkkaSys.MMS;
using DataAccess.Repository;
using DummyAkkaNetWeb.Actor;
using Microsoft.AspNetCore.Mvc;

namespace DummyAkkaNetWeb.Controllers
{
    public class CoilPDIController : Controller
    {
        private readonly ICoilRepo _coilRepository;
        private readonly ISysAkkaManager _akkaManager;

        public CoilPDIController(ICoilRepo coilRepository, ISysAkkaManager akkaManager)
        {
            _coilRepository = coilRepository;
            _akkaManager = akkaManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Send(string msg)
        {
            _akkaManager.GetActor(nameof(MMSSndEdit)).Tell(msg);

            return Json(true);
        }
    }
}
