using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.HammaddeGruplariDtos;
using KykKaliteApi.Repositories.FabrikalarRepository;
using KykKaliteApi.Repositories.HammaddeGruplariRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HammaddeGruplariController : ControllerBase
    {
        private readonly IHammaddeGruplariRespository _hammaddeGruplariRespository ;
        public HammaddeGruplariController(IHammaddeGruplariRespository hammaddeGruplariRespository)
        {
            _hammaddeGruplariRespository = hammaddeGruplariRespository;
        }
        [HttpGet]
        public async Task<IActionResult> HammaddeGruplariList()
        {
            var values = await _hammaddeGruplariRespository.GetAllHammaddeGruplari();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateHammaddeGruplari(CreateHammaddeGruplariDto createHammaddeGruplariDto)
        {
            _hammaddeGruplariRespository.CreateHammaddeGruplari(createHammaddeGruplariDto);
            return Ok("Hammadde Grubu Başarılı Bir Şekilde Eklendi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateHammaddeGruplari(UpdateHammaddeGruplariDto updateHammaddeGruplariDto)
        {
            _hammaddeGruplariRespository.UpdateHammaddeGruplari(updateHammaddeGruplariDto);
            return Ok("Hammadde Grubu Başarıyla Güncellendi");
        }
    }
}
