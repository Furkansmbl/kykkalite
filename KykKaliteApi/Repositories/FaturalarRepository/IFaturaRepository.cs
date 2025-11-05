using HalApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Repositories.FaturaRepository
{
    public interface IFaturaRepository
    {
        Task<List<Fatura>> GetAllAsync();
        Task<Fatura?> GetByIdAsync(int id);
        Task CreateAsync(Fatura fatura);
        Task UpdateAsync(Fatura fatura);
        Task DeleteAsync(int id);
    }
}
