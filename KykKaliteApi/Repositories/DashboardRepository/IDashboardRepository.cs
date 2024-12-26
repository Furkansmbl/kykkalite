using KykKaliteApi.Dtos.DashboardDtos;
using KykKaliteApi.Dtos.FabrikalarDtos;

namespace KykKaliteApi.Repositories.DashboardRepository
{
    public interface IDashboardRepository
    {
        Task<List<UrunDegKatDto>> GetAllDegKatAsync();
        Task<List<UrunDegKatDto>> GetAllDegKatHmAsync();
        Task<List<FabrikaOnayDto>> GetAllFabrikaOnayAsync();
        Task<List<UrunOranDto>> GetAllUrunOranAsync();
        Task<List<FabrikaOnayDto>> GetAllFabrikaOnayHammaddeAsync();
        Task<List<FabrikaOnayDto>> GetAllTedarikciRedHammaddeAsync();
        Task<List<FabrikaOnayDto>> GetAllTedarikciSartliOnayHammaddeAsync();


    }
}
