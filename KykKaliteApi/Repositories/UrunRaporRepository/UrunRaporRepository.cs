using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.ParametrelerDtos;
using KykKaliteApi.Dtos.UrunlerDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.UrunRaporRepository
{
    public class UrunRaporRepository : IUrunRaporRepository
    {
        private readonly Context _context;
        public UrunRaporRepository(Context context)
        {
            _context = context;
        }
        public Task<IEnumerable<ResultFabrikaDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ResultUrunlerDto>> GetByFabrikaId(int fabrikaId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ResultParametrelerDto>> GetByUrunId(int urunId)
        {
            throw new NotImplementedException();
        }
    }
}
