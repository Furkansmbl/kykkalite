﻿namespace Kykkalite_UI.Dtos.ParametrelerDtos
{
    public class UpdateParametrelerDto
    {
        public string ParametreKodu { get; set; } = null!;

        public string KontrolParametresi { get; set; } = null!;

        public bool ParametreTipiOlcmeGozlem { get; set; }

        public string Birimi { get; set; } = null!;

        public string PersonelSicilNo { get; set; } = null!;

        public DateTime EklenmeGuncellenmeTarihi { get; set; }

        public bool KullanimDurumu { get; set; }
    }
}
