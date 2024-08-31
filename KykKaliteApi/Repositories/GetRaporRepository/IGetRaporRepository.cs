 using KykKaliteApi.Dtos.GetRaporDtos;

namespace KykKaliteApi.Repositories.GetRaporRepository
{
    public interface IGetRaporRepository
    {
        Task<List<ResultGetRaporDtos>> GetRaporDtosAsync(int FabrikaId, string malzemeAciklamasi, string OlusturmaTarihi, string BitisTarihi );
        Task<List<ResultGetRaporDtos>> GetRaporDtosNotKontrolAsync(int FabrikaId, string malzemeAciklamasi, string OlusturmaTarihi, string BitisTarihi, string amirOnayDurumu);

    }
}
