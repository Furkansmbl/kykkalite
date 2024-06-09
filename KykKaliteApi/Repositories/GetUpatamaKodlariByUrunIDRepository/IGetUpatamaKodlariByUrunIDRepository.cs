using KykKaliteApi.Dtos.GetUpatamaKodlariByUrunIDDtos;
using KykKaliteApi.Dtos.UnumuneDtos;

namespace KykKaliteApi.Repositories.GetUpatamaKodlariByUrunIDRepository
{
    public interface IGetUpatamaKodlariByUrunIDRepository
    {
        Task<List<ResultGetUpatamaKodlariByUrunIDDto>> GetUpatamaKodlariByUrunIDAsync(string MalzemeAciklamasi);
    }
}
