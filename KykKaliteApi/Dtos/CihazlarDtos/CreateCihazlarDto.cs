﻿namespace KykKaliteApi.Dtos.CihazlarDtos
{
    public class CreateCihazlarDto
    {

        public string CihazKodu { get; set; } = null!;

        public string KullanılanCihazEkipman { get; set; } = null!;

        public int FabrikaID { get; set; }

        public string PersonelSicilNo { get; set; } = null!;

        public DateTime EklenmeGuncellenmeTarihi { get; set; }

        public bool KullanımDurumu { get; set; }

    }
}
