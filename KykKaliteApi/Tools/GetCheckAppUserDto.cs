namespace HalApi.Tools
{
    public class GetCheckAppUserDto
    {
        public string PersonelSicilNo { get; set; } = null!;
        public int Password { get; set; }
        public string AdminUser { get; set; }
        public string IsExist { get; set; }

        public string PersonelAdiSoyadi { get; set; } = null!;
    }
}
