using Dapper;
using KykKaliteApi.Dtos.GetUpatamaKodlariByUrunIDDtos;
using KykKaliteApi.Dtos.HammaddeGruplariDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.GetUpatamaKodlariByUrunIDRepository
{
    public class GetUpatamaKodlariByUrunIDRepository : IGetUpatamaKodlariByUrunIDRepository
    {
        private readonly Context _context;
        public GetUpatamaKodlariByUrunIDRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultGetUpatamaKodlariByUrunIDDto>> GetUpatamaKodlariByUrunIDAsync(int urunId)
        {
            string query = "SELECT U.UrunID, U.MalzemeKodu, U.MalzemeAciklamasi, UP.UPAtamaKodu, UP.ParametreKodu, N.NumuneId " +
                "From Urunler U WITH(NOLOCK) INNER JOIN UPatama UP WITH(NOLOCK) ON U.UrunID = UP.UrunID" +
                " INNER JOIN Unumune N WITH(NOLOCK) ON U.UrunID = N.UrunID  INNER JOIN UPNvalue UV WITH(NOLOCK) ON UP.UPAtamaKodu = UV.UPAtamaKodu WHERE UV.NumuneID = @numuneId AND U.UrunID = @urunId";
            var parameters = new DynamicParameters();
            parameters.Add("@urunId", urunId);
            using (var connection = _context.CreateConnection()) 
            {
                var values = await connection.QueryAsync<ResultGetUpatamaKodlariByUrunIDDto>(query,parameters);
                return values.ToList();
            }
        }
    }
}
