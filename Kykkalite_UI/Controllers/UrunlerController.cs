using Kykkalite_UI.Dtos.HammaddeGruplariDtos;
using Kykkalite_UI.Dtos.HammaddelerDtos;
using Kykkalite_UI.Dtos.UrunGruplariDtos;
using Kykkalite_UI.Dtos.UrunlerDtos;
using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using ClosedXML.Excel;

namespace Kykkalite_UI.Controllers
{
    public class UrunlerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IHttpContextAccessor _contextAccessor;
        public UrunlerController(IHttpClientFactory httpClientFactory, ILoginService loginService, IHttpContextAccessor contextAccessor)
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
                var responseMessage = await client.GetAsync("https://localhost:44344/api/Urunler");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultUrunlerDto>>(jsonData);
                    return View(values);
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44344/api/Urunler");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultUrunlerDto>>(jsonData);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Urunler");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "MalzemeAciklamasi";
                worksheet.Cell(currentRow, 2).Value = "MalzemeKodu";
                worksheet.Cell(currentRow, 3).Value = "UrunId";
                worksheet.Cell(currentRow, 4).Value = "UrunGrupId";
                worksheet.Cell(currentRow, 5).Value = "PersonelSicilNo";
                worksheet.Cell(currentRow, 6).Value = "KullanimDurumu";
                worksheet.Cell(currentRow, 7).Value = "EklenmeGuncellenmeTarihi";

                foreach (var item in values)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.MalzemeAciklamasi;
                    worksheet.Cell(currentRow, 2).Value = item.MalzemeKodu;
                    worksheet.Cell(currentRow, 3).Value = item.UrunId;
                    worksheet.Cell(currentRow, 4).Value = item.UrunGrupId;
                    worksheet.Cell(currentRow, 5).Value = item.PersonelSicilNo;
                    worksheet.Cell(currentRow, 6).Value = item.KullanimDurumu;
                    worksheet.Cell(currentRow, 7).Value = item.EklenmeGuncellenmeTarihi;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Urunler.xlsx");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateUrunler()
        {
           
                var userId = _loginService.GetPersonelSicilNo; 
                var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value; 

                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:44344/api/UrunGruplari");

                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultUrunGruplarİDto>>(jsonData);

                List<SelectListItem> urunGrupValues = (from x in values.ToList()
                                                       select new SelectListItem
                                                       {
                                                           Text = x.UgrupAdi,
                                                           Value = x.UrunGrupId.ToString()
                                                       }).ToList();
            ViewBag.v = urunGrupValues;
                ViewBag.UserId = userId; 
                ViewBag.Token = token; 
                ViewBag.PersonelSicilNo = userId; // PersonelSicilNo'yu view'a geçir
                return View();
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateUrunler(CreateUrunlerDto createUrunlerDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createUrunlerDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("https://localhost:44344/api/Urunler", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();

                    ViewBag.ErrorMessage = $"İstek başarısız oldu. Sunucu tarafından dönen hata: {responseContent}";
                    return View(createUrunlerDto);
                }
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.";
                return View(createUrunlerDto);
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUrunler()
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44344/api/UrunGruplari");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultUrunGruplarİDto>>(jsonData);

            List<SelectListItem> urunGrupValues = (from x in values.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.UgrupAdi,
                                                       Value = x.UrunGrupId.ToString()
                                                   }).ToList();

            var client2 = _httpClientFactory.CreateClient();
            var responseMessage2 = await client2.GetAsync("https://localhost:44344/api/Urunler");

            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<List<ResultUrunlerDto>>(jsonData2);

            List<SelectListItem> urunadValues = (from x in values2.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.MalzemeAciklamasi,
                                                     Value = x.UrunId.ToString()
                                                 }).ToList();
            ViewBag.malzemeaciklamasi = urunadValues;
            ViewBag.v = urunGrupValues;
            ViewBag.UserId = userId;
            ViewBag.Token = token;
            ViewBag.PersonelSicilNo = userId; // PersonelSicilNo'yu view'a geçir
            return View();


        }
        [HttpPost]
        public async Task<IActionResult> UpdateUrunler(UpdateUrunlerDto updateUrunlerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateUrunlerDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44344/api/Urunler/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
