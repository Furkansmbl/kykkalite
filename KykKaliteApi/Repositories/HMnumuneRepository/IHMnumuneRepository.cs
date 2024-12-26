using KykKaliteApi.Dtos.HammaddelerDtos;
using KykKaliteApi.Dtos.HMnumuneDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.HMnumuneRepository
{
    public interface IHMnumuneRepository
    {
       
        Task<List<ResultHMnumuneDto>> GetAllHMnumuneAsync();
        void CreateHMnumune(CreateHMnumuneDto createHMnumuneDto);
        void DeleteHMnumune(int id);
        void UpdateHMnumune(UpdateHMnumuneDto updateHMnumuneDto);
    }
}
