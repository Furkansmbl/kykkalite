using Kykkalite_UI.Dtos.FabrikalarDtos;
using Kykkalite_UI.Dtos.UrunGruplariDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Kykkalite_UI.Controllers
{
    public class UrunGruplariController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UrunGruplariController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:44344/api/UrunGruplari");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultUrunGruplarİDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateUrunGruplari()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUrunGruplari(CreateUrunGruplari createUrunGruplari)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createUrunGruplari);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:44344/api/UrunGruplari", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUrunGruplari(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:44344/api/UrunGruplari/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateUrunGruplariDto>(jsonData);
                return View(values);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUrunGruplari(UpdateUrunGruplariDto updateUrunGruplariDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateUrunGruplariDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:44344/api/UrunGruplari/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
