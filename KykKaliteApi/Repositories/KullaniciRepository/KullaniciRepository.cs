using Dapper;
using KykKaliteApi.Dtos.HMPNvalueDtos;
using KykKaliteApi.Dtos.KullaniciDtos;
using KykKaliteApi.Dtos.UPNvalueDtos;
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
            parameters.Add("@adminUser", createKullaniciDto.AdminUser);
            parameters.Add("@password", createKullaniciDto.Password); 
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

        public async void UpdateKullanici(UpdateKullaniciDto updateKullaniciDto)
        {
            string query = "UPDATE Kullanici SET PersonelAdiSoyadi = @personelAdiSoyadi,FabrikaId = @fabrikaId, Gorevi = @gorevi, EklenmeGuncellenmeTarihi = @eklenmeGuncellenmeTarihi, KullanimDurumu = @kullanimDurumu, adminUser = @adminUser WHERE PersonelSicilNo = @personelSicilNo";
            var parameters = new DynamicParameters();
            parameters.Add("@personelSicilNo", updateKullaniciDto.PersonelSicilNo);
            parameters.Add("@personelAdiSoyadi", updateKullaniciDto.PersonelAdiSoyadi);
            parameters.Add("@fabrikaId", updateKullaniciDto.FabrikaId);
            parameters.Add("@gorevi", updateKullaniciDto.Gorevi);
            parameters.Add("@adminUser", updateKullaniciDto.AdminUser);
            parameters.Add("@password", updateKullaniciDto.Password);
            parameters.Add("@eklenmeGuncellenmeTarihi", updateKullaniciDto.EklenmeGuncellenmeTarihi);
            parameters.Add("@kullanimDurumu", updateKullaniciDto.KullanimDurumu);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
