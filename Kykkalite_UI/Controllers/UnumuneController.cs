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
            var responseMessage = await client.GetAsync("http://localhost:44344/api/Unumune");
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
            var responseMessage = await client.GetAsync("http://localhost:44344/api/Unumune");
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
                worksheet.Cell(currentRow, 10).Value = "OlusturmaTarihi";
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
                    worksheet.Cell(currentRow, 10).Value = item.OlusturmaTarihi;
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

            // Trend durumu kontrolü
            bool isTrendFound = false;
            List<int> trendIndices = new List<int>();

            for (int i = 1; i <= 13; i++)
            {
                string trendProperty = $"Trend{i}";
                string valueProperty = $"Value{i}";

                var trendValue = createUnumuneDto.GetType().GetProperty(trendProperty).GetValue(createUnumuneDto)?.ToString();
                var valueValue = createUnumuneDto.GetType().GetProperty(valueProperty).GetValue(createUnumuneDto)?.ToString();

                if (trendValue != null && valueValue != null)
                {
                    string combinedTrendAndValue = CombineTrendAndValue(trendValue, valueValue);
                    string trendResult = CalculateTrend(combinedTrendAndValue);
                    Console.WriteLine($"Trend Result for {trendProperty} and {valueProperty}: {trendResult}");

                    if (trendResult == "Trend var")
                    {
                        isTrendFound = true;
                        trendIndices.Add(i);  // Trend bulunan i değerini listeye ekle
                    }
                }
            }

            // Trend bulunan i değerlerini kaydet
            if (isTrendFound)
            {
                createUnumuneDto.TrendKontrol = string.Join(", ", trendIndices);
                createUnumuneDto.Trend = "Trend Var";
                SendMail1(createUnumuneDto);
            }
            else
            {
                createUnumuneDto.TrendKontrol = "-";
                createUnumuneDto.Trend = "Trend Yok";
            }

            // HTTP isteği gönderimi
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createUnumuneDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            try
            {
                var responseMessage = await client.PostAsync("http://localhost:5185/api/Unumune", stringContent);
                Console.WriteLine(responseMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return View();
        }


        private static string CombineTrendAndValue(string trend, string value)
        {
            // Trend'deki '-' karakterini ',' ile değiştir ve Value'yi ekle
            string modifiedTrend = trend.Replace('-', ',');
            string combinedString = $"{modifiedTrend},{value}";
            return combinedString;
        }

        private static string CalculateTrend(string combinedTrendAndValue)
        {
            // Virgül ile ayır ve double dizisine dönüştür
            double[] trendValues = combinedTrendAndValue.Split(',')
                                                        .Select(v => double.TryParse(v, out var result) ? result : 0)
                                                        .ToArray();

            if (trendValues.Length < 2)
                throw new ArgumentException("Not enough data to calculate trend.");

            var ranks = trendValues.Select((value, index) => new { Value = value, Index = index })
                                   .OrderBy(x => x.Value)
                                   .Select((x, rank) => new { Rank = rank + 1, Index = x.Index })
                                   .ToDictionary(x => x.Index, x => x.Rank);

            double sumSquaredDifferences = trendValues
                .Select((value, index) => Math.Pow(ranks[index] - (index + 1), 2))
                .Sum();

            int n = trendValues.Length;
            double spearmanRho = 1 - (6 * sumSquaredDifferences) / (n * (Math.Pow(n, 2) - 1));

            double zValue = spearmanRho * Math.Sqrt(n - 1);

            if (Math.Abs(zValue) > 2.326347874)
                return "Trend var";
            else
                return "Trend yok";
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
        private SendResponse SendMail1(CreateUnumuneDto createUnumuneDto)
        {
            var email = fluentEmail
                .To(new List<Address> {
            new Address { EmailAddress = "furkansumbul1903@gmail.com", Name = "Furkan" }
                })
                .Subject("Konu")
                .UsingTemplateFromFile("Views/EmailTemplates/TrendMail.cshtml", createUnumuneDto)
                .Send();

            return email;
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUnumune(int id)
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:44344/api/UNumune/{id}");
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
            var responseMessage = await client.PutAsync("http://localhost:44344/api/Unumune", stringContent);
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
            var responseMessage = await client.GetAsync($"http://localhost:44344/api/Unumune/validate-token?token={token}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var createUnumuneDto = JsonConvert.DeserializeObject<CreateUnumuneDto>(await responseMessage.Content.ReadAsStringAsync());
                var isUpdated = await UpdateAmirAsync(createUnumuneDto!);

                return isUpdated ?  View(createUnumuneDto) : RedirectToAction("Error");
            }
            return RedirectToAction("Error"); 
        }
  
        private  async Task<bool> UpdateAmirAsync(CreateUnumuneDto createUnumuneDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createUnumuneDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PutAsync("http://localhost:44344/api/Unumune/update-amir", stringContent);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
          
        }
    }
}
