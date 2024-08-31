using Dapper;
using KykKaliteApi.Dtos.UretimHatlariDtos;
using KykKaliteApi.Dtos.UrunGruplariDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.UrunGruplariRepository
{
    public class UrunGruplariRepository : IUrunGruplariRepository
    {
        private readonly Context _context;
        public UrunGruplariRepository(Context context)
        {
            _context = context;
        }

        public async void CreateUrunGruplari(CreateUrunGruplariDto createUrunGruplariDto)
        {

            string query = "insert into UrunGruplari (UgrupAdi,EklenmeGuncellenmeTarihi) values (@ugrupAdi , @eklenmeGuncellenmeTarihi)";
            var parameters = new DynamicParameters();
            parameters.Add("@ugrupAdi", createUrunGruplariDto.UgrupAdi);
            parameters.Add("@eklenmeGuncellenmeTarihi", createUrunGruplariDto.EklenmeGuncellenmeTarihi);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteUrunGruplari(int id)
        {
            string query = "Delete From UrunGruplari Where UrunGrupId=@urunGrupId";
            var parameters = new DynamicParameters();
            parameters.Add("urunGrupId", id);
            using (var connection = _context.CreateConnection())
            {
                int v = await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultUrunGruplariDto>> GetAllUrunGruplariAsync()
        {
            string query = "Select * From UrunGruplari";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultUrunGruplariDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateUrunGruplari(UpdateUrunGruplariDto updateUrunGruplariDto)
        {
            string query = "UPDATE UrunGruplari SET UgrupAdi = @ugrupAdi , EklenmeGuncellenmeTarihi = @eklenmeGuncellenmeTarihi WHERE UrunGrupId = @urunGrupId";
            var parameters = new DynamicParameters();
            parameters.Add("@urunGrupId", updateUrunGruplariDto.UrunGrupId);
            parameters.Add("@eklenmeGuncellenmeTarihi", updateUrunGruplariDto.EklenmeGuncellenmeTarihi);
            parameters.Add("@ugrupAdi", updateUrunGruplariDto.UgrupAdi);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
