namespace HalApi.Dtos
{
    public class Kullanici
    {
        public string PersonelSicilNo { get; set; } = null!;

        public string PersonelAdiSoyadi { get; set; } = null!;

        public string Gorevi { get; set; } = null!;

        public string AdminUser { get; set; } = null!;

        public int Password { get; set; }

        public DateTime EklenmeGuncellenmeTarihi { get; set; }

        public bool KullanimDurumu { get; set; }
    }
}
