namespace KykKaliteApi.Dtos.UrunlerDtos
{
    public class CreateUrunlerDto
    {

        public int UrunGrupId { get; set; }

        public string MalzemeKodu { get; set; } = null!;

        public string MalzemeAciklamasi { get; set; } = null!;

        public string PersonelSicilNo { get; set; } = null!;

        public DateTime EklenmeGuncellenmeTarihi { get; set; }

        public bool KullanimDurumu { get; set; }
    }
}
