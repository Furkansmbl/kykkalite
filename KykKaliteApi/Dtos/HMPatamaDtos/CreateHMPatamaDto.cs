namespace KykKaliteApi.Dtos.HMPatamaDtos
{
    public class CreateHMPatamaDto
    {
        public int HmpaId { get; set; }

        public string ParametreKodu { get; set; } = null!;
        public string ParametreYonu { get; set; }

        public int HammaddeId { get; set; }
        public int ParametreId { get; set; }
        public string HMPAtamaKodu { get; set; } = null!;

        public bool ParametreKritiklikSeviyesi { get; set; }

        public double KontrolDegeriNominal { get; set; }

        public double AltOnaySiniri { get; set; }

        public double UstOnaySiniri { get; set; }

        public double AltSartliKabulSiniri { get; set; }

        public double UstSartliKabulSiniri { get; set; }
        public string MevcutPartiBuyuklugu { get; set; }
        public string TedarikSikligi { get; set; }
        public string TedarikSikligiOrtalama { get; set; }
        public string TedarikSikligiBirim { get; set; }

        
        public int CihazId { get; set; }
        public string Versiyon { get; set; }

        public string ReferansDokuman { get; set; } = null!;

        public string Aciklama { get; set; } = null!;

        public int FabrikaId { get; set; }

        public string PersonelSicilNo { get; set; } = null!;

        public string OlusturmaTarihi { get; set; }

        public bool KullanimDurumu { get; set; }
    }
}
