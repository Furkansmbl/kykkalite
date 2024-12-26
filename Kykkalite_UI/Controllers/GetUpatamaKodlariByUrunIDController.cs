using Microsoft.AspNetCore.Mvc;
using Kykkalite_UI.Dtos.GetUpatamaKodlariByUrunIDDtos;

namespace Kykkalite_UI.Controllers
{
    public class GetUpatamaKodlariByUrunIDController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GetUpatamaKodlariByUrunIDController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        public class QueryViewModel
        {
            public List<ResultGetUpatamaKodlariByUrunIDDto> ResultSet { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Index(int numuneId, int urunId)
        {
            var client = _httpClientFactory.CreateClient();


            var response = await client.GetAsync($"https://localhost:44344/api/GetUpatamaKodlariByUrunID/{urunId}/?urunId={urunId}&numuneId={numuneId}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ResultGetUpatamaKodlariByUrunIDDto>>();

                var viewModel = new QueryViewModel
                {
                    ResultSet = result
                };

                return View(viewModel);
            }
            else
            {
                return View(new QueryViewModel());
            }
        }
    }
}

