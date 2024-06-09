using KykKaliteApi.Dtos.GetHmpatamaByHmIdDtos;

namespace KykKaliteApi.Repositories.GetHmpatamaByHmIdRepository
{
    public interface IGetHmpatamaByHmıdRepository

    {
        Task<List<ResultGetHmpatamaByHmıdDto>> GetHmatamaKodlariByHmIDAsync(string MalzemeAciklamasi);
    }
}
