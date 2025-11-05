using HalApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Repositories.SatisDetayRepository
{
    public interface ISatisDetayRepository
    {
        Task<List<SatisDetay>> GetAllAsync();
        Task<SatisDetay?> GetByIdAsync(int detayId);
        Task<int> CreateAsync(SatisDetay satisDetay);
        Task<bool> UpdateAsync(SatisDetay satisDetay);
        Task<bool> DeleteAsync(int detayId);
        Task<List<SatisDetay>> GetBySatisIdAsync(int satisId);
    }
}
