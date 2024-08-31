using Dapper;
using KykKaliteApi.Dtos.HammaddelerDtos;
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
            string query = "insert into Parametreler (ParametreKodu,KontrolParametresi,ParametreTipiOlcmeGozlem,Birimi,PersonelSicilNo,OlusturmaTarihi,KullanimDurumu) values (@parametreKodu,@kontrolParametresi,@parametreTipiOlcmeGozlem,@birimi,@personelSicilNo,@olusturmaTarihi,@kullanimDurumu)";
            var parameters = new DynamicParameters();
            parameters.Add("@parametreKodu", createParametrelerDto.ParametreKodu);
            parameters.Add("@kontrolParametresi", createParametrelerDto.KontrolParametresi);
            parameters.Add("@parametreTipiOlcmeGozlem", createParametrelerDto.ParametreTipiOlcmeGozlem);
            parameters.Add("@birimi", createParametrelerDto.Birimi);
            parameters.Add("@personelSicilNo", createParametrelerDto.PersonelSicilNo);
            parameters.Add("@olusturmaTarihi", createParametrelerDto.OlusturmaTarihi);
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

        public async void UpdateParametreler(UpdateParametrelerDto updateParametrelerDto)
        { 
            string query = "Update Parametreler Set ParametreKodu=@parametreKodu,KontrolParametresi=@kontrolParametresi,ParametreTipiOlcmeGozlem=@parametreTipiOlcmeGozlem,Birimi=@birimi,PersonelSicilNo=@personelSicilNo,OlusturmaTarihi=@olusturmaTarihi,GuncellenmeTarihi=@guncellenmeTarihi,KullanimDurumu=@kullanimDurumu where ParametreKodu=@parametreKodu";
            var parameters = new DynamicParameters();
            parameters.Add("@parametreKodu", updateParametrelerDto.ParametreKodu);
            parameters.Add("@kontrolParametresi", updateParametrelerDto.KontrolParametresi);
            parameters.Add("@parametreTipiOlcmeGozlem", updateParametrelerDto.ParametreTipiOlcmeGozlem);
            parameters.Add("@birimi", updateParametrelerDto.Birimi);
            parameters.Add("@personelSicilNo", updateParametrelerDto.PersonelSicilNo);
            parameters.Add("@olusturmaTarihi", updateParametrelerDto.OlusturmaTarihi);
            parameters.Add("@guncellenmeTarihi", updateParametrelerDto.GuncellenmeTarihi);
            parameters.Add("@kullanimDurumu", true);

            using (var connectiont = _context.CreateConnection())
            {
                await connectiont.ExecuteAsync(query, parameters);
            }
        }
    }
}
