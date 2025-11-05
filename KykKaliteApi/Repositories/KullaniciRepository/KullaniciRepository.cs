using Dapper;
using HalApi.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalApi.Repositories.KullaniciRepository
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly string _connectionString;

        public KullaniciRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Kullanici>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Kullanicilar";
            var result = await connection.QueryAsync<Kullanici>(query);
            return result.ToList();
        }

        public async Task<Kullanici?> GetByIdAsync(string sicilNo)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Kullanicilar WHERE PersonelSicilNo = @SicilNo";
            return await connection.QueryFirstOrDefaultAsync<Kullanici>(query, new { SicilNo = sicilNo });
        }

        public async Task<int> CreateAsync(Kullanici kullanici)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"INSERT INTO Kullanicilar 
                          (PersonelSicilNo, PersonelAdiSoyadi, Gorevi, AdminUser, Password, EklenmeGuncellenmeTarihi, KullanimDurumu)
                          VALUES 
                          (@PersonelSicilNo, @PersonelAdiSoyadi, @Gorevi, @AdminUser, @Password, @EklenmeGuncellenmeTarihi, @KullanimDurumu)";
            var result = await connection.ExecuteAsync(query, kullanici);
            return result;
        }

        public async Task<bool> UpdateAsync(Kullanici kullanici)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"UPDATE Kullanicilar SET
                          PersonelAdiSoyadi = @PersonelAdiSoyadi,
                          Gorevi = @Gorevi,
                          AdminUser = @AdminUser,
                          Password = @Password,
                          EklenmeGuncellenmeTarihi = @EklenmeGuncellenmeTarihi,
                          KullanimDurumu = @KullanimDurumu
                          WHERE PersonelSicilNo = @PersonelSicilNo";
            var affected = await connection.ExecuteAsync(query, kullanici);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(string sicilNo)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM Kullanicilar WHERE PersonelSicilNo = @SicilNo";
            var affected = await connection.ExecuteAsync(query, new { SicilNo = sicilNo });
            return affected > 0;
        }
    }
}
