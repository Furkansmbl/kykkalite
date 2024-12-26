namespace KykKaliteApi.Dtos.HMPatamaDtos
{
    public class ResultHMPatamaDto
    {
        public int HmpaId { get; set; }

        public string ParametreKodu { get; set; } = null!;
        public string Tolerans { get; set; }
        public int ParametreID { get; set; }
        public string ParametreYonu { get; set; }

        public int HammaddeId { get; set; }
        public string MalzemeAciklamasi { get; set; } = null!;

        public string HmpatamaKodu { get; set; } = null!;
        public string MevcutPartiBuyuklugu { get; set; }
        public string TedarikSikligi { get; set; }
        public string TedarikSikligiOrtalama { get; set; }
        public string TedarikSikligiBirim { get; set; }
        public string Versiyon { get; set; }
        public string ParametreKritiklikSeviyesi { get; set; }


        public double KontrolDegeriNominal { get; set; }

        public double AltOnaySiniri { get; set; }

        public double UstOnaySiniri { get; set; }

        public double AltSartliKabulSiniri { get; set; }

        public double UstSartliKabulSiniri { get; set; }

        public int CihazId { get; set; }

        public string ReferansDokuman { get; set; } = null!;

        public string Aciklama { get; set; } = null!;

        public int FabrikaId { get; set; }

        public string PersonelSicilNo { get; set; } = null!;

        public string OlusturmaTarihi { get; set; }

        public bool KullanimDurumu { get; set; }
    }
}
