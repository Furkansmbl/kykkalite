namespace Kykkalite_UI.Dtos.GetHmatamaByHmıdDtos
{
    public class ResultGetHmatamaByHmıdDto
    {
        public string KontrolParametresi { get; set; } = null!;

        public int HmpaId { get; set; }
        public string ParametreId { get; set; } = null!;
        public string MalzemeAciklamasi { get; set; } = null!;


        public string ParametreKodu { get; set; } = null!;

        public int HammaddeId { get; set; }

        public string HmpatamaKodu { get; set; } = null!;
        public string Versiyon { get; set; }

        public string ParametreKritiklikSeviyesi { get; set; }

        public double KontrolDegeriNominal { get; set; }

        public double AltOnaySiniri { get; set; }

        public double UstOnaySiniri { get; set; }

        public double AltSartliKabulSiniri { get; set; }

        public double UstSartliKabulSiniri { get; set; }

        public int CihazId { get; set; }

        public string ReferansDokuman { get; set; }

        public string Aciklama { get; set; }

        public int FabrikaId { get; set; }

        public string PersonelSicilNo { get; set; } = null!;

        public string OlusturmaTarihi { get; set; }
        public string Value { get; set; }
    }
}
