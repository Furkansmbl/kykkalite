namespace KykKaliteApi.Dtos.KullaniciDtos
{
    public class ResultKullaniciDto
    {
        public string PersonelSicilNo { get; set; } = null!;

        public string PersonelAdiSoyadi { get; set; } = null!;

        public int FabrikaId { get; set; }

        public string Gorevi { get; set; } = null!;

        public bool AdminUser { get; set; }

        public byte[] Password { get; set; } = null!;

        public DateTime EklenmeGuncellenmeTarihi { get; set; }

        public bool KullanimDurumu { get; set; }
    }
}
