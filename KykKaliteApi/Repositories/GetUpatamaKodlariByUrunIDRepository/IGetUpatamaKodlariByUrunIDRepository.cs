using KykKaliteApi.Dtos.GetUpatamaKodlariByUrunIDDtos;

namespace KykKaliteApi.Repositories.GetUpatamaKodlariByUrunIDRepository
{
    public interface IGetUpatamaKodlariByUrunIDRepository
    {
        Task<List<ResultGetUpatamaKodlariByUrunIDDto>> GetUpatamaKodlariByUrunIDAsync(int urunId);
    }
}
