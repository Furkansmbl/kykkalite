using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Kykkalite_UI.Dtos.TedarikciDtos;
using Kykkalite_UI.Dtos.FabrikalarDtos;
using System.Text;

namespace Kykkalite_UI.Controllers
{
    public class TedarikciController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IHttpContextAccessor _contextAccessor;
        public TedarikciController(IHttpClientFactory httpClientFactory, ILoginService loginService, IHttpContextAccessor contextAccessor)
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
                var responseMessage = await client.GetAsync("http://localhost:44344/api/Tedarikci");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<TedarikciDtos>>(jsonData);
                    return View(values);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateTedarikci()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTedarikci(TedarikciDtos tedarikciDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(tedarikciDtos);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:44344/api/Tedarikci", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateTedarikci(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:44344/api/Tedarikci/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateFabrikalarDto>(jsonData);
                return View(values);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTedarikci(TedarikciDtos tedarikciDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(tedarikciDtos);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:44344/api/Tedarikci/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
