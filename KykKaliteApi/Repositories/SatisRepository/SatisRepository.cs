using Dapper;
using HalApi.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalApi.Repositories.SatisRepository
{
    public class SatisRepository : ISatisRepository
    {
        private readonly string _connectionString;

        public SatisRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Satis>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Satislar";
            var result = await connection.QueryAsync<Satis>(query);
            return result.ToList();
        }

        public async Task<Satis?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Satislar WHERE SatisID = @Id";
            return await connection.QueryFirstOrDefaultAsync<Satis>(query, new { Id = id });
        }

        public async Task<int> CreateAsync(Satis satis)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"INSERT INTO Satislar (CariID, SatisTarihi, ToplamTutar, FaturaNo)
                          VALUES (@CariID, @SatisTarihi, @ToplamTutar, @FaturaNo);
                          SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await connection.ExecuteScalarAsync<int>(query, satis);
            return id;
        }

        public async Task<bool> UpdateAsync(Satis satis)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"UPDATE Satislar SET
                          CariID = @CariID,
                          SatisTarihi = @SatisTarihi,
                          ToplamTutar = @ToplamTutar,
                          FaturaNo = @FaturaNo
                          WHERE SatisID = @SatisID";
            var affected = await connection.ExecuteAsync(query, satis);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM Satislar WHERE SatisID = @Id";
            var affected = await connection.ExecuteAsync(query, new { Id = id });
            return affected > 0;
        }

        public async Task<List<Satis>> GetByCariIdAsync(int cariId)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Satislar WHERE CariID = @CariID";
            var result = await connection.QueryAsync<Satis>(query, new { CariID = cariId });
            return result.ToList();
        }
    }
}
