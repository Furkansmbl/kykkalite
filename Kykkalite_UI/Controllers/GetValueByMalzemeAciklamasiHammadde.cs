using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;
using Kykkalite_UI.Dtos.GetValueByMalzemeAciklamasiWParametreKodu;
using Kykkalite_UI.Dtos.HammaddelerDtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Kykkalite_UI.Controllers
{
    public class GetValueByMalzemeAciklamasiHammadde : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;

        public GetValueByMalzemeAciklamasiHammadde(IHttpClientFactory httpClientFactory, ILoginService loginService)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
        }

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
            var responseMessage = await client.GetAsync("http://localhost:44344/api/Hammaddeler");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultHammaddelerDto>>(jsonData);
            var urunGrupValues = values.Select(x => new SelectListItem
            {
                Text = x.MalzemeAciklamasi,
                Value = x.MalzemeAciklamasi,
            }).ToList();
            return urunGrupValues;
        }
        public class QueryViewModel
        {
            public List<ResultGetValueDto> ResultSet { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string malzemeAciklamasi, string kontrolParametresi, string baslangicTarihi, string bitisTarihi)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var personelSicilNo = _loginService.GetPersonelSicilNo;
            ViewBag.PersonelSicilNo = personelSicilNo;
            ViewBag.v = await GetProductDescription();

            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var encodedMalzemeAciklamasi = Uri.EscapeDataString(malzemeAciklamasi);
                var encodedkontrolParametresi = Uri.EscapeDataString(kontrolParametresi);
                var encodedbaslangicTarihi = Uri.EscapeDataString(baslangicTarihi);
                var encodedbitisTarihi = Uri.EscapeDataString(bitisTarihi);


                var response = await client.GetAsync($"http://localhost:44344/api/GetValueByMalzemeAciklamasiWParametreKodu/hm?malzemeaciklamasi={encodedMalzemeAciklamasi}&kontrolparametresi={encodedkontrolParametresi}&baslangicTarihi={encodedbaslangicTarihi}&bitisTarihi={encodedbitisTarihi}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<ResultGetValueDto>>();

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
