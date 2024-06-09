using Kykkalite_UI.Dtos.FabrikalarDtos;
using Kykkalite_UI.Dtos.HammaddelerDtos;
using Kykkalite_UI.Dtos.HMnumuneDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Kykkalite_UI.Controllers
{
    public class HMnumuneController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HMnumuneController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44344/api/HMnumune");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultHMnumuneDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateHMnumune()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateHMnumune([FromBody] CreateHMnumuneDto createHMnumuneDto )
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { errors });
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createHMnumuneDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            try
            {
                var responseMessage = await client.PostAsync("https://localhost:44344/api/HMnumune", stringContent);
                Console.WriteLine(responseMessage);
                // Handle response here...
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            /* if (!responseMessage.IsSuccessStatusCode)
             {
                 // Log response content for debugging
                 var responseContent = await responseMessage.Content.ReadAsStringAsync();
                 return RedirectToAction("Index");
             }*/

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateHMnumune(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44344/api/HMnumune/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateHMnumuneDto>(jsonData);
                return View(values);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateHMnumune(UpdateHMnumuneDto updateHMnumuneDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateHMnumuneDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44344/api/HMnumune/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
