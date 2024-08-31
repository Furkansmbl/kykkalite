using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.ParametrelerDtos;
using KykKaliteApi.Dtos.UrunlerDtos;

namespace KykKaliteApi.Repositories.UrunRaporRepository
{
    public interface IUrunRaporRepository
    {
        Task<IEnumerable<ResultFabrikaDto>> GetAll();
        Task<IEnumerable<ResultUrunlerDto>> GetByFabrikaId(int fabrikaId);
        Task<IEnumerable<ResultParametrelerDto>> GetByUrunId(int urunId);
    }
}
