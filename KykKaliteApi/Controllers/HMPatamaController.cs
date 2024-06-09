using KykKaliteApi.Dtos.HammaddelerDtos;
using KykKaliteApi.Dtos.HMnumuneDtos;
using KykKaliteApi.Dtos.HMPatamaDtos;
using KykKaliteApi.Repositories.HMPatamaRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HMPatamaController : ControllerBase
    {
        private readonly IHMPatamaRepository _hMPatamaRepository;
        public HMPatamaController(IHMPatamaRepository hMPatamaRepository)
        {
            _hMPatamaRepository = hMPatamaRepository;
        }
        [HttpGet]
        public async Task<IActionResult> HMPatamaList()
        {
            var values = await _hMPatamaRepository.GetAllHMPatamaAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateHMPatama(CreateHMPatamaDto createHMPatamaDto)
        {
            _hMPatamaRepository.CreateHMPatama(createHMPatamaDto);
            return Ok("HMPatama Başarılı Bir Şekilde Eklendi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateHMPatama(UpdateHMPatamaDto updateHMPatamaDto)
        {
            _hMPatamaRepository.UpdateHMPatama(updateHMPatamaDto);
            return Ok("Hammadde ataması Başarıyla Güncellendi");
        }
    }
}
