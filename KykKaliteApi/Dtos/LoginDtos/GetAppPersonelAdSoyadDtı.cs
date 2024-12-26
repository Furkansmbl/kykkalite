namespace KykKaliteApi.Dtos.LoginDtos
{
    public class GetAppPersonelAdSoyadDtı
        
    {
        public string PersonelSicilNo { get; set; } = null!;
        public string PersonelAdiSoyadi { get; set; } = null!;
        public string AdminUser { get; set; } = null!;
        public int Password { get; set; }
        public int FabrikaId { get; set; }

    }
}
