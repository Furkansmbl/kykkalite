using Dapper;
using HalApi.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalApi.Repositories.AlimRepository
{
    public class AlimRepository : IAlimRepository
    {
        private readonly string _connectionString;

        public AlimRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Alim>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Alim";
            var result = await connection.QueryAsync<Alim>(query);
            return result.ToList();
        }

        public async Task<Alim?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Alim WHERE AlimID = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<Alim>(query, new { Id = id });
            return result;
        }

        public async Task<int> CreateAsync(Alim alim)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"INSERT INTO Alim (CariID, AlimTarihi, ToplamTutar)
                          VALUES (@CariID, @AlimTarihi, @ToplamTutar);
                          SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await connection.ExecuteScalarAsync<int>(query, alim);
            return id;
        }

        public async Task<bool> UpdateAsync(Alim alim)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"UPDATE Alim 
                          SET CariID = @CariID, AlimTarihi = @AlimTarihi, ToplamTutar = @ToplamTutar
                          WHERE AlimID = @AlimID";
            var affected = await connection.ExecuteAsync(query, alim);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM Alim WHERE AlimID = @Id";
            var affected = await connection.ExecuteAsync(query, new { Id = id });
            return affected > 0;
        }
    }
}
