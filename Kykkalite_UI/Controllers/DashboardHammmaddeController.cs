using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Kykkalite_UI.Dtos.DashboardDtos;

namespace Kykkalite_UI.Controllers
{

    public class DashboardHammmaddeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IHttpContextAccessor _contextAccessor;
        public DashboardHammmaddeController(IHttpClientFactory httpClientFactory, ILoginService loginService, IHttpContextAccessor contextAccessor)
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
                var postResponseMessage = await client.GetAsync("http://localhost:44344/api/Dashboard/hm");

                if (postResponseMessage.IsSuccessStatusCode)
                {
                    var foJsonData = await postResponseMessage.Content.ReadAsStringAsync();
                    var foValues = JsonConvert.DeserializeObject<List<UrunDegKatDto>>(foJsonData);

                    var eskisehirDegKat = foValues.Where(x => x.FabrikaID == 1).Take(5).ToList();
                    var adanaDegKat = foValues.Where(x => x.FabrikaID == 2).Take(5).ToList();
                    var diyarbakirDegKat = foValues.Where(x => x.FabrikaID == 3).Take(5).ToList();
                    var samsunDegKat = foValues.Where(x => x.FabrikaID == 4).Take(5).ToList();

                    ViewBag.EskisehirDegKat = eskisehirDegKat;
                    ViewBag.AdanaDegKat = adanaDegKat;
                    ViewBag.DiyarbakirDegKat = diyarbakirDegKat;
                    ViewBag.SamsunDegKat = samsunDegKat;
                }


                var foResponseMessage = await client.GetAsync("http://localhost:44344/api/Dashboard/foHammadde");

                if (foResponseMessage.IsSuccessStatusCode)
                {
                    var foJsonData = await foResponseMessage.Content.ReadAsStringAsync();
                    var foValues = JsonConvert.DeserializeObject<List<FabrikaOnayDto>>(foJsonData);

                    var eskisehirOnay = foValues.FirstOrDefault(x => x.FabrikaID == 1);
                    var adanaOnay = foValues.FirstOrDefault(x => x.FabrikaID == 2);
                    var diyarbakırOnay = foValues.FirstOrDefault(x => x.FabrikaID == 3);
                    var samsunOnay = foValues.FirstOrDefault(x => x.FabrikaID == 4);

                    ViewBag.EskisehirOnay = eskisehirOnay;
                    ViewBag.AdanaOnay = adanaOnay;
                    ViewBag.DiyarbakırOnay = diyarbakırOnay;
                    ViewBag.SamsunOnay = samsunOnay;

                    ViewBag.Genel = foValues;
                }

                var redResponseMessage = await client.GetAsync("http://localhost:44344/api/Dashboard/tedred");

                if (redResponseMessage.IsSuccessStatusCode)
                {
                    var foJsonData = await redResponseMessage.Content.ReadAsStringAsync();
                    var foValues = JsonConvert.DeserializeObject<List<FabrikaOnayDto>>(foJsonData);

                    var eskisehirRed = foValues.Where(x => x.FabrikaID == 1).Take(5).ToList();
                    var adanaRed = foValues.Where(x => x.FabrikaID == 2).Take(5).ToList();
                    var diyarbakirRed = foValues.Where(x => x.FabrikaID == 3).Take(5).ToList();
                    var samsunRed = foValues.Where(x => x.FabrikaID == 4).Take(5).ToList();

                    ViewBag.DiyarbakirRed = diyarbakirRed;
                    ViewBag.SamsunRed = samsunRed;
                    ViewBag.EskisehirRed = eskisehirRed;
                    ViewBag.AdanaRed = adanaRed;
                }
                var sartliOnayResponseMessage = await client.GetAsync("http://localhost:44344/api/Dashboard/tedSartli");

                if (redResponseMessage.IsSuccessStatusCode)
                {
                    var foJsonData = await sartliOnayResponseMessage.Content.ReadAsStringAsync();
                    var foValues = JsonConvert.DeserializeObject<List<FabrikaOnayDto>>(foJsonData);

                    var eskisehirSartli = foValues.Where(x => x.FabrikaID == 1).Take(5).ToList();
                    var adanaSartli = foValues.Where(x => x.FabrikaID == 2).Take(5).ToList();
                    var diyarbakirSartli = foValues.Where(x => x.FabrikaID == 3).Take(5).ToList();
                    var samsunSartli = foValues.Where(x => x.FabrikaID == 4).Take(5).ToList();

                    ViewBag.DiyarbakirSartli = diyarbakirSartli;
                    ViewBag.SamsunSartli = samsunSartli;
                    ViewBag.EskisehirSartli = eskisehirSartli;
                    ViewBag.AdanaSartli = adanaSartli;
                }
                return View();

            }
            return View();
        }
    }
}
