using Kykkalite_UI.Dtos.FabrikalarDtos;
using Kykkalite_UI.Dtos.HammaddelerDtos;
using Kykkalite_UI.Dtos.HMPatamaDtos;
using Kykkalite_UI.Dtos.CihazlarDtos;
using Kykkalite_UI.Dtos.ParametrelerDtos;
using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace Kykkalite_UI.Controllers
{
    public class HMPatamaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IHttpContextAccessor _contextAccessor;
        public HMPatamaController(IHttpClientFactory httpClientFactory, ILoginService loginService, IHttpContextAccessor contextAccessor)
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
                var responseMessage = await client.GetAsync("http://localhost:44344/api/HMPatama");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultHMPatamaDto>>(jsonData);
                    return View(values);
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateHMPatama()
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:44344/api/Fabrika");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFabrikalarDto>>(jsonData);

            List<SelectListItem> fabrikaValues = (from x in values.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.FabrikaAdi,
                                                      Value = x.FabrikaID.ToString(),
                                                  }).ToList();
            var client2 = _httpClientFactory.CreateClient();
            var responseMessage2 = await client2.GetAsync("http://localhost:44344/api/Cihazlar");

            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<List<ResultCihazlarDto>>(jsonData2);

            List<SelectListItem> cihazlarValues = (from x in values2.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.KullanılanCihazEkipman,
                                                       Value = x.CihazID.ToString(),
                                                   }).ToList();
            var client3 = _httpClientFactory.CreateClient();
            var responseMessage3 = await client3.GetAsync("http://localhost:44344/api/Parametreler");

            if (responseMessage3.IsSuccessStatusCode)
            {
                var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
                var values3 = JsonConvert.DeserializeObject<List<ResultParametreDto>>(jsonData3);

                List<SelectListItem> parametrelervalues = values3.Select(x => new SelectListItem
                {
                    Text = x.KontrolParametresi,      
                    Value = x.ParametreID.ToString() 
                }).ToList();

                ViewBag.ParametreList = parametrelervalues;
            }
            else
            {
                ViewBag.ParametreList = new List<SelectListItem>(); 
            }

            var client4 = _httpClientFactory.CreateClient();
            var responseMessage4 = await client4.GetAsync("http://localhost:44344/api/Hammaddeler");

            if (responseMessage4.IsSuccessStatusCode)
            {
                var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
                var values4 = JsonConvert.DeserializeObject<List<ResultHammaddelerDto>>(jsonData4);

                List<SelectListItem> urunlervalues = values4.Select(x => new SelectListItem
                {
                    Text = x.MalzemeAciklamasi,      
                    Value = x.HammaddeId.ToString()      
                }).ToList();

                
                ViewBag.UrunList = urunlervalues; 
            }
            else
            {
                ViewBag.UrunList = new List<SelectListItem>(); 
            }


            ViewBag.v = fabrikaValues;
            ViewBag.vv = cihazlarValues;


            ViewBag.UserId = userId;
            ViewBag.Token = token;
            ViewBag.PersonelSicilNo = userId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateHMPatama(CreateHMPatamaDto createHMPatamaDto)
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:44344/api/Fabrika");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFabrikalarDto>>(jsonData);

            List<SelectListItem> fabrikaValues = (from x in values.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.FabrikaAdi,
                                                      Value = x.FabrikaID.ToString(),
                                                  }).ToList();
            var client2 = _httpClientFactory.CreateClient();
            var responseMessage2 = await client2.GetAsync("http://localhost:44344/api/Cihazlar");

            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<List<ResultCihazlarDto>>(jsonData2);

            List<SelectListItem> cihazlarValues = (from x in values2.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.KullanılanCihazEkipman,
                                                       Value = x.CihazID.ToString(),
                                                   }).ToList();
            var client3 = _httpClientFactory.CreateClient();
            var responseMessage3 = await client3.GetAsync("http://localhost:44344/api/Parametreler");

            if (responseMessage3.IsSuccessStatusCode)
            {
                var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
                var values3 = JsonConvert.DeserializeObject<List<ResultParametreDto>>(jsonData3);

                List<SelectListItem> parametrelervalues = values3.Select(x => new SelectListItem
                {
                    Text = x.KontrolParametresi,      // Display value
                    Value = x.ParametreID.ToString()  // Value for selection
                }).ToList();

                ViewBag.ParametreList = parametrelervalues;
            }
            else
            {
                ViewBag.ParametreList = new List<SelectListItem>(); // Empty list in case of failure
            }

            var client4 = _httpClientFactory.CreateClient();
            var responseMessage4 = await client4.GetAsync("http://localhost:44344/api/Hammaddeler");

            if (responseMessage4.IsSuccessStatusCode)
            {
                var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
                var values4 = JsonConvert.DeserializeObject<List<ResultHammaddelerDto>>(jsonData4);

                List<SelectListItem> urunlervalues = values4.Select(x => new SelectListItem
                {
                    Text = x.MalzemeAciklamasi,
                    Value = x.HammaddeId.ToString()
                }).ToList();


                ViewBag.UrunList = urunlervalues;
            }
            else
            {
                ViewBag.UrunList = new List<SelectListItem>();
            }


            ViewBag.v = fabrikaValues;
            ViewBag.vv = cihazlarValues;


            ViewBag.UserId = userId;
            ViewBag.Token = token;
            ViewBag.PersonelSicilNo = userId;
            var jsonDatapost = JsonConvert.SerializeObject(createHMPatamaDto);
            StringContent stringContent = new StringContent(jsonDatapost, Encoding.UTF8, "application/json");
            var responseMessagePost = await client.PostAsync("http://localhost:44344/api/HMPatama", stringContent);
            if (responseMessagePost.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateHMPatama()
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:44344/api/Fabrika");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFabrikalarDto>>(jsonData);

            List<SelectListItem> fabrikaValues = (from x in values.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.FabrikaAdi,
                                                      Value = x.FabrikaID.ToString(),
                                                  }).ToList();
            ViewBag.v = fabrikaValues;
            ViewBag.UserId = userId;
            ViewBag.Token = token;
            ViewBag.PersonelSicilNo = userId;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateHMPatama(UpdateHMPatamaDto updateHMPatamaDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateHMPatamaDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:44344/api/HMPatama/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
