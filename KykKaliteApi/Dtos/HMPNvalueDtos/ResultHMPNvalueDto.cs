namespace KykKaliteApi.Dtos.HMPNvalueDtos
{
    public class ResultHMPNvalueDto
    {
        public int HmpnvalueId { get; set; }

        public string HmpatamaKodu { get; set; } = null!;

        public string Value { get; set; } = null!;

        public DateTime EklenmeTarihi { get; set; }

        public string PersonelSicilNo { get; set; } = null!;
        public int NumuneId { get; set; }

    }
}
