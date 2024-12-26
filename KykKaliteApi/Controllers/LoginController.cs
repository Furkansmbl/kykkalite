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
            string query = "SELECT * FROM Kullanici WHERE PersonelSicilNo = @personelSicilNo AND Password = @password";
            string query2 = "SELECT PersonelAdiSoyadi, AdminUser, FabrikaId FROM Kullanici WHERE PersonelSicilNo = @personelSicilNo AND Password = @password";

            var parameters = new DynamicParameters();
            parameters.Add("@personelSicilNo", createLoginDto.PersonelSicilNo);
            parameters.Add("@password", createLoginDto.Password);

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<CreateLoginDto>(query, parameters);
                var userDetails = await connection.QueryFirstOrDefaultAsync<GetAppPersonelAdSoyadDtı>(query2, parameters);

                if (user != null && userDetails != null)
                {
                    GetCheckAppUserDto model = new GetCheckAppUserDto
                    {
                        FabrikaId = userDetails.FabrikaId,
                        PersonelSicilNo = user.PersonelSicilNo,
                        PersonelAdiSoyadi = userDetails.PersonelAdiSoyadi,
                        AdminUser = userDetails.AdminUser
                    };

                    var token = JwtTokenGenerator.GenerateToken(model);
                    return Ok(token);
                }
                else
                {
                    return Unauthorized("Invalid login attempt");
                }
            }
        }

    }
}
