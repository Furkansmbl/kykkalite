using HalApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Repositories.CariRepository
{
    public interface ICariRepository
    {
        Task<List<Cari>> GetAllAsync();
        Task<Cari?> GetByIdAsync(int id);
        Task<int> CreateAsync(Cari cari);
        Task<bool> UpdateAsync(Cari cari);
        Task<bool> DeleteAsync(int id);
    }
}
