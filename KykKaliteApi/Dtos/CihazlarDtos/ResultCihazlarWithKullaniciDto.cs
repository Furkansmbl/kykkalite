namespace KykKaliteApi.Dtos.CihazlarDtos
{
    public class ResultCihazlarWithKullaniciDto
    {
        public int CihazId { get; set; }

        public string CihazKodu { get; set; } = null!;

        public string KullanılanCihazEkipman { get; set; } = null!;

        public int FabrikaAdi{ get; set; }

        public string PersonelSicilNo { get; set; } = null!;

        public DateTime EklenmeGuncellenmeTarihi { get; set; }

        public bool KullanımDurumu { get; set; }

    }
}
