using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Kykkalite_UI.Dtos.UPNvalueDtos;
using System.Text;
using Kykkalite_UI.Services;
using ClosedXML.Excel;

namespace Kykkalite_UI.Controllers
{
    public class UPNvalueController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IHttpContextAccessor _contextAccessor;
        public UPNvalueController(IHttpClientFactory httpClientFactory, ILoginService loginService, IHttpContextAccessor contextAccessor)
        {

            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44344/api/UPNvalue");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultUPNvalueDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44344/api/UPNvalue");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultUPNvalueDto>>(jsonData);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("UPNvalue");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "UpnvalueId";
                worksheet.Cell(currentRow, 2).Value = "UpatamaKodu";
                worksheet.Cell(currentRow, 3).Value = "NumuneId";
                worksheet.Cell(currentRow, 4).Value = "Value";
                worksheet.Cell(currentRow, 5).Value = "EklenmeTarihi";
                worksheet.Cell(currentRow, 6).Value = "PersonelSicilNo";

                foreach (var item in values)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.UpnvalueId;
                    worksheet.Cell(currentRow, 2).Value = item.UpatamaKodu;
                    worksheet.Cell(currentRow, 3).Value = item.NumuneId;
                    worksheet.Cell(currentRow, 4).Value = item.Value;
                    worksheet.Cell(currentRow, 5).Value = item.PersonelSicilNo;
                    worksheet.Cell(currentRow, 6).Value = item.EklenmeTarihi;
                    worksheet.Cell(currentRow, 7).Value = item.PersonelSicilNo;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "UPNvalue.xlsx");
                }
            }
        }
        [HttpGet]
        public IActionResult CreateUPNvalue()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUPNvalue(CreateUPNvalueDto createUPNvalueDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createUPNvalueDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44344/api/UPNvalue", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUPNvalue(int id)
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44344/api/UPNvalue/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateUPNvalueDto>(jsonData);
                return View(values);
            }
            ViewBag.UserId = userId;
            ViewBag.Token = token;
            ViewBag.PersonelSicilNo = userId;
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUPNvalue(UpdateUPNvalueDto updateUPNvalueDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateUPNvalueDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44344/api/UPNvalue/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
