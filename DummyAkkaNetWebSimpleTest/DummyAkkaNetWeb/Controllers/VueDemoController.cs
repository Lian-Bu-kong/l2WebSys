using Microsoft.AspNetCore.Mvc;

namespace DummyAkkaNetWeb.Controllers
{
    public class VueDemoController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
