using Dapper;
using HalApi.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalApi.Repositories.KasaTipiRepository
{
    public class KasaTipiRepository : IKasaTipiRepository
    {
        private readonly string _connectionString;

        public KasaTipiRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<KasaTipi>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM KasaTipleri";
            var result = await connection.QueryAsync<KasaTipi>(query);
            return result.ToList();
        }

        public async Task<KasaTipi?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM KasaTipleri WHERE KasaTipID = @Id";
            return await connection.QueryFirstOrDefaultAsync<KasaTipi>(query, new { Id = id });
        }

        public async Task<int> CreateAsync(KasaTipi kasaTipi)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"INSERT INTO KasaTipleri (KasaTipiAdi, Aciklama)
                          VALUES (@KasaTipiAdi, @Aciklama);
                          SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await connection.ExecuteScalarAsync<int>(query, kasaTipi);
            return id;
        }

        public async Task<bool> UpdateAsync(KasaTipi kasaTipi)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"UPDATE KasaTipleri SET 
                          KasaTipiAdi = @KasaTipiAdi,
                          Aciklama = @Aciklama
                          WHERE KasaTipID = @KasaTipID";
            var affected = await connection.ExecuteAsync(query, kasaTipi);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM KasaTipleri WHERE KasaTipID = @Id";
            var affected = await connection.ExecuteAsync(query, new { Id = id });
            return affected > 0;
        }
    }
}
