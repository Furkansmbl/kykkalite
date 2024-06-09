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
                // Log and return the error view
                return BadRequest(ModelState);
            }

            try
            {
                _hMnumuneRepository.CreateHMnumune(CreateHMnumuneDto);
                return Ok("Hmnumune Başarılı Bir Şekilde Eklendi");
            }
            catch (Exception ex)
            {
                // Log the exception
                // You can also return a more specific error message if needed
                return StatusCode(500, "Bir hata oluştu. HmNumune eklenemedi.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHMnumune(int id)
        {
            _hMnumuneRepository.DeleteHMnumune(id);
            return Ok("HMnumune Başarılı Bir Şekilde Silindi");
        }
    }
}
