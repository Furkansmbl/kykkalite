using Kykkalite_UI.Dtos.FabrikalarDtos;
using Kykkalite_UI.Dtos.HammaddelerDtos;
using Kykkalite_UI.Dtos.HMPatamaDtos;
using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace Kykkalite_UI.Controllers
{
    public class HMPatamaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IHttpContextAccessor _contextAccessor;
        public HMPatamaController(IHttpClientFactory httpClientFactory, ILoginService loginService, IHttpContextAccessor contextAccessor)
        {

            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
        }
        public async Task<IActionResult> Index()
        {
            var user = User.Claims;
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("http://localhost:44344/api/HMPatama");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultHMPatamaDto>>(jsonData);
                    return View(values);
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateHMPatama()
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;

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
            ViewBag.UserId = userId;
            ViewBag.Token = token;
            ViewBag.PersonelSicilNo = userId;


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateHMPatama(CreateHMPatamaDto createHMPatamaDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createHMPatamaDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:44344/api/HMPatama", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateHMPatama()
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
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
            ViewBag.UserId = userId;
            ViewBag.Token = token;
            ViewBag.PersonelSicilNo = userId;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateHMPatama(UpdateHMPatamaDto updateHMPatamaDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateHMPatamaDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:44344/api/HMPatama/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
