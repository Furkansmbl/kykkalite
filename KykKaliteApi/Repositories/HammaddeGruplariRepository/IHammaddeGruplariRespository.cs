using KykKaliteApi.Dtos.CihazlarDtos;
using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.HammaddeGruplariDtos;

namespace KykKaliteApi.Repositories.HammaddeGruplariRepository
{
    public interface IHammaddeGruplariRespository
    {
        Task<List<ResultHammaddeGruplariDto>> GetAllHammaddeGruplari();
        void CreateHammaddeGruplari(CreateHammaddeGruplariDto createHammaddeGruplariDto);
        void UpdateHammaddeGruplari(UpdateHammaddeGruplariDto updateHammaddeGruplariDto);
    }
}
