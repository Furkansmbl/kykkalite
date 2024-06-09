using Dapper;
using KykKaliteApi.Dtos.GetUpatamaKodlariByUrunIDDtos;
using KykKaliteApi.Dtos.GetValueByMalzemeAciklamasiWParametreKodu;
using KykKaliteApi.Models.DapperContext;
using System.Web;

namespace KykKaliteApi.Repositories.GetValueByMalzemeAciklamasiWParametreKodu
{
    public class GetValueRepository : IGetValueRepository
    {
        private readonly Context _context;

        public GetValueRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultGetValueDto>> GetValueByMalzemeAciklamasiWParametreKoduAsync(string malzemeaciklamasi, string kontrolparametresi)
        {
            string query = @"
        SELECT TOP 49 upn.Value,
        upa.AltOnaySiniri,
        upa.AltSartliKabulSiniri,
          upa.UstSartliKabulSiniri,
        upa.UstOnaySiniri,
        par.ParametreTipiOlcmeGozlem
        FROM upnvalue upn
        JOIN upatama upa ON upn.upatamakodu = upa.upatamakodu
        JOIN urunler ur ON upa.UrunID = ur.UrunID
        JOIN parametreler par ON upa.ParametreKodu = par.ParametreKodu
        WHERE ur.malzemeaciklamasi = @malzemeaciklamasi
        AND par.kontrolparametresi = @kontrolparametresi
        ORDER BY upn.EklenmeTarihi DESC";

            var parameters = new { malzemeaciklamasi, kontrolparametresi };
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultGetValueDto>(query, parameters);
                return values.ToList();
            }
        }
    }
}
