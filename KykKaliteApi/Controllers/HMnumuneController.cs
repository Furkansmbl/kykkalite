using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.HMnumuneDtos;
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
        public async Task<IActionResult> CreateHMnumune(CreateHMnumuneDto createHMnumuneDto)
        {
            _hMnumuneRepository.CreateHMnumune(createHMnumuneDto);
            return Ok("HMnumune Başarılı Bir Şekilde Eklendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHMnumune(int id)
        {
            _hMnumuneRepository.DeleteHMnumune(id);
            return Ok("HMnumune Başarılı Bir Şekilde Silindi");
        }
    }
}
