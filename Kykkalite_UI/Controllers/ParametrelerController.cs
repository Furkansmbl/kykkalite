using ClosedXML.Excel;
using Kykkalite_UI.Dtos.FabrikalarDtos;
using Kykkalite_UI.Dtos.ParametrelerDtos;
using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace Kykkalite_UI.Controllers
{
    public class ParametrelerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IHttpContextAccessor _contextAccessor;
        public ParametrelerController(IHttpClientFactory httpClientFactory, ILoginService loginService, IHttpContextAccessor contextAccessor)
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
                var responseMessage = await client.GetAsync("https://localhost:44344/api/Parametreler");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultParametreDto>>(jsonData);
                    return View(values);
                }
            }
                return View();
        }
        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44344/api/Parametreler");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultParametreDto>>(jsonData);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Parametreler");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "ParametreKodu";
                worksheet.Cell(currentRow, 2).Value = "KontrolParametresi";
                worksheet.Cell(currentRow, 3).Value = "ParametreTipiOlcmeGozlem";
                worksheet.Cell(currentRow, 4).Value = "Birimi";
                worksheet.Cell(currentRow, 5).Value = "PersonelSicilNo";
                worksheet.Cell(currentRow, 6).Value = "KullanimDurumu";
                worksheet.Cell(currentRow, 7).Value = "EklenmeGuncellenmeTarihi";

                foreach (var item in values)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.ParametreKodu;
                    worksheet.Cell(currentRow, 2).Value = item.KontrolParametresi;
                    worksheet.Cell(currentRow, 3).Value = item.ParametreTipiOlcmeGozlem;
                    worksheet.Cell(currentRow, 4).Value = item.Birimi;
                    worksheet.Cell(currentRow, 5).Value = item.PersonelSicilNo;
                    worksheet.Cell(currentRow, 6).Value = item.KullanimDurumu;
                    worksheet.Cell(currentRow, 7).Value = item.EklenmeGuncellenmeTarihi;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Parametreler.xlsx");
                }
            }

        }
        [HttpGet]
        public IActionResult CreateParametreler()
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            ViewBag.UserId = userId;
            ViewBag.Token = token;
            ViewBag.PersonelSicilNo = userId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateParametreler(CreateParametrelerDto createParametrelerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createParametrelerDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44344/api/Parametreler", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateParametreler(int id)
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44344/api/Parametreler/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateParametrelerDto>(jsonData);
                return View(values);

            }
            ViewBag.UserId = userId;
            ViewBag.Token = token;
            ViewBag.PersonelSicilNo = userId;
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateParametreler(UpdateParametrelerDto updateParametrelerDto )
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateParametrelerDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44344/api/Parametreler/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
