namespace Kykkalite_UI.Dtos.HMnumuneDtos
{
    public class ResultHMnumuneDto
    {
        public int NumuneId { get; set; }

        public int HammaddeId { get; set; }

        public int SiraNo { get; set; }

        public DateTime Tarihi { get; set; }

        public string IrsaliyeNo { get; set; } = null!;

        public string MalzemeLotSeriNo { get; set; } = null!;

        public string KykbarkodNo { get; set; } = null!;

        public DateTime MalzemeUretimTarihi { get; set; }

        public DateTime MalzemeSkt { get; set; }

        public int MalzemeMiktarı { get; set; }

        public string MiktarBirimi { get; set; } = null!;

        public string Aciklama { get; set; } = null!;

        public bool OnayDurumu { get; set; }

        public int AmirOnayDurumu { get; set; }

        public DateTime EklenmeTarihi { get; set; }

        public string PersonelSicilNo { get; set; } = null!;
    }
}
