using KykKaliteApi.Dtos.GetHmpatamaByHmIdDtos;
using KykKaliteApi.Dtos.GetUpatamaKodlariByUrunIDDtos;
using KykKaliteApi.Dtos.UnumuneDtos;

namespace KykKaliteApi.Repositories.GetUpatamaKodlariByUrunIDRepository
{
    public interface IGetUpatamaKodlariByUrunIDRepository
    {
        Task<List<ResultGetUpatamaKodlariByUrunIDDto>> GetUpatamaKodlariByUrunIDAsync(string MalzemeAciklamasi,int FabrikaId, string HatAdiAciklamasi);
        Task<List<ResultGetUpatamaKodlariByUrunIDDto>> GetUretimHatlari();

    }
}
