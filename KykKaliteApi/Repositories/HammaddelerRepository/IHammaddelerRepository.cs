using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.HammaddelerDtos;
using KykKaliteApi.Dtos.UrunlerDtos;

namespace KykKaliteApi.Repositories.HammaddelerRepository
{
    public interface IHammaddelerRepository
    {
        Task<List<ResultHammaddelerDto>> GetAllHammaddelerAsync();
        void CreateHammaddeler(CreateHammaddelerDto createHammaddelerDto);

        void UpdateHammadde(UpdateHammaddelerDto updateHammaddelerDto);
    }
}
