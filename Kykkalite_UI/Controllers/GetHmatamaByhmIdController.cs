using Kykkalite_UI.Dtos.GetHmatamaByHmıdDtos;
using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kykkalite_UI.Controllers
{
    public class GetHmatamaByhmIdController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;

        public GetHmatamaByhmIdController(IHttpClientFactory httpClientFactory, ILoginService loginService)
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
            public List<ResultGetHmatamaByHmıdDto> ResultSet { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string MalzemeAciklamasi)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var personelSicilNo = _loginService.GetPersonelSicilNo;
            ViewBag.PersonelSicilNo = personelSicilNo;

            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var encodedMalzemeAciklamasi = Uri.EscapeDataString(MalzemeAciklamasi);

                var response = await client.GetAsync($"https://localhost:44344/api/GetHmpatamaByhmid?malzemeaciklamasi={encodedMalzemeAciklamasi}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<ResultGetHmatamaByHmıdDto>>();

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
