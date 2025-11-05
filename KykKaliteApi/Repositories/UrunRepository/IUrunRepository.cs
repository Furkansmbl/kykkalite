using HalApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Repositories.UrunRepository
{
    public interface IUrunRepository
    {
        Task<List<Urun>> GetAllAsync();
        Task<Urun?> GetByIdAsync(int urunId);
        Task<int> CreateAsync(Urun urun);
        Task<bool> UpdateAsync(Urun urun);
        Task<bool> DeleteAsync(int urunId);
    }
}
