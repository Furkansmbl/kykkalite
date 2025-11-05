using HalApi.Dtos;

namespace HalApi.Repositories.AlimRepository
{
    public interface IAlimRepository
    {
        Task<List<Alim>> GetAllAsync();
        Task<Alim?> GetByIdAsync(int id);
        Task<int> CreateAsync(Alim alim);
        Task<bool> UpdateAsync(Alim alim);
        Task<bool> DeleteAsync(int id);
    }
}
