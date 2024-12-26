using Dapper;
using KykKaliteApi.Dtos.HMPNvalueDtos;
using KykKaliteApi.Dtos.KullaniciDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.KullaniciRepository
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly Context _context;
        public KullaniciRepository(Context context)
        {
            _context = context;
        }

        public async void CreateKullanici(CreateKullaniciDto createKullaniciDto)
        {
            string query = "insert into Kullanici (PersonelSicilNo,PersonelAdiSoyadi,FabrikaId,Gorevi,AdminUser,Password,EklenmeGuncellenmeTarihi,KullanimDurumu) values (@personelSicilNo,@personelAdiSoyadi,@fabrikaId,@gorevi,@adminUser,@password,@eklenmeGuncellenmeTarihi,@kullanimDurumu)";
            var parameters = new DynamicParameters();
            parameters.Add("@personelSicilNo", createKullaniciDto.PersonelSicilNo);
            parameters.Add("@personelAdiSoyadi", createKullaniciDto.PersonelAdiSoyadi);
            parameters.Add("@fabrikaId", createKullaniciDto.FabrikaId);
            parameters.Add("@gorevi", createKullaniciDto.Gorevi);
            parameters.Add("@adminUser", true);
            parameters.Add("@password", Convert.ToBase64String(createKullaniciDto.Password)); 
            parameters.Add("@eklenmeGuncellenmeTarihi", createKullaniciDto.EklenmeGuncellenmeTarihi);
            parameters.Add("@kullanimDurumu", true);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteKullanici(int id)
        {
            string query = "Delete From Kullanici Where PersonelSicilNo=@personelSicilNo";
            var parameters = new DynamicParameters();
            parameters.Add("personelSicilNo", id);
            using (var connection = _context.CreateConnection())
            {
                int v = await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultKullaniciDto>> GetAllKullaniciAsync()
        {
            string query = "Select * From Kullanici";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultKullaniciDto>(query);
                return values.ToList();
            }
        }
    }
}
