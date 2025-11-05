using HalApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Repositories.KasaTipiRepository
{
    public interface IKasaTipiRepository
    {
        Task<List<KasaTipi>> GetAllAsync();
        Task<KasaTipi?> GetByIdAsync(int id);
        Task<int> CreateAsync(KasaTipi kasaTipi);
        Task<bool> UpdateAsync(KasaTipi kasaTipi);
        Task<bool> DeleteAsync(int id);
    }
}
