using Microsoft.AspNetCore.Mvc;
using Kykkalite_UI.Dtos.GetUpatamaKodlariByUrunIDDtos;
using Kykkalite_UI.Services;
using Kykkalite_UI.Dtos.UrunlerDtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using KykKaliteApi.Dtos.GetHmpatamaByHmIdDtos;

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
            var fabrikaId = _loginService.GetFabrikaId;
            ViewBag.v = await GetProductDescription();
            ViewBag.UretimHatlariList = await GetUretimHatlari();
            ViewBag.PersonelSicilNo = personelSicilNo;
            ViewBag.FabrikaId = fabrikaId;
            return View();
        }

        private async Task<List<SelectListItem>> GetProductDescription()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5185/api/Urunler");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultUrunlerDto>>(jsonData);
            var urunGrupValues = values.Select(x => new SelectListItem
            {
                Text = x.MalzemeAciklamasi,
                Value = x.MalzemeAciklamasi,
            }).ToList();
            return urunGrupValues;
        }
        private async Task<List<ResultGetUpatamaKodlariByUrunIDDto>> GetUretimHatlari()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5185/api/GetUpatamaKodlariByUrunID/Uh");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultGetUpatamaKodlariByUrunIDDto>>(jsonData);

            return values;
        }
        public class QueryViewModel
        {
            public List<ResultGetUpatamaKodlariByUrunIDDto> ResultSet { get; set; }
            public string SelectedMalzemeAciklamasi { get; set; }
            public int SelectedFabrikaId { get; set; }
            public string SelectedHatAdiAciklamasi { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string MalzemeAciklamasi, int FabrikaId, string HatAdiAciklamasi)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var personelSicilNo = _loginService.GetPersonelSicilNo;
            ViewBag.PersonelSicilNo = personelSicilNo;
            var uretimHatlariList = await GetUretimHatlari();
            // Seçili değerleri ViewBag'lere set et
            ViewBag.UretimHatlariList = uretimHatlariList;
            ViewBag.v = await GetProductDescription();
            ViewBag.SelectedFabrikaId = FabrikaId;
            ViewBag.SelectedHatAdiAciklamasi = HatAdiAciklamasi;
            ViewBag.SelectedMalzemeAciklamasi = MalzemeAciklamasi;
            var fabrikaId = _loginService.GetFabrikaId;
            ViewBag.FabrikaId = fabrikaId;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var encodedMalzemeAciklamasi = Uri.EscapeDataString(MalzemeAciklamasi);
                var encodedHatAdiAciklamasi = Uri.EscapeDataString(HatAdiAciklamasi);
                var response = await client.GetAsync($"http://localhost:5185/api/GetUpatamaKodlariByUrunID?MalzemeAciklamasi={encodedMalzemeAciklamasi}&FabrikId={fabrikaId}&HatAdiAciklamasi={encodedHatAdiAciklamasi}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<ResultGetUpatamaKodlariByUrunIDDto>>();

                    double highestN = double.MinValue;

                    // Set `A` based on `FabrikaId`
                    double A = FabrikaId switch
                    {
                        1 => 135,
                        2 => 133,
                        3 => 175,
                        4 => 216,
                        _ => 135
                    };

                    foreach (var item in result)
                    {
                        // Eğer ParametreKritiklikSeviyesi string'inde "Onay" varsa ve Value boş değilse
                        if (item.ParametreKritiklikSeviyesi.Contains("ONAY") && !string.IsNullOrEmpty(item.Value))
                        {
                            var values = item.Value
                                .Split('-')
                                .Where(x => !string.IsNullOrWhiteSpace(x)) // Boş olan değerleri filtrele
                                .Select(x =>
                                {
                                    if (double.TryParse(x.Trim(), out double parsedValue))
                                    {
                                        return parsedValue; // Eğer sayıysa döndür
                                    }
                                    else
                                    {
                                        return double.NaN; 
                                    }
                                })
                                .Where(x => !double.IsNaN(x)) 
                                .ToList();

                            if (values.Any())
                            {
                                var average = values.Average();
                                var stdDev = Math.Sqrt(values.Average(v => Math.Pow(v - average, 2)));
                                var errorLimit = average * 0.10;

                                Console.WriteLine($"Malzeme: {item.MalzemeAciklamasi}, Ortalama: {average}, Standart Sapma: {stdDev}, Hata Limiti: {errorLimit}");

                                item.Averages.Add(average);
                                item.StandardDeviations.Add(stdDev);
                                item.ErrorLimits.Add(errorLimit);

                                var numuneSeriNoSarjNo = result[0].NumuneSeriNoSarjNo;
                                if (!string.IsNullOrEmpty(numuneSeriNoSarjNo))
                                {
                                    var numuneValues = numuneSeriNoSarjNo
                                        .Split('-')
                                        .Where(x => !string.IsNullOrWhiteSpace(x)) 
                                        .Select(x => double.Parse(x.Trim()))
                                        .ToList();

                                    double N = 0;
                                    for (int i = 0; i < numuneValues.Count - 1; i++)
                                    {
                                        var current = numuneValues[i];
                                        var next = numuneValues[i + 1];
                                        N += next > current ? (next - current) : next;
                                    }

                                    N += numuneValues.First();
                                    Console.WriteLine($"N: {N}");

                                    double Z = 2.58; // %99.5
                                    var n = A / ((N * Z * Z * stdDev * stdDev) / (((N - 1) * errorLimit * errorLimit) + (Z * Z * stdDev * stdDev)));

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
                                        item.Hesaplama = Math.Ceiling(n).ToString(); 
                                    }
                                }

                            }
                        }
                        }

                    foreach (var item in result)
                    {
                        item.Hesaplama = Math.Ceiling(highestN).ToString();
                    }

                    var viewModel = new QueryViewModel
                    {
                        ResultSet = result,
                        SelectedFabrikaId = FabrikaId, 
                        SelectedHatAdiAciklamasi = HatAdiAciklamasi, 
                        SelectedMalzemeAciklamasi = MalzemeAciklamasi 
                    };

                    return View(viewModel);
                }
            }

            return View(new QueryViewModel());
        }

    }
}

