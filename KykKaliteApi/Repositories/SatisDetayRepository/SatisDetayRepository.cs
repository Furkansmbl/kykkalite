using Dapper;
using HalApi.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalApi.Repositories.SatisDetayRepository
{
    public class SatisDetayRepository : ISatisDetayRepository
    {
        private readonly string _connectionString;

        public SatisDetayRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<SatisDetay>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM SatisDetaylar";
            var result = await connection.QueryAsync<SatisDetay>(query);
            return result.ToList();
        }

        public async Task<SatisDetay?> GetByIdAsync(int detayId)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM SatisDetaylar WHERE DetayID = @DetayID";
            return await connection.QueryFirstOrDefaultAsync<SatisDetay>(query, new { DetayID = detayId });
        }

        public async Task<int> CreateAsync(SatisDetay satisDetay)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"INSERT INTO SatisDetaylar (SatisID, UrunID, KasaTipID, Birim, Miktar, BirimFiyat)
                          VALUES (@SatisID, @UrunID, @KasaTipID, @Birim, @Miktar, @BirimFiyat);
                          SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await connection.ExecuteScalarAsync<int>(query, satisDetay);
            return id;
        }

        public async Task<bool> UpdateAsync(SatisDetay satisDetay)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"UPDATE SatisDetaylar SET
                          SatisID = @SatisID,
                          UrunID = @UrunID,
                          KasaTipID = @KasaTipID,
                          Birim = @Birim,
                          Miktar = @Miktar,
                          BirimFiyat = @BirimFiyat
                          WHERE DetayID = @DetayID";
            var affected = await connection.ExecuteAsync(query, satisDetay);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int detayId)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM SatisDetaylar WHERE DetayID = @DetayID";
            var affected = await connection.ExecuteAsync(query, new { DetayID = detayId });
            return affected > 0;
        }

        public async Task<List<SatisDetay>> GetBySatisIdAsync(int satisId)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM SatisDetaylar WHERE SatisID = @SatisID";
            var result = await connection.QueryAsync<SatisDetay>(query, new { SatisID = satisId });
            return result.ToList();
        }
    }
}
