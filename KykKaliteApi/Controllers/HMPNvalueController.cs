using KykKaliteApi.Dtos.HammaddeGruplariDtos;
using KykKaliteApi.Dtos.HammaddelerDtos;
using KykKaliteApi.Dtos.HMPNvalueDtos;
using KykKaliteApi.Repositories.HMnumuneRepository;
using KykKaliteApi.Repositories.HMPNvalueRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HMPNvalueController : ControllerBase
    {

        private readonly IHMPNvalueRepository _hMPNvalueRepository;
        public HMPNvalueController(IHMPNvalueRepository hMPNvalueRepository)
        {
            _hMPNvalueRepository = hMPNvalueRepository;
        }
        [HttpGet]
        public async Task<IActionResult> HMPNvalueList()
        {
            var values = await _hMPNvalueRepository.GetAllHMPatamaAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateHMPNvalue(CreateHMPNvalueDto createHMPNvalueDto)
        {
            _hMPNvalueRepository.CreateHMPNvalue(createHMPNvalueDto);
            return Ok("HMPNvalue Başarılı Bir Şekilde Eklendi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateHMPNvalue(UpdateHMPNvalueDto updateHMPNvalueDto)
        {
            _hMPNvalueRepository.UpdateHMPNvalue(updateHMPNvalueDto);
            return Ok("HMPNvalue Başarıyla Güncellendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHMPNValue(int id)
        {
            _hMPNvalueRepository.DeleteHMPNvalue(id);
            return Ok("HMPNValue Başarılı Bir Şekilde Silindi");
        }
    }
}
