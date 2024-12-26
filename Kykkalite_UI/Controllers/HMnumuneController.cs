using FluentEmail.Core.Models;
using FluentEmail.Core;
using Kykkalite_UI.Dtos.HMnumuneDtos;
using Kykkalite_UI.Dtos.HammaddelerDtos;
using Kykkalite_UI.Dtos.HMnumuneDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Kykkalite_UI.Services;
using DocumentFormat.OpenXml.Presentation;


namespace Kykkalite_UI.Controllers
{
    public class HMnumuneController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IFluentEmail fluentEmail;
        private readonly IHttpContextAccessor _contextAccessor;
        public HMnumuneController(IHttpClientFactory httpClientFactory, ILoginService loginService, IHttpContextAccessor contextAccessor, IFluentEmail fluentEmail)
        {

            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
            this.fluentEmail = fluentEmail;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5185/api/HMnumune");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultHMnumuneDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        public async Task<IActionResult> DeleteHMnumune(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var reponseMessage = await client.DeleteAsync($"http://localhost:5185/api/HMnumune/{id}");
            if (reponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateHMnumuneManuel()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateHMnumuneManuel(CreateHMnumuneManuelDto createHMnumuneManuelDto )
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createHMnumuneManuelDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            try
            {
                var responseMessage = await client.PostAsync("http://localhost:5185/api/HMnumune/Manuel", stringContent);
                Console.WriteLine(responseMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return View();
        }
        [HttpGet]
        public IActionResult CreateHMnumune()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateHMnumune([FromBody] CreateHMnumuneDto createHMnumuneDto)
        {

            string token = createHMnumuneDto.Token;

            if (string.IsNullOrEmpty(createHMnumuneDto.Token) && (createHMnumuneDto.red > 0 || createHMnumuneDto.yellow > 0))
            {
                token = GenerateToken();
                createHMnumuneDto.Token = token;

                if (createHMnumuneDto.red > 0)
                {
                    createHMnumuneDto.AmirOnayDurumu = "Red";
                    createHMnumuneDto.OnayDurumu = true;
                }
                else if (createHMnumuneDto.yellow > 0)
                {
                    createHMnumuneDto.AmirOnayDurumu = "SartliOnay";
                    createHMnumuneDto.OnayDurumu = true;

                }

                 SendMail(createHMnumuneDto);
            }
            else
            {
                createHMnumuneDto.Token = "-";
                createHMnumuneDto.AmirOnayDurumu = "1";
                createHMnumuneDto.OnayDurumu = true;

            }
            bool isTrendFound = false;
            List<string> trendParameters = new List<string>();
            List<string> trendResults = new List<string>();

            for (int i = 1; i <= 13; i++)
            {
                string trendProperty = $"Trend{i}";
                string valueProperty = $"Value{i}";
                string kontrolParametresiProperty = $"KontrolParametresi{i}";

                var trendValue = createHMnumuneDto.GetType().GetProperty(trendProperty)?.GetValue(createHMnumuneDto)?.ToString();
                var valueValue = createHMnumuneDto.GetType().GetProperty(valueProperty)?.GetValue(createHMnumuneDto)?.ToString();
                var kontrolParametresiValue = createHMnumuneDto.GetType().GetProperty(kontrolParametresiProperty)?.GetValue(createHMnumuneDto)?.ToString();

                if (trendValue != null && valueValue != null && kontrolParametresiValue != null)
                {
                    string combinedTrendAndValue = CombineTrendAndValue(trendValue, valueValue);
                    string trendResult = CalculateTrend(combinedTrendAndValue);
                    Console.WriteLine($"Trend Result for {trendProperty} and {valueProperty}: {trendResult}");

                    if (trendResult == "Pozitif Trend var" || trendResult == "Negatif Trend var")
                    {
                        trendResults.Add($"{kontrolParametresiValue}:{trendResult}");
                        isTrendFound = true;
                        trendParameters.Add(kontrolParametresiValue);
                    }
                }
            }

            if (isTrendFound)
            {
                createHMnumuneDto.TrendKontrol = string.Join(", ", trendParameters);
                createHMnumuneDto.Trend = "Trend Var";
                createHMnumuneDto.TrendYonu = string.Join(", ", trendResults); 
                SendMailTrend(createHMnumuneDto);
            }
            else
            {
                createHMnumuneDto.TrendKontrol = "-";
                createHMnumuneDto.Trend = "Trend Yok";
                createHMnumuneDto.TrendYonu = "-"; 
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createHMnumuneDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            try
            {
                var responseMessage = await client.PostAsync("http://localhost:5185/api/HMnumune", stringContent);
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
                return  "Pozitif Trend var";
        }
        private async Task SendMail(CreateHMnumuneDto createHMnumuneDto )
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createHMnumuneDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("http://localhost:5185/api/HMnumune/Mail", stringContent);
            Console.WriteLine(responseMessage);
        }
        private async Task SendMailTrend(CreateHMnumuneDto createHMnumuneDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createHMnumuneDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("http://localhost:5185/api/HMnumune/TrendMail", stringContent);
            Console.WriteLine(responseMessage);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateHMnumune(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5185/api/HMnumune/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateHMnumuneDto>(jsonData);
                return View(values);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateHMnumune(UpdateHMnumuneDto updateHMnumuneDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateHMnumuneDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:5185/api/HMnumune/", stringContent);
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
            var responseMessage = await client.GetAsync($"http://localhost:5185/api/HMnumune/validate-token?token={token}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var createHMnumuneDto = JsonConvert.DeserializeObject<CreateHMnumuneDto>(await responseMessage.Content.ReadAsStringAsync());
                var isUpdated = await UpdateAmirAsync(createHMnumuneDto!);

                return isUpdated ? View(createHMnumuneDto) : RedirectToAction("Error");
            }
            return RedirectToAction("Error");
        }

        private async Task<bool> UpdateAmirAsync(CreateHMnumuneDto createHMnumuneDto )
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createHMnumuneDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PutAsync("http://localhost:5185/api/HMnumune/update-amir", stringContent);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
