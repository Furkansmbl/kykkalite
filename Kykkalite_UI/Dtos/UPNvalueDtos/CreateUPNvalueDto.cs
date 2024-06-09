namespace Kykkalite_UI.Dtos.UPNvalueDtos
{
    public class CreateUPNvalueDto
    {
        public int UpnvalueId { get; set; }

        public string UpatamaKodu { get; set; } = null!;
        public int NumuneId { get; set; }

        public string Value { get; set; } = null!;

        public DateTime EklenmeTarihi { get; set; }

        public string PersonelSicilNo { get; set; } = null!;
    }
}
