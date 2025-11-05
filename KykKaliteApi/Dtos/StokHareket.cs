using System;

namespace HalApi.Dtos
{
    public class StokHareket
    {
        public int HareketID { get; set; }
        public int UrunID { get; set; }
        public string HareketTipi { get; set; }  // Giriş / Çıkış
        public int? KasaTipID { get; set; }
        public string Birim { get; set; }
        public decimal Miktar { get; set; }
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
    }
}
