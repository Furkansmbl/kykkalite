namespace KykKaliteApi.Dtos.HMPNvalueDtos
{
    public class CreateHMPNvalueDto
    {

        public string HmpatamaKodu { get; set; } = null!;

        public string Value { get; set; } = null!;

        public DateTime EklenmeTarihi { get; set; }

        public string PersonelSicilNo { get; set; } = null!;
    }
}
