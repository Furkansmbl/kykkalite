using Dapper;
using KykKaliteApi.Dtos.KullaniciDtos;
using KykKaliteApi.Dtos.ParametrelerDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.ParametrelerRepository
{
    public class ParametrelerRepository : IParametrelerRepository
    {
        private readonly Context _context;
        public ParametrelerRepository(Context context)
        {
            _context = context;
        }

        public async void CreateParametreler(CreateParametrelerDto createParametrelerDto)
        {
            string query = "insert into Parametreler (ParametreKodu,KontrolParametresi,ParametreTipiOlcmeGozlem,Birimi,PersonelSicilNo,EklenmeGuncellenmeTarihi,KullanimDurumu) values (@parametreKodu,@kontrolParametresi,@parametreTipiOlcmeGozlem,@birimi,@personelSicilNo,@eklenmeGuncellenmeTarihi,@kullanimDurumu)";
            var parameters = new DynamicParameters();
            parameters.Add("@parametreKodu", createParametrelerDto.ParametreKodu);
            parameters.Add("@kontrolParametresi", createParametrelerDto.KontrolParametresi);
            parameters.Add("@parametreTipiOlcmeGozlem", createParametrelerDto.ParametreTipiOlcmeGozlem);
            parameters.Add("@birimi", createParametrelerDto.Birimi);
            parameters.Add("@personelSicilNo", createParametrelerDto.PersonelSicilNo);
            parameters.Add("@eklenmeGuncellenmeTarihi", createParametrelerDto.EklenmeGuncellenmeTarihi);
            parameters.Add("@kullanimDurumu", createParametrelerDto.KullanimDurumu);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteParametreler(int id)
        {
            string query = "Delete From Parametreler Where ParametreKodu=@parametreKodu";
            var parameters = new DynamicParameters();
            parameters.Add("parametreKodu", id);
            using (var connection = _context.CreateConnection())
            {
                int v = await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultParametrelerDto>> GetAllParametrelerAsync()
        {
            string query = "Select * From Parametreler";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultParametrelerDto>(query);
                return values.ToList();
            }
        }
    }
}
