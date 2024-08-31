using KykKaliteApi.Dtos.UnumuneDtos;
using KykKaliteApi.Dtos.UPatamaDtos;
using KykKaliteApi.Repositories.HMnumuneRepository;
using KykKaliteApi.Repositories.UnumuneRepository;
using KykKaliteApi.Repositories.UPatamaRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UPatamaController : ControllerBase
    {
        private readonly IUPatamaRepository _uPatamaRepository;
        public UPatamaController(IUPatamaRepository uPatamaRepository)
        {
            _uPatamaRepository = uPatamaRepository;
        }
        [HttpGet]
        public async Task<IActionResult> UPatamaList()
        {
            var values = await _uPatamaRepository.GetAllUPatamaAsync();
            return Ok(values);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUPatama(int id)
        {
            _uPatamaRepository.DeleteUPatama(id);
            return Ok("UPatama Başarılı Bir Şekilde Silindi");
        }
        [HttpPost("UpdateUPatama")]
        public async Task<IActionResult> CreateUPatama(CreateUPatamaDto createUPatamaDto)
        {
            _uPatamaRepository.CreateUPatama(createUPatamaDto);
            return Ok("Unumune Başarılı Bir Şekilde Eklendi");
        }
        [HttpPost("UpdateUPatamaPasif")]
        public async Task<IActionResult> CreateUPatamaPasif(CreateUpAtamaPasifDto createUpAtamaPasifDto)
        {
            _uPatamaRepository.CreateUPatamaPasif(createUpAtamaPasifDto);
            return Ok("Unumune Başarılı Bir Şekilde Eklendi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUPatama(UpdateUPatamaDto updateUPatamaDto)
        {
            _uPatamaRepository.UpdateUPatama(updateUPatamaDto);
            return Ok("Unumune Başarıyla Güncellendi");
        }
    }
}
