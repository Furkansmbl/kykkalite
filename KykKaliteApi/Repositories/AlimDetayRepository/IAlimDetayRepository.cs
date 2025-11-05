using HalApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Repositories.AlimDetayRepository
{
    public interface IAlimDetayRepository
    {
        Task<List<AlimDetay>> GetAllAsync();
        Task<AlimDetay?> GetByIdAsync(int id);
        Task<int> CreateAsync(AlimDetay detay);
        Task<bool> UpdateAsync(AlimDetay detay);
        Task<bool> DeleteAsync(int id);
        Task<List<AlimDetay>> GetByAlimIdAsync(int alimId); // İlgili AlimID’ye göre detayları al
    }
}
