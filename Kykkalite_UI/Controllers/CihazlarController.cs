using Kykkalite_UI.Dtos.CihazlarDtos;
using Kykkalite_UI.Dtos.FabrikalarDtos;
using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace Kykkalite_UI.Controllers
{
    public class CihazlarController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IHttpContextAccessor _contextAccessor;
        public CihazlarController(IHttpClientFactory httpClientFactory, ILoginService loginService, IHttpContextAccessor contextAccessor)
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
                var responseMessage = await client.GetAsync("http://localhost:44344/api/Cihazlar");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultCihazlarDto>>(jsonData);
                    return View(values);
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateCihazlar()
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
        public async Task<IActionResult> CreateCihazlar(CreateCihazlarDto createCihazlarDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCihazlarDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:44344/api/Cihazlar", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCihazlar(int id)
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:44344/api/Fabrika/");

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
        public async Task<IActionResult> UpdateCihazlar(UpdateCihazlarDto updateCihazlarDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCihazlarDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:44344/api/Cihazlar/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
