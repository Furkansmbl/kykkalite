using Kykkalite_UI.Dtos.HammaddelerDtos;
using Kykkalite_UI.Dtos.GetHmatamaByHmıdDtos;

using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using KykKaliteApi.Dtos.GetHmpatamaByHmIdDtos;

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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var personelSicilNo = _loginService.GetPersonelSicilNo;
            var fabrikaId = _loginService.GetFabrikaId;

            ViewBag.v = await GetProductDescription();
            var tedarikciList = await GetTedarikci();
            ViewBag.TedarikciList = tedarikciList; // Pass the full list to the view
            ViewBag.PersonelSicilNo = personelSicilNo;
            ViewBag.FabrikaId = fabrikaId;

            return View();
        }

        private async Task<List<SelectListItem>> GetProductDescription()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5185/api/Hammaddeler");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultHammaddelerDto>>(jsonData);
            var urunGrupValues = values.Select(x => new SelectListItem
            {
                Text = x.MalzemeAciklamasi,
                Value = x.MalzemeAciklamasi,
            }).ToList();
            return urunGrupValues;
        }

        private async Task<List<ResultGetHmpatamaByHmıdDto>> GetTedarikci()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5185/api/GetHmpatamaByhmid/Pa");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultGetHmpatamaByHmıdDto>>(jsonData);

            return values;
        }
        public class QueryViewModel
        {
            public List<ResultGetHmatamaByHmıdDto> ResultSet { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string MalzemeAciklamasi, int fabrikaId, string THMID, string Unvani)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var personelSicilNo = _loginService.GetPersonelSicilNo;

            ViewBag.PersonelSicilNo = personelSicilNo;
            ViewBag.v = await GetProductDescription();
            var tedarikciList = await GetTedarikci();
            ViewBag.TedarikciList = tedarikciList;
            var FabrikaId = _loginService.GetFabrikaId;
            ViewBag.FabrikaId = FabrikaId;

            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var encodedMalzemeAciklamasi = MalzemeAciklamasi != null ? Uri.EscapeDataString(MalzemeAciklamasi) : string.Empty;
                var encodedTHMID = THMID != null ? Uri.EscapeDataString(THMID) : string.Empty;
                var encodedUnvani = Unvani != null ? Uri.EscapeDataString(Unvani) : string.Empty;

                // İlk API çağrısı
                var response = await client.GetAsync($"http://localhost:5185/api/GetHmpatamaByhmid?MalzemeAciklamasi={encodedMalzemeAciklamasi}&fabrikaId={fabrikaId}&THMID={encodedTHMID}&Unvani={encodedUnvani}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<ResultGetHmatamaByHmıdDto>>();

                    double highestN = double.MinValue;

                    // Process each result item
                    foreach (var item in result)
                    {
                        if (!string.IsNullOrEmpty(item.Value))
                        {
                            var values = item.Value.Split('-').Select(x => double.Parse(x.Trim())).ToList();

                            var average = values.Average();
                            var stdDev = Math.Sqrt(values.Average(v => Math.Pow(v - average, 2)));
                            var errorLimit = average * 0.10;

                            Console.WriteLine($"Malzeme: {item.MalzemeAciklamasi}, Ortalama: {average}, Standart Sapma: {stdDev}, Hata Limiti: {errorLimit}");

                            item.Averages.Add(average);
                            item.StandardDeviations.Add(stdDev);
                            item.ErrorLimits.Add(errorLimit);

                            var numuneSeriNoSarjNo = item.NumuneSeriNoSarjNo; 
                            if (!string.IsNullOrEmpty(numuneSeriNoSarjNo))
                            {
                                var numuneValues = numuneSeriNoSarjNo.Split('-').Select(x => double.Parse(x.Trim())).ToList();
                                double N = 0;

                                for (int i = 0; i < numuneValues.Count - 1; i++)
                                {
                                    var current = numuneValues[i];
                                    var next = numuneValues[i + 1];
                                    N += next > current ? (next - current) : next;
                                }

                                N += numuneValues.First();

                                Console.WriteLine($"N: {N}");

                                double Z = 1.96;
                                var n = ((N * Z * Z * stdDev * stdDev) / (((N - 1) * errorLimit * errorLimit) + (Z * Z * stdDev * stdDev)));

                                if (double.IsInfinity(n) || n > 1e6)  
                                {
                                    Console.WriteLine("Calculated 'n' is too large or infinite. Skipping calculation.");
                                }
                                else
                                {
                                    if (n > highestN)
                                    {
                                        highestN = n;
                                    }
                                }
                            }

                        }
                    }

                   
                    foreach (var item in result)
                    {
                        item.Hesaplama = highestN.ToString();
                    }

                    var viewModel = new QueryViewModel
                    {
                        ResultSet = result,
                    };

                    return View(viewModel);
                }
            }

            return View(new QueryViewModel());
        }

    }
}
