using KykKaliteApi.Dtos.GetHmpatamaByHmIdDtos;
using KykKaliteApi.Dtos.GetValueByMalzemeAciklamasiWParametreKodu;

namespace KykKaliteApi.Repositories.GetHmpatamaByHmIdRepository
{
    public interface IGetHmpatamaByHmıdRepository

    {
        Task<List<ResultGetHmpatamaByHmıdDto>> GetHmatamaKodlariByHmIDAsync(string MalzemeAciklamasi, int fabrikaId, string THMID, string Unvani);
        Task<List<ResultGetHmpatamaByHmıdDto>> GetHmatamaKodlariByHmIDNullAsync(string MalzemeAciklamasi, int fabrikaId,string THMID);
        Task<List<ResultGetHmpatamaByHmıdDto>> GetTedarikci();

    }
}
