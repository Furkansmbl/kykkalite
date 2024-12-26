using KykKaliteApi.Repositories.CihazlarRepository;
using KykKaliteApi.Repositories.GetUpatamaKodlariByUrunIDRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetUpatamaKodlariByUrunIDController : ControllerBase
    {
        private readonly IGetUpatamaKodlariByUrunIDRepository _getUpatamaKodlariByUrunIDRepository;

        public GetUpatamaKodlariByUrunIDController(IGetUpatamaKodlariByUrunIDRepository getUpatamaKodlariByUrunIDRepository)
        {
            _getUpatamaKodlariByUrunIDRepository = getUpatamaKodlariByUrunIDRepository;
        }
    }
}
