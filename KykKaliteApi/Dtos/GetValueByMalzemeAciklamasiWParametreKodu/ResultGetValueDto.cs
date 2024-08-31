namespace KykKaliteApi.Dtos.GetValueByMalzemeAciklamasiWParametreKodu
{
    public class ResultGetValueDto
    {
        public string Value { get; set; } = null!;
        public string KontrolParametresi { get; set; } = null!;
        public double AltOnaySiniri { get; set; }
        public double AltSartliKabulSiniri { get; set; }

        public double UstSartliKabulSiniri { get; set; }
        public double UstOnaySiniri { get; set; }
        public string ParametreTipiOlcmeGozlem { get; set; }
        public string BaslanicTarihi { get; set; }
        public string BitisTarihi { get; set; }



    }
}
