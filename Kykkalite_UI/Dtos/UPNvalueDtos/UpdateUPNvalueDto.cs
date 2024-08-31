namespace Kykkalite_UI.Dtos.UPNvalueDtos
{
    public class UpdateUPNvalueDto
    {
         public int UpnvalueId { get; set; }

        public string UpatamaKodu { get; set; } = null!;
        public int NumuneId { get; set; }

        public string Value { get; set; } = null!;
        public string OlusturmaTarihi { get; set; }
        public DateTime GuncellenmeTarihi { get; set; }
        public string Versiyon { get; set; }

        public string PersonelSicilNo { get; set; } = null!;
    }
}
