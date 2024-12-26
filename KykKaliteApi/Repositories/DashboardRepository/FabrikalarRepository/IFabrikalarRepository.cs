using KykKaliteApi.Dtos.FabrikalarDtos;

namespace KykKaliteApi.Repositories.FabrikalarRepository
{
    public interface IFabrikalarRepository
    {
        Task<List<ResultFabrikaDto>> GetAllFabrikaAsync();
        void CreateFabrika(CreateFabrikaDTO categoryDto);
        void DeleteFabrika(int id);
        void UpdateFabrika( UpdateFabrikaDto updateFabrikaDto);
    }
}
