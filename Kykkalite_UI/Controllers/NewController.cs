using Kykkalite_UI.Dtos.NewDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kykkalite_UI.Controllers
{
    public class NewController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NewController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44344/api/Neww");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultNewDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
