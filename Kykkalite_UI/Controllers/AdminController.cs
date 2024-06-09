using Microsoft.AspNetCore.Mvc;

namespace Kykkalite_UI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
