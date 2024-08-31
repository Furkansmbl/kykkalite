namespace Kykkalite_UI.Dtos.ParametrelerDtos
{
    public class ResultParametreDto
    {
        public int ParametreID { get; set; }
        public string ParametreKodu { get; set; } = null!;

        public string KontrolParametresi { get; set; } = null!;

        public string ParametreTipiOlcmeGozlem { get; set; }

        public string Birimi { get; set; } = null!;

        public string PersonelSicilNo { get; set; } = null!;

        public DateTime OlusturmaTarihi { get; set; }
        public DateTime GuncellenmeTarihi { get; set; }

        public bool KullanimDurumu { get; set; }
    }
}
