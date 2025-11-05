using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace HalApi.Tools
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


            //JWT'nin imzalanmasında kullanılacak gizli anahtar (JwtTokenDefault.Key) bir SymmetricSecurityKey nesnesine dönüştürülür.
            //Bu anahtar ve SecurityAlgorithms.HmacSha256 algoritması kullanılarak imzalama işlemi yapılır.
            // issuer: Token'ı oluşturan uygulamanın kimliği.
            //audience: Token'ın geçerli olduğu hedef kitlesi.
            //claims: Token'a eklenen bilgilerin listesi.
            //notBefore: Token'ın ne zaman geçerli olmaya başlayacağı (şu anki zaman).
            //expires: Token'ın geçerlilik süresi (şu anki zaman + JwtTokenDefault.Expire gün).
            //signingCredentials: Token'ın imzalanmasında kullanılacak imzalama bilgileri.


            JwtSecurityToken token = new JwtSecurityToken(
                issuer: JwtTokenDefault.ValidIssuer,
                audience: JwtTokenDefault.ValidAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expireDate,
                signingCredentials: signinCredentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string generatedToken = tokenHandler.WriteToken(token);

            return new TokenResponseViewModel(generatedToken, expireDate, model.AdminUser );
        }
    }
}

