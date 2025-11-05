namespace HalApi.Tools
{
    public class TokenResponseViewModel
    {
        public TokenResponseViewModel(string token, DateTime expireDate,string adminUser )
        {
            AdminUser = adminUser;
            Token = token;
            ExpireDate = expireDate;
        }

        public string Token { get; set; }
        public string AdminUser { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}

