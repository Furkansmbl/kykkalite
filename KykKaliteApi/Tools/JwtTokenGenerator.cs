using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace KykKaliteApi.Tools
{
    public class JwtTokenGenerator
    {
        public static TokenResponseViewModel GenerateToken(GetCheckAppUserDto model)
        {
            var claims = new List<Claim>();

            // Determine if the user is an Admin
            bool isAdmin = !string.IsNullOrWhiteSpace(model.AdminUser) && model.AdminUser == "1";
            if (isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }

            claims.Add(new Claim(ClaimTypes.NameIdentifier, model.PersonelSicilNo.ToString()));

            if (!string.IsNullOrWhiteSpace(model.PersonelSicilNo))
                claims.Add(new Claim("PersonelSicilNo", model.PersonelSicilNo));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefault.Key));
            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefault.Expire);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: JwtTokenDefault.ValidIssuer,
                audience: JwtTokenDefault.ValidAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expireDate,
                signingCredentials: signinCredentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string generatedToken = tokenHandler.WriteToken(token);

            return new TokenResponseViewModel(generatedToken, expireDate, model.AdminUser);
        }
    }
}

