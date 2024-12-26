using Kykkalite_UI.Dtos.GetRaporDtos;
using Kykkalite_UI.Dtos.HammaddelerDtos;
using Kykkalite_UI.Dtos.ParametrelerDtos;
using Kykkalite_UI.Dtos.GetHmatamaByHmıdDtos;
using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Kykkalite_UI.Controllers
{
    public class GetRaporHammaddeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;

        public GetRaporHammaddeController(IHttpClientFactory httpClientFactory, ILoginService loginService)
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
            ViewBag.vv = await GetParametreDescription();

            // Tedarikçi listesini al ve SelectListItem'e dönüştür
            var tedarikciList = await GetTedarikci();
            ViewBag.TedarikciList = tedarikciList.Select(t => new SelectListItem
            {
                Text = t.UNVANI, // Tedarikçi adını buraya koyun
                Value = t.UNVANI.ToString() // Tedarikçi ID'sini buraya koyun
            }).ToList();

            ViewBag.PersonelSicilNo = personelSicilNo;

            return View();
        }

        private async Task<List<ResultGetHmatamaByHmıdDto>> GetTedarikci()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:44344/api/GetHmpatamaByhmid/Pa");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultGetHmatamaByHmıdDto>>(jsonData);

            return values;
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

        private async Task<List<SelectListItem>> GetParametreDescription()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:44344/api/Parametreler");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultParametreDto>>(jsonData);
            var urunGrupValues = values.Select(x => new SelectListItem
            {
                Text = x.KontrolParametresi,
                Value = x.KontrolParametresi,
            }).ToList();
            return urunGrupValues;
        }
        public class QueryViewModel
        {
            public List<GetRaporDtos> ResultSet { get; set; }

        }
        [HttpPost]
        public async Task<IActionResult> Index(int fabrikaId, string malzemeAciklamasi, string OlusturmaTarihi, string BitisTarihi, string UNVANI)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var personelSicilNo = _loginService.GetPersonelSicilNo;
            ViewBag.PersonelSicilNo = personelSicilNo;
            ViewBag.v = await GetProductDescription();
            var tedarikciList = await GetTedarikci();
            ViewBag.TedarikciList = tedarikciList.Select(t => new SelectListItem
            {
                Text = t.UNVANI, // Tedarikçi adını buraya koyun
                Value = t.UNVANI.ToString() // Tedarikçi ID'sini buraya koyun
            }).ToList();


            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var encodedUnvani = Uri.EscapeDataString(UNVANI);

                var response = await client.GetAsync($"http://localhost:44344/api/GetRapor/hm?FabrikaId={fabrikaId}&MalzemeAciklamasi={malzemeAciklamasi}&OlusturmaTarihi={OlusturmaTarihi}&BitisTarihi={BitisTarihi}&UNVANI={encodedUnvani}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<GetRaporDtos>>();

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

