using HalApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Repositories.KullaniciRepository
{
    public interface IKullaniciRepository
    {
        Task<List<Kullanici>> GetAllAsync();
        Task<Kullanici?> GetByIdAsync(string sicilNo);
        Task<int> CreateAsync(Kullanici kullanici);
        Task<bool> UpdateAsync(Kullanici kullanici);
        Task<bool> DeleteAsync(string sicilNo);
    }
}
