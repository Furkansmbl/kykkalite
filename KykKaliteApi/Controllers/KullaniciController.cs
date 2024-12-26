using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.KullaniciDtos;
using KykKaliteApi.Repositories.FabrikalarRepository;
using KykKaliteApi.Repositories.KullaniciRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciRepository _kullaniciRepository;
        public KullaniciController(IKullaniciRepository kullaniciRepository)
        {
            _kullaniciRepository = kullaniciRepository;
        }
        [HttpGet]
        public async Task<IActionResult> KullaniciList()
        {
            var values = await _kullaniciRepository.GetAllKullaniciAsync();
            return Ok(values);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKullanici(int id)
        {
            _kullaniciRepository.DeleteKullanici(id);
            return Ok("Kullanici Başarılı Bir Şekilde Silindi");
        }
        [HttpPost]
        public async Task<IActionResult> CreateKullanici(CreateKullaniciDto createKullaniciDto)
        {
            _kullaniciRepository.CreateKullanici(createKullaniciDto);
            return Ok("Kullanici Başarılı Bir Şekilde Eklendi");
        }
    }
}
