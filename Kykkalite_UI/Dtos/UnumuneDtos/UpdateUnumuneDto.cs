namespace Kykkalite_UI.Dtos.UnumuneDtos
{
    public class UpdateUnumuneDto
    {
        public int NumuneId { get; set; }

        public int UrunId { get; set; }
        public string Token { get; set; } = "-";
        public string HatAdiAciklamasi { get; set; }
        public string Trend { get; set; } = "-";
        public string SiraNo { get; set; }

        public string UretimTarihi { get; set; }
        public string KontrolSaati { get; set; }

        public string NumuneSeriNoSarjNo { get; set; }

        public bool MudahaleVarmi { get; set; }

        public string Aciklama { get; set; } = null!;

        public bool OnayDurumu { get; set; }

        public string AmirOnayDurumu { get; set; }

        public string OlusturmaTarihi { get; set; }
        public DateTime GuncellenmeTarihi { get; set; }

        public string PersonelSicilNo { get; set; } = null!;
    }
}
