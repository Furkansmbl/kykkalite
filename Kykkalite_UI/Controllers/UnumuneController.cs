using ClosedXML.Excel;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Smtp;
using Kykkalite_UI.Dtos.UnumuneDtos;
using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Text;

namespace Kykkalite_UI.Controllers
{
    public class UnumuneController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IFluentEmail fluentEmail;
        private readonly IHttpContextAccessor _contextAccessor;
        public UnumuneController(IHttpClientFactory httpClientFactory, ILoginService loginService, IHttpContextAccessor contextAccessor, IFluentEmail fluentEmail)
        {

            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
            this.fluentEmail = fluentEmail;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44344/api/Unumune");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultUnumuneDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44344/api/Unumune");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultUnumuneDto>>(jsonData);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Unumune");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "NumuneId";
                worksheet.Cell(currentRow, 2).Value = "UrunId";
                worksheet.Cell(currentRow, 3).Value = "SiraNo";
                worksheet.Cell(currentRow, 4).Value = "UretimTarihi";
                worksheet.Cell(currentRow, 5).Value = "NumuneSeriNoSarjNo";
                worksheet.Cell(currentRow, 6).Value = "MudahaleVarmi";
                worksheet.Cell(currentRow, 7).Value = "Aciklama";
                worksheet.Cell(currentRow, 8).Value = "OnayDurumu";
                worksheet.Cell(currentRow, 9).Value = "AmirOnayDurumu";
                worksheet.Cell(currentRow, 10).Value = "EklenmeTarihi";
                worksheet.Cell(currentRow, 11).Value = "PersonelSicilNo";


                foreach (var item in values)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.NumuneId;
                    worksheet.Cell(currentRow, 2).Value = item.UrunId;
                    worksheet.Cell(currentRow, 3).Value = item.SiraNo;
                    worksheet.Cell(currentRow, 4).Value = item.UretimTarihi;
                    worksheet.Cell(currentRow, 5).Value = item.NumuneSeriNoSarjNo;
                    worksheet.Cell(currentRow, 6).Value = item.MudahaleVarmi;
                    worksheet.Cell(currentRow, 7).Value = item.Aciklama;
                    worksheet.Cell(currentRow, 8).Value = item.OnayDurumu;
                    worksheet.Cell(currentRow, 9).Value = item.AmirOnayDurumu;
                    worksheet.Cell(currentRow, 10).Value = item.EklenmeTarihi;
                    worksheet.Cell(currentRow, 11).Value = item.PersonelSicilNo;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "UNumune.xlsx");
                }
            }
        }

        [HttpGet]
        public IActionResult CreateUnumune()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUnumune([FromBody] CreateUnumuneDto createUnumuneDto)
        {
            string token = createUnumuneDto.Token;

            if (string.IsNullOrEmpty(createUnumuneDto.Token) && (createUnumuneDto.red > 0 || createUnumuneDto.yellow > 0))
            {
                token = GenerateToken();
                createUnumuneDto.Token = token;
                SendMail(createUnumuneDto);
            }
            else
            {
                createUnumuneDto.Token = "-";
            }



            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { errors });
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createUnumuneDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            try
            {
                var responseMessage = await client.PostAsync("https://localhost:44344/api/Unumune", stringContent);
                Console.WriteLine(responseMessage);

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return View();
        }

        private string GenerateToken()
        {
            return Guid.NewGuid().ToString("N");
        }

         private SendResponse SendMail(CreateUnumuneDto createUnumuneDto)
        {
            var email = fluentEmail
                .To(new List<Address> {
            new Address { EmailAddress = "furkansumbul1903@gmail.com", Name = "Furkan" }
                })
                .Subject("Konu")
                .UsingTemplateFromFile("Views/EmailTemplates/SariKirmizi.cshtml", createUnumuneDto)
                .Send();

            return email;
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUnumune(int id)
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44344/api/UNumune/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateUnumuneDto>(jsonData);
                return View(values);
            }
            ViewBag.UserId = userId;
            ViewBag.Token = token;
            ViewBag.PersonelSicilNo = userId;
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUnumune(UpdateUnumuneDto updateUnumuneDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateUnumuneDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44344/api/UNumune/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateAmir(string token)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44344/api/Unumune/validate-token?token={token}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var createUnumuneDto = JsonConvert.DeserializeObject<CreateUnumuneDto>(await responseMessage.Content.ReadAsStringAsync());
                return View(createUnumuneDto);
            }
            return RedirectToAction("Error"); 
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAmir(CreateUnumuneDto createUnumuneDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createUnumuneDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44344/api/Unumune/update-amir", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(createUnumuneDto); 
        }
    }
}
