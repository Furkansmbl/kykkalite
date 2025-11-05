using Dapper;
using HalApi.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalApi.Repositories.CariRepository
{
    public class CariRepository : ICariRepository
    {
        private readonly string _connectionString;

        public CariRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Cari>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Cariler";
            var result = await connection.QueryAsync<Cari>(query);
            return result.ToList();
        }

        public async Task<Cari?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Cariler WHERE CariID = @Id";
            return await connection.QueryFirstOrDefaultAsync<Cari>(query, new { Id = id });
        }

        public async Task<int> CreateAsync(Cari cari)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"INSERT INTO Cariler (CariTipi, Unvan, Telefon, Adres, VergiNo, Bakiye, OlusturmaTarihi)
                          VALUES (@CariTipi, @Unvan, @Telefon, @Adres, @VergiNo, @Bakiye, @OlusturmaTarihi);
                          SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await connection.ExecuteScalarAsync<int>(query, cari);
            return id;
        }

        public async Task<bool> UpdateAsync(Cari cari)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"UPDATE Cariler SET 
                          CariTipi = @CariTipi,
                          Unvan = @Unvan,
                          Telefon = @Telefon,
                          Adres = @Adres,
                          VergiNo = @VergiNo,
                          Bakiye = @Bakiye,
                          OlusturmaTarihi = @OlusturmaTarihi
                          WHERE CariID = @CariID";
            var affected = await connection.ExecuteAsync(query, cari);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM Cariler WHERE CariID = @Id";
            var affected = await connection.ExecuteAsync(query, new { Id = id });
            return affected > 0;
        }
    }
}
