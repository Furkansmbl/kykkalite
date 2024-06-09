using ClosedXML.Excel;
using Kykkalite_UI.Dtos.FabrikalarDtos;
using Kykkalite_UI.Dtos.KullaniciDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace Kykkalite_UI.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public KullaniciController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44344/api/Kullanici");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultKullaniciDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44344/api/Kullanici");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultKullaniciDto>>(jsonData);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Kullanici");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "PersonelSicilNo";
                worksheet.Cell(currentRow, 2).Value = "PersonelAdiSoyadi";
                worksheet.Cell(currentRow, 3).Value = "FabrikaId";
                worksheet.Cell(currentRow, 4).Value = "Gorevi";
                worksheet.Cell(currentRow, 5).Value = "AdminUser";
                worksheet.Cell(currentRow, 6).Value = "Password";
                worksheet.Cell(currentRow, 7).Value = "KullanimDurumu";
                worksheet.Cell(currentRow, 8).Value = "EklenmeGuncellenmeTarihi";



                foreach (var item in values)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.PersonelSicilNo;
                    worksheet.Cell(currentRow, 2).Value = item.PersonelAdiSoyadi;
                    worksheet.Cell(currentRow, 3).Value = item.FabrikaId;
                    worksheet.Cell(currentRow, 4).Value = item.Gorevi;
                    worksheet.Cell(currentRow, 5).Value = item.AdminUser;
                    worksheet.Cell(currentRow, 6).Value = item.Password;
                    worksheet.Cell(currentRow, 7).Value = item.KullanimDurumu;
                    worksheet.Cell(currentRow, 7).Value = item.EklenmeGuncellenmeTarihi;


                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Kullanicilar.xlsx");
                }
            }

        }
        [HttpGet]
        public async Task<IActionResult> CreateKullanici()
        {
           

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44344/api/Fabrika");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFabrikalarDto>>(jsonData);

            List<SelectListItem> fabrikaValues = (from x in values.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.FabrikaAdi,
                                                      Value = x.FabrikaID.ToString(),
                                                  }).ToList();
            ViewBag.v = fabrikaValues;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateKullanici(CreateKullaniciDto createKullaniciDto )
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createKullaniciDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44344/api/Kullanici", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateKullanici()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44344/api/Fabrika");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFabrikalarDto>>(jsonData);

            List<SelectListItem> fabrikaValues = (from x in values.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.FabrikaAdi,
                                                      Value = x.FabrikaID.ToString(),
                                                  }).ToList();
            ViewBag.v = fabrikaValues;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateKullanici(UpdateKullaniciDto updateKullaniciDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateKullaniciDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44344/api/Kullanici/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
