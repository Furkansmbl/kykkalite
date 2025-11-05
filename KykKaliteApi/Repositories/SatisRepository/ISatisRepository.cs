using HalApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Repositories.SatisRepository
{
    public interface ISatisRepository
    {
        Task<List<Satis>> GetAllAsync();
        Task<Satis?> GetByIdAsync(int id);
        Task<int> CreateAsync(Satis satis);
        Task<bool> UpdateAsync(Satis satis);
        Task<bool> DeleteAsync(int id);
        Task<List<Satis>> GetByCariIdAsync(int cariId); // İlgili CariID'ye göre satışları al
    }
}
