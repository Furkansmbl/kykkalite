using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.HammaddelerDtos;

namespace KykKaliteApi.Repositories.HammaddelerRepository
{
    public interface IHammaddelerRepository
    {
        Task<List<ResultHammaddelerDto>> GetAllHammaddelerAsync();
        void UpdateHammadde(UpdateHammaddelerDto updateHammaddelerDto);
    }
}
