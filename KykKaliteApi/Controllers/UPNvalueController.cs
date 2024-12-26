using KykKaliteApi.Dtos.UPatamaDtos;
using KykKaliteApi.Dtos.UPNvalueDtos;
using KykKaliteApi.Repositories.HMPatamaRepository;
using KykKaliteApi.Repositories.UPatamaRepository;
using KykKaliteApi.Repositories.UPNvalueRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UPNvalueController : ControllerBase
    {
        private readonly IUPNvalueRepository _uPNvalueRepository ;
        public UPNvalueController(IUPNvalueRepository uPNvalueRepository)
        {
            _uPNvalueRepository = uPNvalueRepository;
        }
        [HttpGet]
        public async Task<IActionResult> UPNvalueList()
        {
            var values = await _uPNvalueRepository.GetAllUPNvalueAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUPNvalue(CreateUPNvalueDto createUPNvalueDto)
        {
            _uPNvalueRepository.CreateUPNvalue(createUPNvalueDto);
            return Ok("UPN VALUE Başarılı Bir Şekilde Eklendi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUPNvalue(UpdateUPNvalueDto updateUPNvalueDto )
        {
            _uPNvalueRepository.UpdateUPNvalue(updateUPNvalueDto);
            return Ok("UPN VALUE Başarıyla Güncellendi");
        }
    }
}
