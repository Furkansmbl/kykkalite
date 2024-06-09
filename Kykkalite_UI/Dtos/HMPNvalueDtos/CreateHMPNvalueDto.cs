namespace Kykkalite_UI.Dtos.HMPNvalueDtos
{
    public class CreateHMPNvalueDto
    {
        public int HmpnvalueId { get; set; }

        public string HmpatamaKodu { get; set; } = null!;
        public int NumuneId { get; set; }


        public string Value { get; set; } = null!;

        public DateTime EklenmeTarihi { get; set; }

        public string PersonelSicilNo { get; set; } = null!;
    }
}
