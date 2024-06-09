using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;
using Kykkalite_UI.Dtos.GetValueByMalzemeAciklamasiWParametreKodu;

namespace Kykkalite_UI.Controllers
{
    public class GetValueByMalzemeAciklamasiWParametreKoduController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;

        public GetValueByMalzemeAciklamasiWParametreKoduController(IHttpClientFactory httpClientFactory, ILoginService loginService)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
        }

        public async Task<IActionResult> Index()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var personelSicilNo = _loginService.GetPersonelSicilNo;
            ViewBag.PersonelSicilNo = personelSicilNo;

            return View();
        }
        public class QueryViewModel
        {
            public List<ResultGetValueDto> ResultSet { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string malzemeAciklamasi, string kontrolParametresi)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var personelSicilNo = _loginService.GetPersonelSicilNo;
            ViewBag.PersonelSicilNo = personelSicilNo;

            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var encodedMalzemeAciklamasi = Uri.EscapeDataString(malzemeAciklamasi);
                var encodedkontrolParametresi = Uri.EscapeDataString(kontrolParametresi);


                var response = await client.GetAsync($"https://localhost:44344/api/GetValueByMalzemeAciklamasiWParametreKodu?malzemeaciklamasi={encodedMalzemeAciklamasi}&kontrolparametresi={encodedkontrolParametresi}");

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
