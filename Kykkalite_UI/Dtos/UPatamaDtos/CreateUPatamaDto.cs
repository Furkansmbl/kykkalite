namespace Kykkalite_UI.Dtos.UPatamaDtos
{
    public class CreateUPatamaDto
    {
        public int UpaId { get; set; }

        public string ParametreKodu { get; set; } = null!;
        public bool KullanimDurumu { get; set; }


        private int urunId;
        private int parametreId;
        private int fabrikaId;
        public int UrunId
        {
            get { return urunId; }
            set
            {
                urunId = value;
                UpdateUpatamaKodu();
            }
        }
        public int ParametreId
        {
            get { return parametreId; }
            set
            {
                parametreId = value;
                UpdateUpatamaKodu();
            }
        }
        public int FabrikaId
        {
            get { return fabrikaId; }
            set
            {
                fabrikaId = value;
                UpdateUpatamaKodu();
            }
        }
        public string UpatamaKodu { get; private set; }

        private void UpdateUpatamaKodu()
        {
            UpatamaKodu = $"{urunId}+{ParametreId}+{fabrikaId}";
        }
        public string Versiyon { get; set; } = "1";

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

        public string PersonelSicilNo { get; set; } = null!;

        public DateTime OlusturmaTarihi { get; set; }

    }
}
