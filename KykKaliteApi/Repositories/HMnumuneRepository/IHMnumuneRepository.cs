using KykKaliteApi.Dtos.HammaddelerDtos;
using KykKaliteApi.Dtos.HMnumuneDtos;
using KykKaliteApi.Dtos.UnumuneDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.HMnumuneRepository
{
    public interface IHMnumuneRepository
    {
       
        Task<List<ResultHMnumuneDto>> GetAllHMnumuneAsync();
        void CreateHMnumune(CreateHMnumuneDto createHMnumuneDto);
        void CreateHMnumuneManuel(CreateHMnumuneManuelDto createHMnumuneManuelDto );
        void SentHMnumune(CreateHMnumuneDto createHMnumuneDto);
        void TrendMailHMnumune(CreateHMnumuneDto createHMnumuneDto);
        void DeleteHMnumune(int id);
        void UpdateHMnumune(UpdateHMnumuneDto updateHMnumuneDto);
        Task UpdateAmir(AmirOnayDurumuHMnumuneDto amirOnayDurumuHMnumuneDto );
        Task<AmirOnayDurumuHMnumuneDto> GetDataByToken(string token);
    }
}
