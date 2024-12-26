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
        private readonly IGetHmpatamaByHmıdRepository _getHmpatamaByHmıdRepository;

        public GetHmpatamaByhmidController(IGetHmpatamaByHmıdRepository getHmpatamaByHmıdRepository)
        {
            _getHmpatamaByHmıdRepository = getHmpatamaByHmıdRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetHmatamaKodlariByHmIDAsync([FromQuery] string MalzemeAciklamasi, [FromQuery] int fabrikaId, [FromQuery] string THMID, [FromQuery] string Unvani)
        {
            var value = await _getHmpatamaByHmıdRepository.GetHmatamaKodlariByHmIDAsync(MalzemeAciklamasi, fabrikaId, THMID, Unvani);
            return Ok(value);
        }
        [HttpGet("Null")]
        public async Task<IActionResult> GetHmatamaKodlariByHmIDNullAsync([FromQuery] string MalzemeAciklamasi, [FromQuery] int fabrikaId, [FromQuery] string THMID )
        {
            var value = await _getHmpatamaByHmıdRepository.GetHmatamaKodlariByHmIDNullAsync(MalzemeAciklamasi, fabrikaId, THMID);
            return Ok(value);
        }
        [HttpGet("Pa")]
        public async Task<IActionResult> GetKontrolTedarikci()
        {
            var values = await _getHmpatamaByHmıdRepository.GetTedarikci();
            return Ok(values);
        }
    }
}