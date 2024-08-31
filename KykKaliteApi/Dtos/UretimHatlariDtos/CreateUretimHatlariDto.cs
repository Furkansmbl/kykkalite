namespace KykKaliteApi.Dtos.UretimHatlariDtos
{
    public class CreateUretimHatlariDto
    {
        public int UretimHattiId { get; set; }

        public int FabrikaId { get; set; }

        public string HatAdiAciklamasi { get; set; }
        public DateTime EklenmeGuncellenmeTarihi { get; set; }

    }
}
