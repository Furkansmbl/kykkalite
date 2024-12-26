using Microsoft.AspNetCore.Mvc;

namespace Kykkalite_UI.Controllers
{
    public class ChooseHmOrUController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ChooseHmOrUController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CheckUserRole()
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

            if (roleClaim != null && roleClaim.Value == "User")
            {
                return RedirectToAction("Index", "ChooseHmOrU");
            }
            else if (roleClaim != null && roleClaim.Value == "Admin")
            {
                return RedirectToAction("Index", "Urunler");
            }
            return RedirectToAction("Index", "Urunler");
        }
    }
}
