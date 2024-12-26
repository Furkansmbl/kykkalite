namespace KykKaliteApi.Dtos.UPNvalueDtos
{
    public class ResultUPNvalueDto
    {
        public int NumuneId { get; set; }
        public int UpnvalueId { get; set; }

        public string UpatamaKodu { get; set; } = null!;

        public string Value { get; set; } = null!;

        public DateTime EklenmeTarihi { get; set; }

        public string PersonelSicilNo { get; set; } = null!;
    }
}
