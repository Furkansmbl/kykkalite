namespace KykKaliteApi.Dtos.UrunlerDtos
{
    public class UpdateUrunlerDto
    {
        public int UrunId { get; set; }

        public int UrunGrupId { get; set; }

        public string MalzemeKodu { get; set; } = null!;


        public string MalzemeAciklamasi { get; set; } = null!;

        public string PersonelSicilNo { get; set; } = null!;

        public DateTime EklenmeGuncellenmeTarihi { get; set; }

        public bool KullanimDurumu { get; set; }
    }
}
