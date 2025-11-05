using Dapper;
using HalApi.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalApi.Repositories.UrunRepository
{
    public class UrunRepository : IUrunRepository
    {
        private readonly string _connectionString;

        public UrunRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Urun>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Urunler";
            var result = await connection.QueryAsync<Urun>(query);
            return result.ToList();
        }

        public async Task<Urun?> GetByIdAsync(int urunId)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Urunler WHERE UrunID = @UrunID";
            return await connection.QueryFirstOrDefaultAsync<Urun>(query, new { UrunID = urunId });
        }

        public async Task<int> CreateAsync(Urun urun)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"INSERT INTO Urunler (UrunAdi, VarsayilanBirim, VarsayilanKasaTipID, AlisFiyati, SatisFiyati, StokMiktari)
                          VALUES (@UrunAdi, @VarsayilanBirim, @VarsayilanKasaTipID, @AlisFiyati, @SatisFiyati, @StokMiktari);
                          SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await connection.ExecuteScalarAsync<int>(query, urun);
            return id;
        }

        public async Task<bool> UpdateAsync(Urun urun)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"UPDATE Urunler SET
                          UrunAdi = @UrunAdi,
                          VarsayilanBirim = @VarsayilanBirim,
                          VarsayilanKasaTipID = @VarsayilanKasaTipID,
                          AlisFiyati = @AlisFiyati,
                          SatisFiyati = @SatisFiyati,
                          StokMiktari = @StokMiktari
                          WHERE UrunID = @UrunID";
            var affected = await connection.ExecuteAsync(query, urun);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int urunId)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM Urunler WHERE UrunID = @UrunID";
            var affected = await connection.ExecuteAsync(query, new { UrunID = urunId });
            return affected > 0;
        }
    }
}
