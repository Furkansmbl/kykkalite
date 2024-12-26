using KykKaliteApi.Repositories.DashboardRepository;
using KykKaliteApi.Repositories.FabrikalarRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardController(IDashboardRepository dashboardRepository )
        {
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDegKatAsync()
        {
            var values = await _dashboardRepository.GetAllDegKatAsync();
            return Ok(values);
        }
        [HttpGet("hm")]
        public async Task<IActionResult> GetAllDegKatHmAsync()
        {
            var values = await _dashboardRepository.GetAllDegKatHmAsync();
            return Ok(values);
        }
        [HttpGet("fo")]
        public async Task<IActionResult> GetAllFabrikaOnayAsync()
        {
            var values = await _dashboardRepository.GetAllFabrikaOnayAsync();
            return Ok(values);
        }
        [HttpGet("foHammadde")]
        public async Task<IActionResult> GetAllFabrikaOnayHammaddeAsync()
        {
            var values = await _dashboardRepository.GetAllFabrikaOnayHammaddeAsync();
            return Ok(values);
        }
        [HttpGet("uo")]
        public async Task<IActionResult> GetAllUrunOranAsync()
        {
            var values = await _dashboardRepository.GetAllUrunOranAsync();
            return Ok(values);
        }
        [HttpGet("tedred")]
        public async Task<IActionResult> GetAllTedarikciRedHammaddeAsync()
        {
            var values = await _dashboardRepository.GetAllTedarikciRedHammaddeAsync();
            return Ok(values);
        }
        [HttpGet("tedSartli")]
        public async Task<IActionResult> GetAllTedarikciSartliOnayHammaddeAsync()
        {
            var values = await _dashboardRepository.GetAllTedarikciSartliOnayHammaddeAsync();
            return Ok(values);
        }
    }
}
