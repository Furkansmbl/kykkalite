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
            if (!string.IsNullOrWhiteSpace(model.AdminUser))
            {
                claims.Add(new Claim(ClaimTypes.Role, model.AdminUser));
            }

            claims.Add(new Claim(ClaimTypes.NameIdentifier, model.PersonelSicilNo.ToString()));

            if (!string.IsNullOrWhiteSpace(model.PersonelSicilNo))
                claims.Add(new Claim("PersonelSicilNo", model.PersonelSicilNo));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefault.Key));
            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefault.Expire);
            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtTokenDefault.ValidIssuer, audience: JwtTokenDefault.ValidAudience, claims: claims, notBefore: DateTime.UtcNow, expires: expireDate, signingCredentials: signinCredentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return new TokenResponseViewModel(tokenHandler.WriteToken(token), expireDate);
        }
    }
}
