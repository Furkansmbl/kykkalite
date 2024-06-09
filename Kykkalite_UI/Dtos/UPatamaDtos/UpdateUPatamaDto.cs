namespace Kykkalite_UI.Dtos.UPatamaDtos
{
    public class UpdateUPatamaDto
    {
        public int UpaId { get; set; }

        public string ParametreKodu { get; set; } = null!;

        public int UrunId { get; set; }

        public string UpatamaKodu { get; set; } = null!;

        public bool ParametreKritiklikSeviyesi { get; set; }

        public double KontrolDegeriNominal { get; set; }

        public double AltOnaySiniri { get; set; }

        public double UstOnaySiniri { get; set; }

        public double AltSartliKabulSiniri { get; set; }

        public double UstSartliKabulSiniri { get; set; }

        public int CihazId { get; set; }

        public string ReferansDokuman { get; set; } = null!;

        public string Aciklama { get; set; } = null!;

        public string OrneklemSikligi { get; set; } = null!;

        public string OrneklemSiklikBirim { get; set; } = null!;

        public int FabrikaId { get; set; }

        public string PersonelSicilNo { get; set; } = null!;

        public DateTime EklenmeTarihi { get; set; }

        public bool KullanımDurumu { get; set; }
    }
}
