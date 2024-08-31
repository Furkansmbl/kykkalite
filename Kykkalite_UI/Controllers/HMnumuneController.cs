using FluentEmail.Core.Models;
using FluentEmail.Core;
using Kykkalite_UI.Dtos.FabrikalarDtos;
using Kykkalite_UI.Dtos.HammaddelerDtos;
using Kykkalite_UI.Dtos.HMnumuneDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Kykkalite_UI.Services;

namespace Kykkalite_UI.Controllers
{
    public class HMnumuneController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IFluentEmail fluentEmail;
        private readonly IHttpContextAccessor _contextAccessor;
        public HMnumuneController(IHttpClientFactory httpClientFactory, ILoginService loginService, IHttpContextAccessor contextAccessor, IFluentEmail fluentEmail)
        {

            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
            this.fluentEmail = fluentEmail;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:44344/api/HMnumune");
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
        public async Task<IActionResult> CreateHMnumune([FromBody] CreateHMnumuneDto createHMnumuneDto)
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
                var responseMessage = await client.PostAsync("http://localhost:44344/api/HMnumune", stringContent);
                Console.WriteLine(responseMessage);
                // Handle response here...
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return View();
        }
            private string GenerateToken()
            {
                return Guid.NewGuid().ToString("N");
            }


        
        [HttpGet]
        public async Task<IActionResult> UpdateHMnumune(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:44344/api/HMnumune/{id}");
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
            var responseMessage = await client.PutAsync("http://localhost:44344/api/HMnumune/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
