namespace Kykkalite_UI.Models
{
    public class JwtResponseModel
    {
        public string Token { get; set; }
        public string AdminUser { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
