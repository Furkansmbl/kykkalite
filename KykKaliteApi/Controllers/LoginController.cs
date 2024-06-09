using Dapper;
using KykKaliteApi.Dtos.LoginDtos;
using KykKaliteApi.Models.DapperContext;
using KykKaliteApi.Tools;
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
            string query = "Select * From Kullanici Where PersonelSicilNo=@personelSicilNo and Password=@password";
            string query2 = "Select PersonelAdiSoyadi From Kullanici Where PersonelSicilNo=@personelSicilNo and Password=@password";
            var parameters = new DynamicParameters();
            parameters.Add("@personelSicilNo", createLoginDto.PersonelSicilNo);
            parameters.Add("@password", createLoginDto.Password);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<CreateLoginDto>(query, parameters);
                var values2 = await connection.QueryFirstAsync<GetAppPersonelAdSoyadDtı>(query2, parameters);

                if (values != null)
                {
                    GetCheckAppUserDto model = new GetCheckAppUserDto();
                    model.PersonelSicilNo = values.PersonelSicilNo;
                    model.PersonelAdiSoyadi =values2.PersonelAdiSoyadi;
                    var token = JwtTokenGenerator.GenerateToken(model);
                    return Ok(token);
                }

                else
                {
                    return Ok("Başarısız");
                }


            }
        }
    }
}
