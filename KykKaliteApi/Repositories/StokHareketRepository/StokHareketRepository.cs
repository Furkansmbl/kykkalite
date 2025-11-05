using Dapper;
using HalApi.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalApi.Repositories.StokHareketRepository
{
    public class StokHareketRepository : IStokHareketRepository
    {
        private readonly string _connectionString;

        public StokHareketRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<StokHareket>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM StokHareketler";
            var result = await connection.QueryAsync<StokHareket>(query);
            return result.ToList();
        }

        public async Task<StokHareket?> GetByIdAsync(int hareketId)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM StokHareketler WHERE HareketID = @HareketID";
            return await connection.QueryFirstOrDefaultAsync<StokHareket>(query, new { HareketID = hareketId });
        }

        public async Task<int> CreateAsync(StokHareket stokHareket)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"INSERT INTO StokHareketler (UrunID, HareketTipi, KasaTipID, Birim, Miktar, Aciklama, Tarih)
                          VALUES (@UrunID, @HareketTipi, @KasaTipID, @Birim, @Miktar, @Aciklama, @Tarih);
                          SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await connection.ExecuteScalarAsync<int>(query, stokHareket);
            return id;
        }

        public async Task<bool> UpdateAsync(StokHareket stokHareket)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"UPDATE StokHareketler SET
                          UrunID = @UrunID,
                          HareketTipi = @HareketTipi,
                          KasaTipID = @KasaTipID,
                          Birim = @Birim,
                          Miktar = @Miktar,
                          Aciklama = @Aciklama,
                          Tarih = @Tarih
                          WHERE HareketID = @HareketID";
            var affected = await connection.ExecuteAsync(query, stokHareket);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int hareketId)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM StokHareketler WHERE HareketID = @HareketID";
            var affected = await connection.ExecuteAsync(query, new { HareketID = hareketId });
            return affected > 0;
        }

        public async Task<List<StokHareket>> GetByUrunIdAsync(int urunId)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM StokHareketler WHERE UrunID = @UrunID";
            var result = await connection.QueryAsync<StokHareket>(query, new { UrunID = urunId });
            return result.ToList();
        }
    }
}
