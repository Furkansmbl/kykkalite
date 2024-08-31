namespace KykKaliteApi.Dtos.GetRaporDtos
{
    public class ResultGetRaporDtos
    {
        public int FabrikaId { get; set; }
       
        public string FabrikaAdi { get; set; }
        public string Value { get; set; }
        public float AltSartliKabulSiniri { get; set; }
        public float AltOnaySiniri { get; set; }
        public float UstOnaySiniri { get; set; }
        public float UstSartliKabulSiniri { get; set; }

        public int UrunId { get; set; }
        public string MalzemeAciklamasi { get; set; } = null!;
        public int ParametreID { get; set; }
        public string ParametreKodu { get; set; } = null!;

        public string KontrolParametresi { get; set; } = null!;
        public string PersonelSicilNo { get; set; } = null!;
        public int NumuneId { get; set; }
        public string AmirOnayDurumu { get; set; }
        public string OlusturmaTarihi { get; set; }
        public string BitisTarihi { get; set; }

    }
}
