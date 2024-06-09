namespace KykKaliteApi.Dtos.KullaniciDtos
{
    public class ResultKullaniciDto
    {
        public string PersonelSicilNo { get; set; } = null!;

        public string PersonelAdiSoyadi { get; set; } = null!;

        public int FabrikaId { get; set; }

        public string Gorevi { get; set; } = null!;

        public int AdminUser { get; set; }

        public int Password { get; set; } 

        public DateTime EklenmeGuncellenmeTarihi { get; set; }

        public bool KullanimDurumu { get; set; }
    }
}
