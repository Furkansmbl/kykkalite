using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Repositories.FabrikalarRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabrikaController : ControllerBase
    {
        private readonly IFabrikalarRepository _fabrikalarRepository ;
        public FabrikaController(IFabrikalarRepository fabrikalarRepository)
        {
            _fabrikalarRepository = fabrikalarRepository;
        }

        [HttpGet]
        public async Task<IActionResult> FabrikalarList()
        {
            var values = await _fabrikalarRepository.GetAllFabrikaAsync(); 
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFabrika(CreateFabrikaDTO createFabrikaDTO)  
        {
            _fabrikalarRepository.CreateFabrika(createFabrikaDTO);
            return Ok("Fabrika Başarılı Bir Şekilde Eklendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFabrika(int id)
        {
            _fabrikalarRepository.DeleteFabrika(id);
            return Ok("Fabrika Başarılı Bir Şekilde Silindi");
        }
        [HttpPut]
        public async Task<IActionResult> Updatefabrika(UpdateFabrikaDto updateFabrikaDto )
        {
            _fabrikalarRepository.UpdateFabrika(updateFabrikaDto);
            return Ok("Fabrika Başarıyla Güncellendi");
        }
    }
}
