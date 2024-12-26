namespace Kykkalite_UI.Dtos.HMPatamaDtos
{
    public class CreateHMPatamaDto
    {
        private int hammaddeId;
        private int parametreId;
        private int fabrikaId;
        public int HammaddeId
        {
            get { return hammaddeId; }
            set
            {
                hammaddeId = value;
                UpdateHMatamaKodu();
            }
        }
        public int ParametreId
        {
            get { return parametreId; }
            set
            {
                parametreId = value;
                UpdateHMatamaKodu();
            }
        }
        public int FabrikaId
        {
            get { return fabrikaId; }
            set
            {
                fabrikaId = value;
                UpdateHMatamaKodu();
            }
        }
        public string HMPAtamaKodu { get; private set; }
        public string Versiyon { get; set; } = "1";
        public string MevcutPartiBuyuklugu { get; set; }
        public string TedarikSikligi { get; set; }
        public string TedarikSikligiOrtalama { get; set; }
        public string TedarikSikligiBirim { get; set; }
        public string ParametreYonu { get; set; }

        private void UpdateHMatamaKodu()
        {
            HMPAtamaKodu = $"{hammaddeId}+{ParametreId}+{fabrikaId}";
        }
        public string ParametreKodu { get; set; } = null!;

        public bool ParametreKritiklikSeviyesi { get; set; }

        public double KontrolDegeriNominal { get; set; }

        public double AltOnaySiniri { get; set; }

        public double UstOnaySiniri { get; set; }

        public double AltSartliKabulSiniri { get; set; }

        public double UstSartliKabulSiniri { get; set; }

        public int CihazId { get; set; }

        public string ReferansDokuman { get; set; } = null!;

        public string Aciklama { get; set; } = null!;


        public string PersonelSicilNo { get; set; } = null!;
        public string OlusturmaTarihi { get; set; }


        public bool KullanimDurumu { get; set; }
    }
}
