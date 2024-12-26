using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Kykkalite_UI.Dtos.DashboardDtos;
using System.Net.Http.Headers;

namespace Kykkalite_UI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IHttpContextAccessor _contextAccessor;
        public DashboardController(IHttpClientFactory httpClientFactory, ILoginService loginService, IHttpContextAccessor contextAccessor)
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

                var responseMessage = await client.GetAsync("http://localhost:44344/api/Dashboard");
                List<UrunDegKatDto> fabrika1Values = new List<UrunDegKatDto>();
                List<UrunDegKatDto> fabrika2Values = new List<UrunDegKatDto>();
                List<UrunDegKatDto> fabrika3Values = new List<UrunDegKatDto>();
                List<UrunDegKatDto> fabrika4Values = new List<UrunDegKatDto>();
                List<UrunDegKatDto> fabrika5Values = new List<UrunDegKatDto>();

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var allValues = JsonConvert.DeserializeObject<List<UrunDegKatDto>>(jsonData)
                                    .Where(x => x.FabrikaID == 1 || x.FabrikaID == 2 || x.FabrikaID == 3 || x.FabrikaID == 4 || x.FabrikaID == 5)
                                    .ToList();

                    fabrika1Values = allValues.Where(x => x.FabrikaID == 1).Take(5).ToList();
                    fabrika2Values = allValues.Where(x => x.FabrikaID == 2).Take(5).ToList();
                    fabrika3Values = allValues.Where(x => x.FabrikaID == 3).Take(5).ToList();
                    fabrika4Values = allValues.Where(x => x.FabrikaID == 4).Take(5).ToList();
                    fabrika5Values = allValues.Where(x => x.FabrikaID == 5).Take(5).ToList();
                }

                var responseMessage1 = await client.GetAsync("http://localhost:44344/api/Dashboard/uo");
                List<UrunOranDto> fabrika1Oran = new List<UrunOranDto>();
                List<UrunOranDto> fabrika2Oran = new List<UrunOranDto>();
                List<UrunOranDto> fabrika3Oran = new List<UrunOranDto>();
                List<UrunOranDto> fabrika4Oran = new List<UrunOranDto>();

                if (responseMessage1.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage1.Content.ReadAsStringAsync();
                    var allOran = JsonConvert.DeserializeObject<List<UrunOranDto>>(jsonData)
                                   .Where(x => x.FabrikaID == 1 || x.FabrikaID == 2 || x.FabrikaID == 3 || x.FabrikaID == 4)
                                   .ToList();

                    fabrika1Oran = allOran.Where(x => x.FabrikaID == 1).Take(5).ToList();
                    fabrika2Oran = allOran.Where(x => x.FabrikaID == 2).Take(5).ToList();
                    fabrika3Oran = allOran.Where(x => x.FabrikaID == 3).Take(5).ToList();
                    fabrika4Oran = allOran.Where(x => x.FabrikaID == 4).Take(5).ToList();

                    double toplamSayiFabrika1 = allOran
                    .Where(x => x.FabrikaID == 1) 
                    .Sum(x => x.Sayi); 
                   
                    double toplamSonucFabrika1 = allOran
                    .Where(x => x.FabrikaID == 1) 
                    .Sum(x => x.Sonuc); 

                    double toplamSayiFabrika2 = allOran
                   .Where(x => x.FabrikaID == 2) 
                   .Sum(x => x.Sayi);

                    double toplamSonucFabrika2 = allOran
                    .Where(x => x.FabrikaID == 2) 
                    .Sum(x => x.Sonuc);

                    double toplamSayiFabrika3 = allOran
                     .Where(x => x.FabrikaID == 3)
                    .Sum(x => x.Sayi);

                    double toplamSonucFabrika3 = allOran
                    .Where(x => x.FabrikaID == 3)
                    .Sum(x => x.Sonuc);
                    
                    double toplamSayiFabrika4 = allOran
                     .Where(x => x.FabrikaID == 4)
                    .Sum(x => x.Sayi);

                    double toplamSonucFabrika4 = allOran
                    .Where(x => x.FabrikaID == 4)
                    .Sum(x => x.Sonuc);

                    double[] factoryOranlar = new double[4];
                    factoryOranlar[0] = Math.Round((toplamSayiFabrika1 / toplamSonucFabrika1 * 100), 0);
                    factoryOranlar[1] = Math.Round((toplamSayiFabrika2 / toplamSonucFabrika2 * 100), 0);
                    factoryOranlar[2] = Math.Round((toplamSayiFabrika3 / toplamSonucFabrika3 * 100), 0);
                    factoryOranlar[3] = Math.Round((toplamSayiFabrika4 / toplamSonucFabrika4 * 100), 0);

                    // Diziyi ViewBag'e ata
                    ViewBag.FactoryOranlar = factoryOranlar;

                }

                // 3. GET request to /api/Dashboard/hm
                var postResponseMessage = await client.GetAsync("http://localhost:44344/api/Dashboard/hm");
                List<UrunDegKatDto> hammaddelerList = new List<UrunDegKatDto>();

                if (postResponseMessage.IsSuccessStatusCode)
                {
                    var postJsonData = await postResponseMessage.Content.ReadAsStringAsync();
                    hammaddelerList = JsonConvert.DeserializeObject<List<UrunDegKatDto>>(postJsonData)
                                       .Take(5)
                                       .ToList(); // Get the first 5 raw materials
                }

                // 4. GET request to /api/Dashboard/fo
                var foResponseMessage = await client.GetAsync("http://localhost:44344/api/Dashboard/fo");

                if (foResponseMessage.IsSuccessStatusCode)
                {
                    var foJsonData = await foResponseMessage.Content.ReadAsStringAsync();
                    var foValues = JsonConvert.DeserializeObject<List<FabrikaOnayDto>>(foJsonData);

                    var eskisehirOnay = foValues.FirstOrDefault(x => x.FabrikaID == 1); 
                    var adanaOnay = foValues.FirstOrDefault(x => x.FabrikaID == 2); 
                    var diyarbakirOnay = foValues.FirstOrDefault(x => x.FabrikaID == 3); 
                    var samsunOnay = foValues.FirstOrDefault(x => x.FabrikaID == 4); 
                    ViewBag.EskisehirOnay = eskisehirOnay;
                    ViewBag.AdanaOnay = adanaOnay;
                    ViewBag.DiyarbakirOnay = diyarbakirOnay;
                    ViewBag.SamsunOnay = samsunOnay;
                }

                ViewBag.Fabrika1Values = fabrika1Values;
                ViewBag.Fabrika2Values = fabrika2Values;
                ViewBag.Fabrika3Values = fabrika3Values;
                ViewBag.Fabrika4Values = fabrika4Values;
                ViewBag.Fabrika5Values = fabrika5Values;
                ViewBag.Fabrika1Oran = fabrika1Oran;
                ViewBag.Fabrika2Oran = fabrika2Oran;
                ViewBag.Fabrika3Oran = fabrika3Oran;
                ViewBag.Fabrika4Oran = fabrika4Oran;
                ViewBag.HammaddelerList = hammaddelerList;

                return View();
            }

            return View();
        }




    }
}
