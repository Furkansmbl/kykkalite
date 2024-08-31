namespace Kykkalite_UI.Dtos.UrunGruplariDtos
{
    public class UpdateUrunGruplariDto
    {
        public DateTime EklenmeGuncellenmeTarihi { get; set; }
        public int UrunGrupId { get; set; }

        public string UgrupAdi { get; set; } = null!;
    }
}
