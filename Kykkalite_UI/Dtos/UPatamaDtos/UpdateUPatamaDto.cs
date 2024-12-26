namespace Kykkalite_UI.Dtos.UPatamaDtos
{
    public class UpdateUPatamaDto
    {
        public int UpaId { get; set; }

        public string ParametreKodu { get; set; } = null!;

        public int UrunId { get; set; }
        public int ParametreId { get; set; }

        public string UpatamaKodu { get; set; } = null!;
        public string Versiyon { get; set; }

        public string Tolerans { get; set; }
        public string ParametreYonu { get; set; }

        public bool KullanimDurumu { get; set; }
        public string ParametreKritiklikSeviyesi { get; set; }

        public string KontrolDegeriNominal { get; set; }

        public double AltOnaySiniri { get; set; }

        public double UstOnaySiniri { get; set; }

        public double AltSartliKabulSiniri { get; set; }

        public double UstSartliKabulSiniri { get; set; }

        public int CihazId { get; set; }

        public string ReferansDokuman { get; set; } = null!;

        public string Aciklama { get; set; } = "-";

        public string OrneklemSikligi { get; set; } = null!;

        public string OrneklemSiklikBirim { get; set; } = null!;

        public int FabrikaId { get; set; }

        public string PersonelSicilNo { get; set; } = null!;

        public DateTime OlusturmaTarihi { get; set; }


    }
}
