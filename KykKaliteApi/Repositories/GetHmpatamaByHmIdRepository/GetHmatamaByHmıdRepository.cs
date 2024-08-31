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
   up.AltOnaySiniri,
   up.UstOnaySiniri,
    u.MalzemeAciklamasi,
   u.HammaddeID,
   up.AltSartliKabulSiniri,
   up.UstSartliKabulSiniri,
    up.Versiyon,
        STRING_AGG(CAST(upn.Value AS NVARCHAR(MAX)), '-') AS Value,
    STRING_AGG(CAST(un.MalzemeUretimTarihi AS NVARCHAR(MAX)), '*') AS UretimTarihi,
    STRING_AGG(CAST(un.SiraNo AS NVARCHAR(MAX)), '-') AS SiraNo,
    STRING_AGG(CAST(un.Saat AS NVARCHAR(MAX)), '*') AS KontrolSaati,
    STRING_AGG(CAST(un.MalzemeLotSeriNo AS NVARCHAR(MAX)), '-') AS NumuneSeriNoSarjNo,
    STRING_AGG(CAST(un.AmirOnayDurumu AS NVARCHAR(MAX)), '-') AS AmirOnayDurumu,
    STRING_AGG(CAST(un.Aciklama AS NVARCHAR(MAX)), '-') AS Aciklama,
      up.HMPA_ID,
   u.malzemeAciklamasi
   FROM HMPatamaAktif up
   INNER JOIN Hammaddeler u ON u.HammaddeID = up.HammaddeID
   INNER JOIN Parametreler p ON p.ParametreKodu = up.ParametreKodu 
   INNER JOIN 
    HMnumune un ON un.HammaddeID = u.HammaddeID
INNER JOIN 
    HMPNvalue upn ON upn.NumuneID = un.NumuneID and upn.HMPAtamaKodu = up.HMPAtamaKodu
   WHERE u.MalzemeAciklamasi = @malzemeAciklamasi
   GROUP BY 
    p.KontrolParametresi,
    up.AltOnaySiniri,
    up.UstOnaySiniri,
    u.HammaddeID,
    up.Versiyon,
    u.MalzemeAciklamasi,
    up.AltSartliKabulSiniri,
    up.UstSartliKabulSiniri,
    up.HMPAtamaKodu,
    up.HMPA_ID
ORDER BY 
    up.HMPA_ID ASC;";

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
