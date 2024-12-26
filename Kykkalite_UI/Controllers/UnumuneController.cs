using ClosedXML.Excel;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Smtp;
using Kykkalite_UI.Dtos.UnumuneDtos;
using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using NuGet.Common;
using System.Text;
using Dapper;
using Kykkalite_UI.Dtos.FabrikalarDtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using KykKaliteApi.Dtos.HMnumuneDtos;
using Kykkalite_UI.Dtos.HMnumuneDtos;
namespace Kykkalite_UI.Controllers
{
    public class UnumuneController : Controller
    {
        private readonly ILogger<UnumuneController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IFluentEmail fluentEmail;
        private readonly IHttpContextAccessor _contextAccessor;
        public UnumuneController(ILogger<UnumuneController> logger,IHttpClientFactory httpClientFactory, ILoginService loginService, IHttpContextAccessor contextAccessor, IFluentEmail fluentEmail)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
            this.fluentEmail = fluentEmail;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5185/api/Unumune");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultUnumuneDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        public async Task<IActionResult> DeleteUnumune(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var reponseMessage = await client.DeleteAsync($"http://localhost:5185/api/Unumune/{id}");
            if (reponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5185/api/Unumune");
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
                    worksheet.Cell(currentRow, 11).Value = item.UnPersonel;

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
        public async Task<IActionResult> CreateUnumuneManuel()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUnumuneManuel(CreateUnumuneManuelDto createUnumuneManuelDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createUnumuneManuelDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            try
            {
                var responseMessage = await client.PostAsync("http://localhost:5185/api/Unumune/Manuel", stringContent);
                Console.WriteLine(responseMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return View();
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

                if (createUnumuneDto.red > 0)
                {
                    createUnumuneDto.AmirOnayDurumu = "Red";
                    createUnumuneDto.OnayDurumu = true; 
                }
                else if (createUnumuneDto.yellow > 0)
                {
                    createUnumuneDto.AmirOnayDurumu = "SartliOnay";
                    createUnumuneDto.OnayDurumu = true;

                }
                    SendMail(createUnumuneDto);

                
            }
            else
            {
                createUnumuneDto.Token = "-";
                createUnumuneDto.AmirOnayDurumu = "1";
                createUnumuneDto.OnayDurumu = true;

            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { errors });
            }

            // Trend durumu kontrolü
            bool isTrendFound = false;
            List<string> trendParameters = new List<string>();
            List<string> trendResults = new List<string>();

            for (int i = 1; i <= 13; i++)
            {
                string trendProperty = $"Trend{i}";
                string valueProperty = $"Value{i}";
                string kontrolParametresiProperty = $"KontrolParametresi{i}";

                var trendValue = createUnumuneDto.GetType().GetProperty(trendProperty)?.GetValue(createUnumuneDto)?.ToString();
                var valueValue = createUnumuneDto.GetType().GetProperty(valueProperty)?.GetValue(createUnumuneDto)?.ToString();
                var kontrolParametresiValue = createUnumuneDto.GetType().GetProperty(kontrolParametresiProperty)?.GetValue(createUnumuneDto)?.ToString();

                if (trendValue != null && valueValue != null && kontrolParametresiValue != null)
                {
                    string combinedTrendAndValue = CombineTrendAndValue(trendValue, valueValue);
                    string trendResult = CalculateTrend(combinedTrendAndValue);
                    Console.WriteLine($"Trend Result for {trendProperty} and {valueProperty}: {trendResult}");

                    // Sadece pozitif veya negatif trend varsa ekleyin
                    if (trendResult == "Pozitif Trend var" || trendResult == "Negatif Trend var")
                    {
                        trendResults.Add($"{kontrolParametresiValue}:{trendResult}");
                        isTrendFound = true;
                        trendParameters.Add(kontrolParametresiValue);
                    }
                }
            }

            // Trend bulunan i değerlerini kaydet
            if (isTrendFound)
            {
                createUnumuneDto.TrendKontrol = string.Join(", ", trendParameters);
                createUnumuneDto.Trend = "Trend Var";
                createUnumuneDto.TrendYonu = string.Join(", ", trendResults); // Trend sonuçlarını kaydet
                SendMailTrend(createUnumuneDto);
            }
            else
            {
                createUnumuneDto.TrendKontrol = "-";
                createUnumuneDto.Trend = "Trend Yok";
                createUnumuneDto.TrendYonu = "-"; // Trend sonuçları olmadığını belirle
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
            string modifiedTrend = trend.Replace('-', '.');
            string combinedString = $"{modifiedTrend}.{value}";
            return combinedString;
        }

        private static string CalculateTrend(string combinedTrendAndValue)
        {
            var parts = combinedTrendAndValue.Split('.');
            if (parts.Distinct().Count() == 1)
                return "Trend yok";

            double[] trendValues = parts.Select(v => double.TryParse(v, out var result) ? result : 0).ToArray();

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
            if (Math.Abs(zValue) <= 2.326347874)
                return "Trend Yok";

            else if (Math.Abs(zValue) >= 0 && (zValue) >= -2.326347874)
                return "Negatif Trend var";
            else
                return "Pozitif Trend var";
        }
    

        private string GenerateToken()
        {
            return Guid.NewGuid().ToString("N");
        }

        private async Task SendMail(CreateUnumuneDto createUnumuneDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createUnumuneDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("http://localhost:5185/api/Unumune/Mail", stringContent);
            Console.WriteLine(responseMessage);
        }
        private async Task SendMailTrend(CreateUnumuneDto createUnumuneDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createUnumuneDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("http://localhost:5185/api/Unumune/TrendMail", stringContent);
            Console.WriteLine(responseMessage);
        }



        [HttpGet]
        public async Task<IActionResult> UpdateUnumune(int id)
        {
            var userId = _loginService.GetPersonelSicilNo;
            var token = User.Claims.FirstOrDefault(x => x.Type == "ipktoken")?.Value;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5185/api/UNumune/{id}");
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
            var responseMessage = await client.PutAsync("http://localhost:5185/api/Unumune", stringContent);
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
            var responseMessage = await client.GetAsync($"http://localhost:5185/api/Unumune/validate-token?token={token}");
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
                var responseMessage = await client.PutAsync("http://localhost:5185/api/Unumune/update-amir", stringContent);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
          
        }
    }
}
