﻿namespace KykKaliteApi.Dtos.UretimHatlariDtos
{
    public class ResultUretimHatlariDto
    {
        public int UretimHattiId { get; set; }

        public int FabrikaId { get; set; }

        public string HatAdiAciklamasi { get; set; }
        public DateTime EklenmeGuncellenmeTarihi { get; set; }

    }
}
