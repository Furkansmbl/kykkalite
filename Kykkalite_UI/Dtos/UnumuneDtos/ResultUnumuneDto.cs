namespace Kykkalite_UI.Dtos.UnumuneDtos
{
    public class ResultUnumuneDto
    {
        public int NumuneId { get; set; }

        public int UrunId { get; set; }

        public int SiraNo { get; set; }

        public DateTime UretimTarihi { get; set; }

        public int NumuneSeriNoSarjNo { get; set; }

        public bool MudahaleVarmi { get; set; }

        public string Aciklama { get; set; } = null!;

        public bool OnayDurumu { get; set; }

        public int AmirOnayDurumu { get; set; }

        public DateTime EklenmeTarihi { get; set; }

        public string PersonelSicilNo { get; set; } = null!;
    }
}
