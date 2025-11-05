using Dapper;
using HalApi.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalApi.Repositories.AlimDetayRepository
{
    public class AlimDetayRepository : IAlimDetayRepository
    {
        private readonly string _connectionString;

        public AlimDetayRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<AlimDetay>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM AlimDetay";
            var result = await connection.QueryAsync<AlimDetay>(query);
            return result.ToList();
        }

        public async Task<AlimDetay?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM AlimDetay WHERE DetayID = @Id";
            return await connection.QueryFirstOrDefaultAsync<AlimDetay>(query, new { Id = id });
        }

        public async Task<int> CreateAsync(AlimDetay detay)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"INSERT INTO AlimDetay (AlimID, UrunID, KasaTipID, Birim, Miktar, BirimFiyat)
                          VALUES (@AlimID, @UrunID, @KasaTipID, @Birim, @Miktar, @BirimFiyat);
                          SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await connection.ExecuteScalarAsync<int>(query, detay);
            return id;
        }

        public async Task<bool> UpdateAsync(AlimDetay detay)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"UPDATE AlimDetay SET 
                          AlimID = @AlimID,
                          UrunID = @UrunID,
                          KasaTipID = @KasaTipID,
                          Birim = @Birim,
                          Miktar = @Miktar,
                          BirimFiyat = @BirimFiyat
                          WHERE DetayID = @DetayID";
            var affected = await connection.ExecuteAsync(query, detay);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM AlimDetay WHERE DetayID = @Id";
            var affected = await connection.ExecuteAsync(query, new { Id = id });
            return affected > 0;
        }

        public async Task<List<AlimDetay>> GetByAlimIdAsync(int alimId)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM AlimDetay WHERE AlimID = @AlimID";
            var result = await connection.QueryAsync<AlimDetay>(query, new { AlimID = alimId });
            return result.ToList();
        }
    }
}
