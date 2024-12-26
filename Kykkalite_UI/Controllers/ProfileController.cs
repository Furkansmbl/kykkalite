using Microsoft.AspNetCore.Mvc;

namespace Kykkalite_UI.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
