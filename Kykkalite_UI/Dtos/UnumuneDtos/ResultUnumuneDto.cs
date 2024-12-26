namespace Kykkalite_UI.Dtos.UnumuneDtos
{
    public class ResultUnumuneDto
    {
        public int NumuneId { get; set; }

        public int UrunId { get; set; }

        public string SiraNo { get; set; }
        public string MalzemeAciklamasi { get; set; } = null!;
        public string HatAdiAciklamasi { get; set; }
        public string UretimTarihi { get; set; }
        public string KontrolSaati { get; set; }
        public string NumuneSeriNoSarjNo { get; set; }
        public string MudahaleVarmi { get; set; }
        public string Aciklama { get; set; } = null!;
        public bool OnayDurumu { get; set; }

        public string AmirOnayDurumu { get; set; }

        public string OlusturmaTarihi { get; set; }
        public string GuncellenmeTarihi { get; set; }

        public string UnPersonel { get; set; } = null!;
        public string Token { get; set; }
        public string Trend { get; set; }

    }
}
