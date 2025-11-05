using System;

namespace HalApi.Dtos
{
    public class Urun
    {
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public string VarsayilanBirim { get; set; }
        public int? VarsayilanKasaTipID { get; set; }
        public decimal AlisFiyati { get; set; }
        public decimal SatisFiyati { get; set; }
        public decimal StokMiktari { get; set; }
    }
}
