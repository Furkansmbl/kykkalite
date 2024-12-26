using Dapper;
using KykKaliteApi.Dtos.LoginDtos;
using KykKaliteApi.Models.DapperContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Context _context;
        public LoginController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(CreateLoginDto createLoginDto)
        {
            string query = "Select * From KUllanici Where PersonelSicilNo=@personelSicilNo and Password=@password";
            var parameters = new DynamicParameters();
            parameters.Add("@personelSicilNo", createLoginDto.PersonelSicilNo);
            parameters.Add("@password", createLoginDto.Password);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<CreateLoginDto>(query, parameters);

                if (values != null)
                {
                    return Ok("Başarılı");
                }
                else
                {
                    return Ok("Başarısız");
                }
            }
        }
    }
}
