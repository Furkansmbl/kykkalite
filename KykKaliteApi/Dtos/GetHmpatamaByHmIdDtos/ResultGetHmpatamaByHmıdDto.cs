namespace KykKaliteApi.Dtos.GetHmpatamaByHmIdDtos
{
    public class ResultGetHmpatamaByHmıdDto
    {
        public string KontrolParametresi { get; set; } = null!;

        public int HmpaId { get; set; }
        public string ParametreId { get; set; } = null!;
        public string MalzemeAciklamasi { get; set; } = null!;

        public string AmirOnayDurumu { get; set; }
        public string NumuneSeriNoSarjNo { get; set; }
        public string SiraNo { get; set; }
        public string IrsaliyeNo { get; set; }
        public string MalzemeLotSeriNo { get; set; }
        public string MalzemeMiktarı { get; set; }
        public string MiktarBirimi { get; set; }
        public string MalzemeSKT { get; set; }
        public string KYKBarkodNo { get; set; }
        public string MalzemeUretimTarihi { get; set; }
        public string Saat { get; set; }
        public int HammaddeID { get; set; }

        public string ParametreKodu { get; set; } = null!;

        public string HmpatamaKodu { get; set; } = null!;
        public string Versiyon { get; set; } 

        public string ParametreKritiklikSeviyesi { get; set; }

        public double KontrolDegeriNominal { get; set; }

        public double AltOnaySiniri { get; set; }

        public double UstOnaySiniri { get; set; }

        public double AltSartliKabulSiniri { get; set; }

        public double UstSartliKabulSiniri { get; set; }
        public string Trend { get; set; }

        public int CihazId { get; set; }

        public string ReferansDokuman { get; set; } 

        public string Aciklama { get; set; }
        public string UNVANI { get; set; }
        public string THMID { get; set; }
        public int FabrikaId { get; set; }
        public string PersonelSicilNo { get; set; } = null!;
        public string OlusturmaTarihi { get; set; }
        public string Value { get; set; }

    }
}
