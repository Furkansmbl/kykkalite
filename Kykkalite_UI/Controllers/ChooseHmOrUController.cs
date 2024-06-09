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
    }
}
