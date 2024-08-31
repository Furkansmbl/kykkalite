namespace KykKaliteApi.Dtos.UrunGruplariDtos
{
    public class ResultUrunGruplariDto
    {
        public int UrunGrupId { get; set; }

        public DateTime EklenmeGuncellenmeTarihi { get; set; }
        public string UgrupAdi { get; set; } = null!;
    }
}
