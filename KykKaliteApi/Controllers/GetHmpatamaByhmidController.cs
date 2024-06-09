using KykKaliteApi.Repositories.GetHmpatamaByHmIdRepository;
using KykKaliteApi.Repositories.GetUpatamaKodlariByUrunIDRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetHmpatamaByhmidController : ControllerBase
    {
        private readonly IGetHmpatamaByHmıdRepository _getHmpatamaByHmıdRepository ;

        public GetHmpatamaByhmidController(IGetHmpatamaByHmıdRepository getHmpatamaByHmıdRepository )
        {
            _getHmpatamaByHmıdRepository = getHmpatamaByHmıdRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetUpatamaKodlariByUrunIDA([FromQuery] string MalzemeAciklamasi)
        {
            var value = await _getHmpatamaByHmıdRepository.GetHmatamaKodlariByHmIDAsync(MalzemeAciklamasi);
            return Ok(value);
        }
    }
}
