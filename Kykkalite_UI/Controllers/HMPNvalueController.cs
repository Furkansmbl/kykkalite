using ClosedXML.Excel;
using Kykkalite_UI.Dtos.HMPNvalueDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Kykkalite_UI.Controllers
{
    public class HMPNvalueController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HMPNvalueController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44344/api/HMPNvalue");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultHMPNvalueDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44344/api/HMPNvalue");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultHMPNvalueDto>>(jsonData);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("HMPNvalue");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "HmpnvalueId";
                worksheet.Cell(currentRow, 2).Value = "HmpatamaKodu";
                worksheet.Cell(currentRow, 3).Value = "NumuneId";
                worksheet.Cell(currentRow, 4).Value = "Value";
                worksheet.Cell(currentRow, 5).Value = "EklenmeTarihi";
                worksheet.Cell(currentRow, 6).Value = "PersonelSicilNo";

                foreach (var item in values)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.HmpnvalueId;
                    worksheet.Cell(currentRow, 2).Value = item.HmpatamaKodu;
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
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HMPNvalue.xlsx");
                }
            }
        }
        [HttpGet]
        public IActionResult CreateHMPNvalue()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateHMPNvalue(CreateHMPNvalueDto createHMPNvalue)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createHMPNvalue);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44344/api/HMPNvalue", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateHMPNvalue(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44344/api/HMPNvalue/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateHMPNvalueDto>(jsonData);
                return View(values);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateHMPNvalue(UpdateHMPNvalueDto updateHMPNvalueDto )
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateHMPNvalueDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44344/api/HMPNvalue/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
