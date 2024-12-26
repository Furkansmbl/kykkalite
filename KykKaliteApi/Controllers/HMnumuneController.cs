using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.HMnumuneDtos;
using KykKaliteApi.Dtos.UnumuneDtos;
using KykKaliteApi.Repositories.HammaddelerRepository;
using KykKaliteApi.Repositories.HMnumuneRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HMnumuneController : ControllerBase
    {
        private readonly IHMnumuneRepository _hMnumuneRepository;
        public HMnumuneController(IHMnumuneRepository hMnumuneRepository)
        {
            _hMnumuneRepository = hMnumuneRepository;
        }
        [HttpGet]
        public async Task<IActionResult> HMnumuneList()
        {
            var values = await _hMnumuneRepository.GetAllHMnumuneAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateHMnumune(CreateHMnumuneDto CreateHMnumuneDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _hMnumuneRepository.CreateHMnumune(CreateHMnumuneDto);
                return Ok("Hmnumune Başarılı Bir Şekilde Eklendi");
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Bir hata oluştu. HmNumune eklenemedi.");
            }
        }
        [HttpPost("Manuel")]
        public async Task<IActionResult> CreateHMnumuneManuel(CreateHMnumuneManuelDto createHMnumuneManuelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _hMnumuneRepository.CreateHMnumuneManuel(createHMnumuneManuelDto);
                return Ok("Hmnumune Başarılı Bir Şekilde Eklendi");
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Bir hata oluştu. HmNumune eklenemedi.");
            }
        }
        [HttpPost("Mail")] 
        public async Task<IActionResult> SentHMnumune([FromBody] CreateHMnumuneDto createHMnumuneDto)
        {
            _hMnumuneRepository.SentHMnumune(createHMnumuneDto);
            return Ok("Unumune Başarılı Bir Şekilde Eklendi");
        }
        [HttpPost("TrendMail")]
        public async Task<IActionResult> TrendMailHmNumune([FromBody] CreateHMnumuneDto createHMnumuneDto)
        {
            _hMnumuneRepository.TrendMailHMnumune(createHMnumuneDto);
            return Ok("Unumune Başarılı Bir Şekilde Eklendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHMnumune(int id)
        {
            _hMnumuneRepository.DeleteHMnumune(id);
            return Ok("HMnumune Başarılı Bir Şekilde Silindi");
        }

        [HttpGet("validate-token")]
        public async Task<IActionResult> ValidateToken(string token)
        {
            var createHMnumuneDto = await _hMnumuneRepository.GetDataByToken(token);
            if (createHMnumuneDto != null)
            {
                return Ok(createHMnumuneDto);
            }
            return NotFound();
        }
        [HttpPut("update-amir")]
        public async Task<IActionResult> UpdateAmir([FromBody] AmirOnayDurumuHMnumuneDto amirOnayDurumuHMnumuneDto )
        {
            await _hMnumuneRepository.UpdateAmir(amirOnayDurumuHMnumuneDto);
            return Ok("Amir Onay Durumu Başarıyla Güncellendi");
        }
    }
}
