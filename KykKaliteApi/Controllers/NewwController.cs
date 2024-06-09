using KykKaliteApi.Repositories.CihazlarRepository;
using KykKaliteApi.Repositories.NewRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewwController : ControllerBase
    {
        private readonly INewwRepository _newwRepository;
        public NewwController(INewwRepository newwRepository)
        {
            _newwRepository = newwRepository;
        }
        [HttpGet]
        public async Task<IActionResult> NewList()
        {
            var values = await _newwRepository.GetAllNewlerAsync();
            return Ok(values);
        }
    }
}
