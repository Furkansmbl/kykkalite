using Microsoft.AspNetCore.Mvc;
using System.Text;
using Kykkalite_UI.Dtos.LoginDtos;
using System.Text.Json;

namespace Kykkalite_UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateLoginDto createLoginDto)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(createLoginDto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:44344/api/Login", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Fabrikalar"); 
            }
            else
            {
                return View();
            }
        }

    }
}
