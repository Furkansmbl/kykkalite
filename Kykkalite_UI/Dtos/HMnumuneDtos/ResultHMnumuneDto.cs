namespace Kykkalite_UI.Dtos.HMnumuneDtos
{
    public class ResultHMnumuneDto
    {
        public int NumuneId { get; set; }

        public int HammaddeId { get; set; }
        public string UNVANI { get; set; }
        public string THMID { get; set; }

        public string SiraNo { get; set; }

        public string Tarihi { get; set; }
        public string MalzemeAciklamasi { get; set; }

        public string Saat { get; set; }
        public string IrsaliyeNo { get; set; } = null!;

        public string MalzemeLotSeriNo { get; set; } = null!;

        public string KykbarkodNo { get; set; } = null!;

        public string MalzemeUretimTarihi { get; set; }

        public string MalzemeSkt { get; set; }

        public string MalzemeMiktarı { get; set; }

        public string MiktarBirimi { get; set; } = null!;

        public string Aciklama { get; set; } = null!;

        public string AmirOnayDurumu { get; set; }
        public bool OnayDurumu { get; set; }


        public string Versiyon { get; set; }

        public string OlusturmaTarihi { get; set; }
        public string PersonelSicilNo { get; set; }
        public DateTime GuncellenmeTarihi { get; set; }
    }
}
