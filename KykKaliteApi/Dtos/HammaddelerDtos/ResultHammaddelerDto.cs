namespace KykKaliteApi.Dtos.HammaddelerDtos
{
    public class ResultHammaddelerDto
    {
        public int HammaddeId { get; set; }

        public int HammaddeGrupId { get; set; }

        public string MalzemeKodu { get; set; } = null!;
        public string Hesaplama { get; set; } 
        public string MalzemeAciklamasi { get; set; } = null!;

        public string PersonelSicilNo { get; set; } = null!;

        public DateTime EklenmeGuncellenmeTarihi { get; set; }

        public bool KullanımDurumu { get; set; }
    }
}
