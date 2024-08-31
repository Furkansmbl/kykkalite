namespace Kykkalite_UI.Dtos.GetUpatamaKodlariByUrunIDDtos
{
    public class CreateGetUpatamaKodlariByUrunId
    {    
        public int UrunId { get; set; }
        public int NumuneId { get; set; }

        public int SiraNo { get; set; }
        public DateTime GuncellenmeTarihi { get; set; }

        public DateTime UretimTarihi { get; set; }

        public int NumuneSeriNoSarjNo { get; set; }

        public bool MudahaleVarmi { get; set; }

        public string Aciklama { get; set; } = null!;

        public bool OnayDurumu { get; set; }

        public int AmirOnayDurumu { get; set; }

        public DateTime EklenmeTarihi { get; set; }

        public string PersonelSicilNo { get; set; } = null!;
        public string UPAtamaKodu { get; set; } = null!;

        public int Value { get; set; }
        public double AltOnaySiniri { get; set; }

        public double UstOnaySiniri { get; set; }
    }
}

