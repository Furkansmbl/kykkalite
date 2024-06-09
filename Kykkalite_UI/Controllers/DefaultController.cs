using Microsoft.AspNetCore.Mvc;

namespace Kykkalite_UI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
