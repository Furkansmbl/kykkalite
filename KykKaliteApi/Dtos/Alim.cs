using System;

namespace HalApi.Dtos
{
    public class Alim
    {
        public int AlimID { get; set; }
        public int CariID { get; set; }
        public DateTime AlimTarihi { get; set; }
        public decimal ToplamTutar { get; set; }
    }
}
