using Dapper;
using HalApi.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalApi.Repositories.FaturaRepository
{
    public class FaturaRepository : IFaturaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public FaturaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Fatura>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"SELECT FaturaID, FaturaNo, CariID, FaturaTarihi, ToplamTutar, Aciklama 
                          FROM Faturalar";
            var result = await connection.QueryAsync<Fatura>(query);
            return result.ToList();
        }

        public async Task<Fatura?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"SELECT FaturaID, FaturaNo, CariID, FaturaTarihi, ToplamTutar, Aciklama 
                          FROM Faturalar WHERE FaturaID = @Id";
            return await connection.QueryFirstOrDefaultAsync<Fatura>(query, new { Id = id });
        }

        public async Task CreateAsync(Fatura fatura)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"INSERT INTO Faturalar (FaturaNo, CariID, FaturaTarihi, ToplamTutar, Aciklama)
                          VALUES (@FaturaNo, @CariID, @FaturaTarihi, @ToplamTutar, @Aciklama)";
            await connection.ExecuteAsync(query, fatura);
        }

        public async Task UpdateAsync(Fatura fatura)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"UPDATE Faturalar SET 
                          FaturaNo = @FaturaNo,
                          CariID = @CariID,
                          FaturaTarihi = @FaturaTarihi,
                          ToplamTutar = @ToplamTutar,
                          Aciklama = @Aciklama
                          WHERE FaturaID = @FaturaID";
            await connection.ExecuteAsync(query, fatura);
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM Faturalar WHERE FaturaID = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
