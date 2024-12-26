using KykKaliteApi.Dtos.GetUpatamaKodlariByUrunIDDtos;
using KykKaliteApi.Dtos.GetValueByMalzemeAciklamasiWParametreKodu;

namespace KykKaliteApi.Repositories.GetValueByMalzemeAciklamasiWParametreKodu
{
    public interface IGetValueRepository
    {
        Task<List<ResultGetValueDto>> GetValueByMalzemeAciklamasiWParametreKoduAsync(string malzemeaciklamasi, string kontrolparametresi,string baslangicTarihi, string bitisTarihi, int fabrikaId);
        Task<List<ResultGetValueDto>> GetValueByMalzemeAciklamasiWParametreKoduHammaddeAsync(string malzemeaciklamasi, string kontrolparametresi, string baslangicTarihi, string bitisTarihi, string UNVANI);

        Task<List<ResultGetValueDto>> GetKontrolParametresi();
        Task<List<ResultGetValueDto>> GetKontrolParametresiHammadde();

    }
}
