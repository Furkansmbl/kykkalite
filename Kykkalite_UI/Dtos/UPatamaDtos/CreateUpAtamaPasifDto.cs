namespace Kykkalite_UI.Dtos.UPatamaDtos
{
    public class CreateUpAtamaPasifDto
    {
        public string ParametreKodu { get; set; } = null!;

        public int UrunId { get; set; }
        public int ParametreId { get; set; }

        public string UpatamaKodu { get; set; } = null!;
        private string _versiyon;

        public string Versiyon
        {
            get { return _versiyon; }
            set { _versiyon = (int.Parse(value) - 1).ToString(); }
        }

        public string Tolerans { get; set; }
        public string ParametreYonu { get; set; }

        public string ParametreKritiklikSeviyesi { get; set; }

        public string KontrolDegeriNominal { get; set; }

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
        public DateTime OlusturmaTarihi { get; set; }
        public string PasifeAlanPersonelSicilNo { get; set; } = "-";
        public DateTime PasifeAlinmaTarihi { get; set; }
    }
}
