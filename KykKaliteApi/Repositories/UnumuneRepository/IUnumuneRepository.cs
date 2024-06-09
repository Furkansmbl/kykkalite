using KykKaliteApi.Dtos.GetUpatamaKodlariByUrunIDDtos;
using KykKaliteApi.Dtos.HMPNvalueDtos;
using KykKaliteApi.Dtos.ParametrelerDtos;
using KykKaliteApi.Dtos.UnumuneDtos;

namespace KykKaliteApi.Repositories.UnumuneRepository
{
    public interface IUnumuneRepository
    {
        Task<List<ResultUnumuneDto>> GetAllUnumuneAsync();
        void CreateUnumune(CreateUnumuneDto createUnumuneDto);
        void DeleteUnumune(int id);
        void UpdateUnumune(UpdateUnumuneDto updateUnumuneDto);
        Task UpdateAmir(AmirOnayDurumuUnumuneDto amirOnayDurumuUnumuneDto);
        Task<AmirOnayDurumuUnumuneDto> GetDataByToken(string token);
    }
}
