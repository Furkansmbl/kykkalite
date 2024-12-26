using KykKaliteApi.Dtos.HMPNvalueDtos;
using KykKaliteApi.Dtos.ParametrelerDtos;
using KykKaliteApi.Dtos.UnumuneDtos;
using KykKaliteApi.Repositories.UnumuneRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnumuneController : ControllerBase
    {
        private readonly IUnumuneRepository _unumuneRepository;
        public UnumuneController(IUnumuneRepository unumuneRepository)
        {
            _unumuneRepository = unumuneRepository;
        }
        [HttpGet]
        public async Task<IActionResult> UnumuneList()
        {
            var values = await _unumuneRepository.GetAllUnumuneAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUnumune( CreateUnumuneDto createUnumuneDto)
        {
                _unumuneRepository.CreateUnumune(createUnumuneDto);
                return Ok("Unumune Başarılı Bir Şekilde Eklendi");

        }
        [HttpPost("Manuel")]
        public async Task<IActionResult> CreateUnumuneManuel(CreateUnumuneManuelDto createUnumuneManuelDto )
        {
            _unumuneRepository.CreateUnumuneManuel(createUnumuneManuelDto);
            return Ok("Unumune Başarılı Bir Şekilde Eklendi");

        }
        [HttpPost("Mail")]  
        public async Task<IActionResult> SendUnumune([FromBody] CreateUnumuneDto createUnumuneDto)
        {
             _unumuneRepository.SendUnumune(createUnumuneDto);  
            return Ok("Unumune Başarılı Bir Şekilde Eklendi");
        }
        [HttpPost("TrendMail")] 
        public async Task<IActionResult> TrendMailUnumune([FromBody] CreateUnumuneDto createUnumuneDto)
        {
            _unumuneRepository.TrendMailUnumune(createUnumuneDto);  
            return Ok("Unumune Başarılı Bir Şekilde Eklendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnumune(int id)
        {
            _unumuneRepository.DeleteUnumune(id);
            return Ok("Unumune Başarılı Bir Şekilde Silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUnumune(UpdateUnumuneDto updateUnumuneDto)
        {
            _unumuneRepository.UpdateUnumune(updateUnumuneDto);
            return Ok("Unumune Başarıyla Güncellendi");
        }
        [HttpGet("validate-token")]
        public async Task<IActionResult> ValidateToken(string token)
        {
            var createUnumuneDto = await _unumuneRepository.GetDataByToken(token);
            if (createUnumuneDto != null)
            {
                return Ok(createUnumuneDto);
            }
            return NotFound();
        }
        [HttpPut("update-amir")]
        public async Task<IActionResult> UpdateAmir([FromBody] AmirOnayDurumuUnumuneDto amirOnayDurumuUnumuneDto)
        {
            await _unumuneRepository.UpdateAmir(amirOnayDurumuUnumuneDto);
            return Ok("Amir Onay Durumu Başarıyla Güncellendi");
        }
    }
}
