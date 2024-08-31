using ClosedXML.Excel;
using Kykkalite_UI.Dtos.FabrikalarDtos;
using Kykkalite_UI.Dtos.HammaddeGruplariDtos;
using Kykkalite_UI.Dtos.HammaddelerDtos;
using Kykkalite_UI.Dtos.ParametrelerDtos;
using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace Kykkalite_UI.Controllers
{
    public class HammaddelerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IHttpContextAccessor _contextAccessor;
        public HammaddelerController(IHttpClientFactory httpClientFactory, ILoginService loginService, IHttpContextAccessor contextAccessor)
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
                var responseMessage = await client.GetAsync("http://localhost:44344/api/Hammaddeler");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultHammaddelerDto>>(jsonData);
                    return View(values);
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:44344/api/Hammaddeler");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultHammaddelerDto>>(jsonData);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Hammaddeler");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "MalzemeKodu";
                worksheet.Cell(currentRow, 2).Value = "MalzemeAciklamasi";
                worksheet.Cell(currentRow, 3).Value = "HammaddeId";
                worksheet.Cell(currentRow, 4).Value = "HammaddeGrupId";
                worksheet.Cell(currentRow, 5).Value = "PersonelSicilNo";
                worksheet.Cell(currentRow, 6).Value = "KullanımDurumu";
                worksheet.Cell(currentRow, 7).Value = "EklenmeGuncellenmeTarihi";

                foreach (var item in values)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.MalzemeKodu;
                    worksheet.Cell(currentRow, 2).Value = item.MalzemeAciklamasi;
                    worksheet.Cell(currentRow, 3).Value = item.HammaddeId;
                    worksheet.Cell(currentRow, 4).Value = item.PersonelSicilNo;
                    worksheet.Cell(currentRow, 5).Value = item.PersonelSicilNo;
                    worksheet.Cell(currentRow, 6).Value = item.KullanımDurumu;
                    worksheet.Cell(currentRow, 7).Value = item.EklenmeGuncellenmeTarihi;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Hammaddeler.xlsx");
                }
            }

        }
        [HttpGet]
        public async Task<IActionResult> CreateHammaddeler()
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:44344/api/HammaddeGruplari");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultHammaddeGruplariDto>>(jsonData);

            List<SelectListItem> hammaddeGrupValues = (from x in values.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.HMGrupAdi,
                                                       Value = x.HammaddeGrupID.ToString()
                                                   }).ToList();
            var client2 = _httpClientFactory.CreateClient();
            var responseMessage2 = await client2.GetAsync("http://localhost:44344/api/Hammaddeler");

            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<List<ResultHammaddelerDto>>(jsonData2);

            List<SelectListItem> urunadValues = (from x in values2.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.MalzemeAciklamasi,
                                                     Value = x.HammaddeId.ToString()
                                                 }).ToList();
            ViewBag.malzemeaciklamasi = urunadValues;
            ViewBag.v = hammaddeGrupValues;
            ViewBag.UserId = userId;
            ViewBag.Token = token;
            ViewBag.PersonelSicilNo = userId;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateHammaddeler(CreateHammadelerDto createHammadelerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createHammadelerDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:44344/api/Hammaddeler", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateHammaddeler()
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:44344/api/HammaddeGruplari");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultHammaddeGruplariDto>>(jsonData);

            List<SelectListItem> hammaddeGrupValues = (from x in values.ToList()
                                                       select new SelectListItem
                                                       {
                                                           Text = x.HMGrupAdi,
                                                           Value = x.HammaddeGrupID.ToString()
                                                       }).ToList();
            var client2 = _httpClientFactory.CreateClient();
            var responseMessage2 = await client2.GetAsync("http://localhost:44344/api/Hammaddeler");

            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<List<ResultHammaddelerDto>>(jsonData2);

            List<SelectListItem> urunadValues = (from x in values2.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.MalzemeAciklamasi,
                                                     Value = x.HammaddeId.ToString()
                                                 }).ToList();
            ViewBag.malzemeaciklamasi = urunadValues;
            ViewBag.v = hammaddeGrupValues;
            ViewBag.UserId = userId;
            ViewBag.Token = token;
            ViewBag.PersonelSicilNo = userId;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateHammaddeler(UpdateHammadelerDto updateHammadelerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateHammadelerDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:44344/api/Hammaddeler/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
