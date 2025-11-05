using System;

namespace HalApi.Dtos
{
    public class Satis
    {
        public int SatisID { get; set; }
        public int CariID { get; set; }
        public DateTime SatisTarihi { get; set; }
        public decimal ToplamTutar { get; set; }
        public string FaturaNo { get; set; }
    }
}
