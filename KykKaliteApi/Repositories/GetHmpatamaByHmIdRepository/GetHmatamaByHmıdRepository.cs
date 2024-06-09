using Dapper;
using KykKaliteApi.Dtos.GetHmpatamaByHmIdDtos;
using KykKaliteApi.Dtos.GetUpatamaKodlariByUrunIDDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.GetHmpatamaByHmIdRepository
{
    public class GetHmatamaByHmıdRepository : IGetHmpatamaByHmıdRepository
    {
        private readonly Context _context;

        public GetHmatamaByHmıdRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultGetHmpatamaByHmıdDto>> GetHmatamaKodlariByHmIDAsync(string MalzemeAciklamasi)
        {
            {
                string query = @"
                     SELECT p.KontrolParametresi,
                        h.HammaddeID,
                        h.MalzemeAciklamasi,
	                    hp.AltOnaySiniri,
	                    hp.UstOnaySiniri,
                        hp.AltSartliKabulSiniri,
	                    hp.UstSartliKabulSiniri
                        FROM HMPatama hp
                        INNER JOIN Hammaddeler h ON h.hammaddeId= hp.hammaddeId
                        INNER JOIN Parametreler p ON p.ParametreKodu = hp.ParametreKodu
                        WHERE h.MalzemeAciklamasi = @malzemeAciklamasi;";

                var parameters = new { MalzemeAciklamasi };
                using (var connection = _context.CreateConnection())
                {
                    var values = await connection.QueryAsync<ResultGetHmpatamaByHmıdDto>(query, parameters);
                    return values.ToList();
                }
            }
        }
    }
}
