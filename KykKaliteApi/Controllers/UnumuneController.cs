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
        public UnumuneController(IUnumuneRepository unumuneRepository )
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
        public async Task<IActionResult> CreateUnumune(CreateUnumuneDto createUnumuneDto)
        {
            _unumuneRepository.CreateUnumune(createUnumuneDto);
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
    }
}
