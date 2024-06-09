 namespace KykKaliteApi.Dtos.GetUpatamaKodlariByUrunIDDtos
{
    public class ResultGetUpatamaKodlariByUrunIDDto
    {
        public string KontrolParametresi { get; set; } = null!;
        public string MalzemeAciklamasi { get; set; } = null!;

        public int NumuneId { get; set; }

        public int UrunId { get; set; }

        public string SiraNo { get; set; }
        public string KontrolSaati { get; set; }


        public string UretimTarihi { get; set; }

        public string NumuneSeriNoSarjNo { get; set; }

        public string MudahaleVarmi { get; set; }

        public string Aciklama { get; set; } = null!;

        public bool OnayDurumu { get; set; }

        public string AmirOnayDurumu { get; set; }

        public string EklenmeTarihi { get; set; }

        public string PersonelSicilNo { get; set; } = null!;
        public string Value { get; set; } = null!;
        public double AltOnaySiniri { get; set; }

        public double UstOnaySiniri { get; set; }
        public double AltSartliKabulSiniri { get; set; }
        public double UstSartliKabulSiniri { get; set; }


    }



}