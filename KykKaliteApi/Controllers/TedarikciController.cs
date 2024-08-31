using KykKaliteApi.Dtos.CihazlarDtos;
using KykKaliteApi.Dtos.TedarikciDtos;
using KykKaliteApi.Repositories.TedarikciRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TedarikciController : ControllerBase
    {

            private readonly ITedarikciRepository _tedarikciRepository;
            public TedarikciController(ITedarikciRepository tedarikciRepository)
            {
                _tedarikciRepository = tedarikciRepository;
            }
            [HttpGet]
            public async Task<IActionResult> TedarikciList()
            {
                var values = await _tedarikciRepository.GetAllTedarikciAsync();
                return Ok(values);
            }
        [HttpPost]
        public async Task<IActionResult> CreateCihazlar(ResultTedarikciDto resultTedarikciDto)
        {
            _tedarikciRepository.CreateTedarikci(resultTedarikciDto);
            return Ok("Tedarikci Başarılı Bir Şekilde Eklendi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTedarikci(UpdateTedarikciDto updateTedarikciDto)
        {
            _tedarikciRepository.UpdateTedarikci(updateTedarikciDto);
            return Ok("Tedarikci Başarıyla Güncellendi");
        }
    }
 }


