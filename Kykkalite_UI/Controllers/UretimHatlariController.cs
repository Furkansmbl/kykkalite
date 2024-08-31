using Kykkalite_UI.Dtos.FabrikalarDtos;
using Kykkalite_UI.Dtos.UretimHatlariDtos;
using Kykkalite_UI.Dtos.FabrikalarDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace Kykkalite_UI.Controllers
{
    public class UretimHatlariController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UretimHatlariController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:44344/api/UretimHatlari");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultUretimHatlariDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateUretimHatlari()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:44344/api/Fabrika");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFabrikalarDto>>(jsonData);

            List<SelectListItem> fabrikaValues = (from x in values.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.FabrikaAdi,
                                                      Value = x.FabrikaID.ToString(),
                                                  }).ToList();
            ViewBag.v = fabrikaValues;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUretimHatlari(CreateUretimHatlariDto createUretimHatlariDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createUretimHatlariDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:44344/api/UretimHatlari", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUretimHatlari(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:44344/api/UretimHatlari/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateUretimHatlariDto>(jsonData);
                return View(values);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUretimHatlari(UpdateUretimHatlariDto updateUretimHatlariDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateUretimHatlariDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:44344/api/UretimHatlari/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
