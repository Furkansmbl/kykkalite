using Microsoft.AspNetCore.Mvc;
using Kykkalite_UI.Dtos.GetUpatamaKodlariByUrunIDDtos;
using Kykkalite_UI.Services;
using Kykkalite_UI.Dtos.UrunlerDtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Kykkalite_UI.Controllers
{
    public class GetUpatamaKodlariByUrunIDController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;

        public GetUpatamaKodlariByUrunIDController(IHttpClientFactory httpClientFactory, ILoginService loginService)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var personelSicilNo = _loginService.GetPersonelSicilNo;
            ViewBag.v = await GetProductDescription();

     
            ViewBag.PersonelSicilNo = personelSicilNo;

            return View();
        }

        private async Task<List<SelectListItem>> GetProductDescription()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:44344/api/Urunler");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultUrunlerDto>>(jsonData);
            var urunGrupValues = values.Select(x => new SelectListItem
            {
                Text = x.MalzemeAciklamasi,
                Value = x.MalzemeAciklamasi,
            }).ToList();
            return urunGrupValues;
        }

        public class QueryViewModel
        {
            public List<ResultGetUpatamaKodlariByUrunIDDto> ResultSet { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string MalzemeAciklamasi)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var personelSicilNo = _loginService.GetPersonelSicilNo;
            ViewBag.PersonelSicilNo = personelSicilNo;
            ViewBag.v = await GetProductDescription();
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var encodedMalzemeAciklamasi = Uri.EscapeDataString(MalzemeAciklamasi);
                var response = await client.GetAsync($"http://localhost:44344/api/GetUpatamaKodlariByUrunID?Malzemeaciklamasi={encodedMalzemeAciklamasi}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<ResultGetUpatamaKodlariByUrunIDDto>>();

                    var viewModel = new QueryViewModel
                    {
                        ResultSet = result
                    };

                    return View(viewModel);
                }
            }

            return View(new QueryViewModel());
        }
    }
}

