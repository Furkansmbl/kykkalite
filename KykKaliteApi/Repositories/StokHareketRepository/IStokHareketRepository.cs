using HalApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Repositories.StokHareketRepository
{
    public interface IStokHareketRepository
    {
        Task<List<StokHareket>> GetAllAsync();
        Task<StokHareket?> GetByIdAsync(int hareketId);
        Task<int> CreateAsync(StokHareket stokHareket);
        Task<bool> UpdateAsync(StokHareket stokHareket);
        Task<bool> DeleteAsync(int hareketId);
        Task<List<StokHareket>> GetByUrunIdAsync(int urunId);
    }
}
