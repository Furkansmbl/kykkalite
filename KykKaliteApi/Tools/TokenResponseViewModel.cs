namespace KykKaliteApi.Tools
{
    public class TokenResponseViewModel
    {
        public TokenResponseViewModel(string token, DateTime expireDate,string adminUser , int fabrikaId)
        {
            FabrikaId = fabrikaId;
            AdminUser = adminUser;
            Token = token;
            ExpireDate = expireDate;
        }

        public string Token { get; set; }
        public string AdminUser { get; set; }
        public int FabrikaId { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}

