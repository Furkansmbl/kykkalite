using System;

namespace HalApi.Dtos
{
    public class Fatura
    {
        public int FaturaID { get; set; }
        public string FaturaNo { get; set; }
        public int CariID { get; set; }
        public DateTime FaturaTarihi { get; set; }
        public decimal ToplamTutar { get; set; }
        public string Aciklama { get; set; }
    }
}
