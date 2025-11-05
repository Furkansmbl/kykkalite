namespace HalApi.Dtos
{
    public class SatisDetay
    {
        public int DetayID { get; set; }
        public int SatisID { get; set; }
        public int UrunID { get; set; }
        public int? KasaTipID { get; set; }
        public string Birim { get; set; }
        public decimal Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
    }
}
