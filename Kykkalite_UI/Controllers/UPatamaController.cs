using Kykkalite_UI.Dtos.CihazlarDtos;
using Kykkalite_UI.Dtos.FabrikalarDtos;
using Kykkalite_UI.Dtos.UPatamaDtos;
using Kykkalite_UI.Dtos.CihazlarDtos;
using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using Kykkalite_UI.Dtos.ParametrelerDtos;
using Kykkalite_UI.Dtos.UrunlerDtos;

namespace Kykkalite_UI.Controllers
{
    public class UPatamaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IHttpContextAccessor _contextAccessor;
        public UPatamaController(IHttpClientFactory httpClientFactory, ILoginService loginService, IHttpContextAccessor contextAccessor)
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
                var responseMessage = await client.GetAsync("http://localhost:44344/api/UPatama");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultUPatamaDto>>(jsonData);
                    return View(values);
                }
            }
            return View();
        }
        [HttpGet]
         public async Task<IActionResult> CreateUPatama()
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
            var responseMessage4 = await client4.GetAsync("http://localhost:44344/api/Urunler");

            if (responseMessage4.IsSuccessStatusCode)
            {
                var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
                var values4 = JsonConvert.DeserializeObject<List<ResultUrunlerDto>>(jsonData4);

                List<SelectListItem> urunlervalues = values4.Select(x => new SelectListItem
                {
                    Text = x.MalzemeAciklamasi,      // Display value
                    Value = x.UrunId.ToString()      // Value for selection
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
        public async Task<IActionResult> CreateUPatama(CreateUPatamaDto createUPatamaDto)
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:44344/api/Fabrika");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFabrikalarDto>>(jsonData);

            List<SelectListItem> fabrikaValues = values.Select(x => new SelectListItem
            {
                Text = x.FabrikaAdi,
                Value = x.FabrikaID.ToString(),
            }).ToList();

            var responseMessage2 = await client.GetAsync("http://localhost:44344/api/Cihazlar");
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<List<ResultCihazlarDto>>(jsonData2);

            List<SelectListItem> cihazlarValues = values2.Select(x => new SelectListItem
            {
                Text = x.KullanılanCihazEkipman,
                Value = x.CihazID.ToString(),
            }).ToList();

            var responseMessage3 = await client.GetAsync("http://localhost:44344/api/Parametreler");
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

            var responseMessage4 = await client.GetAsync("http://localhost:44344/api/Urunler");
            if (responseMessage4.IsSuccessStatusCode)
            {
                var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
                var values4 = JsonConvert.DeserializeObject<List<ResultUrunlerDto>>(jsonData4);

                List<SelectListItem> urunlervalues = values4.Select(x => new SelectListItem
                {
                    Text = x.MalzemeAciklamasi,     
                    Value = x.UrunId.ToString()      
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

            var jsonDataPost = JsonConvert.SerializeObject(createUPatamaDto);
            StringContent stringContent = new StringContent(jsonDataPost, Encoding.UTF8, "application/json");
            var responseMessagePost = await client.PostAsync("http://localhost:44344/api/UPatama", stringContent);
            if (responseMessagePost.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Return the same view in case of failure (to show the error)
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UpdateUPatama(int id)
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
            var values2 = JsonConvert.DeserializeObject<List<ResultCihazlarDto>>(jsonData);

            List<SelectListItem> cihazlarValues = (from x in values2.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.KullanılanCihazEkipman,
                                                       Value = x.CihazID.ToString(),
                                                   }).ToList();
            var client3 = _httpClientFactory.CreateClient();
            var responseMessage3 = await client3.GetAsync("http://localhost:44344/api/Parametreler");

            var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
            var values3 = JsonConvert.DeserializeObject<List<ResultParametreDto>>(jsonData3);

            List<SelectListItem> parametrelervalues = (from x in values3.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.KontrolParametresi,
                                                       Value = x.ParametreKodu.ToString(),
                                                   }).ToList();
            var client4 = _httpClientFactory.CreateClient();
            var responseMessage4 = await client4.GetAsync("http://localhost:44344/api/Urunler");

            var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
            var values4 = JsonConvert.DeserializeObject<List<ResultUrunlerDto>>(jsonData4);

            List<SelectListItem> Urunlervalues = (from x in values4.ToList()
                                                       select new SelectListItem
                                                       {
                                                           Text = x.MalzemeAciklamasi,
                                                           Value = x.UrunId.ToString(),
                                                       }).ToList();
            ViewBag.u = Urunlervalues;
            ViewBag.v = fabrikaValues;
            ViewBag.vv = cihazlarValues;
            ViewBag.vvv = parametrelervalues;


            ViewBag.UserId = userId;
            ViewBag.Token = token;
            ViewBag.PersonelSicilNo = userId;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUPatama(UpdateUPatamaDto updateUPatamaDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateUPatamaDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:44344/api/UPatama/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateUpAtamaPasif()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUpAtamaPasif([FromBody]CreateUpAtamaPasifDto createUpAtamaPasifDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createUpAtamaPasifDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:44344/api/UPatama/pasif/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
