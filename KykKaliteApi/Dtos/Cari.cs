using System;

namespace HalApi.Dtos
{
    public class Cari
    {
        public int CariID { get; set; }
        public string CariTipi { get; set; }  // Müşteri / Tedarikçi
        public string Unvan { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public string VergiNo { get; set; }
        public decimal Bakiye { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
    }
}
