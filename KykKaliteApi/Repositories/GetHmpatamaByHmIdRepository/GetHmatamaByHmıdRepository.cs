using Dapper;
using KykKaliteApi.Dtos.GetHmpatamaByHmIdDtos;
using KykKaliteApi.Dtos.GetUpatamaKodlariByUrunIDDtos;
using KykKaliteApi.Dtos.GetValueByMalzemeAciklamasiWParametreKodu;
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
        public async Task<List<ResultGetHmpatamaByHmıdDto>> GetHmatamaKodlariByHmIDNullAsync(string MalzemeAciklamasi, int fabrikaId, string THMID)
        {
            {
                string query = @"
SELECT 
    p.KontrolParametresi,
    up.AltOnaySiniri,
    up.UstOnaySiniri,
    u.MalzemeAciklamasi,
    u.HammaddeID,
    up.AltSartliKabulSiniri,
    up.UstSartliKabulSiniri,
    up.Versiyon,
    up.HMPA_ID,
    up.HMPAtamaKodu,
    u.HammaddeID,
    up.Versiyon,
    th.THMID
FROM 
    HMPatamaAktif up
INNER JOIN 
    Hammaddeler u ON u.HammaddeID = up.HammaddeID
INNER JOIN 
    Parametreler p ON p.ParametreKodu = up.ParametreKodu
INNER JOIN 
    TedarikciHammadde th ON th.MALZADI = u.MalzemeAciklamasi
WHERE 
    u.MalzemeAciklamasi = @malzemeAciklamasi 
    AND up.FabrikaID = @fabrikaId 
    AND th.THMID = @tHMID
";

                var parameters = new { MalzemeAciklamasi, fabrikaId, THMID };
                using (var connection = _context.CreateConnection())
                {
                    var values = await connection.QueryAsync<ResultGetHmpatamaByHmıdDto>(query, parameters);
                    return values.ToList();
                }
            }
        }
        public async Task<List<ResultGetHmpatamaByHmıdDto>> GetHmatamaKodlariByHmIDAsync(string MalzemeAciklamasi, int fabrikaId, string THMID, string Unvani)
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
	th.Unvanı,
    th.THMID,
	up.FabrikaID,
      STRING_AGG(CAST( th.THMID AS NVARCHAR(MAX)), '-') AS THMID,
    STRING_AGG(CAST(upn.Value AS NVARCHAR(MAX)), '-') AS Value,
    STRING_AGG(CAST(un.MalzemeUretimTarihi AS NVARCHAR(MAX)), '*') AS MalzemeUretimTarihi,
    STRING_AGG(CAST(un.SiraNo AS NVARCHAR(MAX)), '-') AS SiraNo,
    STRING_AGG(CAST(un.Saat AS NVARCHAR(MAX)), '-') AS Saat,
    STRING_AGG(CAST(un.MalzemeLotSeriNo AS NVARCHAR(MAX)), '-') AS MalzemeLotSeriNo,
      STRING_AGG(CASE 
                 WHEN CAST(un.AmirOnayDurumu AS NVARCHAR(MAX)) = '1' THEN 'Onay' 
                 ELSE CAST(un.AmirOnayDurumu AS NVARCHAR(MAX)) 
              END, '-') AS AmirOnayDurumu,
    STRING_AGG(CAST(un.Aciklama AS NVARCHAR(MAX)), '-') AS Aciklama,
    STRING_AGG(CAST(un.Trend AS NVARCHAR(MAX)), '-') AS Trend,
    STRING_AGG(CAST(un.IrsaliyeNo AS NVARCHAR(MAX)), '-') AS IrsaliyeNo,
    STRING_AGG(CAST(un.MalzemeLotSeriNo AS NVARCHAR(MAX)), '-') AS MalzemeLotSeriNo,
    STRING_AGG(CAST(un.MalzemeMiktarı AS NVARCHAR(MAX)), '-') AS MalzemeMiktarı,
    STRING_AGG(CAST(un.MiktarBirimi AS NVARCHAR(MAX)), '-') AS MiktarBirimi,
    STRING_AGG(CAST(un.MalzemeSKT AS NVARCHAR(MAX)), '-') AS MalzemeSKT,
    STRING_AGG(CAST(un.KYKBarkodNo AS NVARCHAR(MAX)), '-') AS KYKBarkodNo,
    STRING_AGG(CAST(un.MalzemeUretimTarihi AS NVARCHAR(MAX)), '-') AS MalzemeUretimTarihi,
    STRING_AGG(CAST(up.ParametreKritiklikSeviyesi AS NVARCHAR(MAX)), '-') AS ParametreKritiklikSeviyesi,
    (SELECT MAX(NumuneID)+1 FROM Unumune) AS LatestNumuneID,
      up.HmpaId,
   u.malzemeAciklamasi
   FROM HMPatamaAktif up
   INNER JOIN Hammaddeler u ON u.HammaddeID = up.HammaddeID
   INNER JOIN Parametreler p ON p.ParametreKodu = up.ParametreKodu 
   INNER JOIN 
    HMnumune un ON un.HammaddeID = u.HammaddeID
	INNER JOIN 
	TedarikciHammadde th ON th.MALZADI = U.MalzemeAciklamasi
	INNER JOIN
    HMPNvalue upn ON upn.NumuneID = un.NumuneID and upn.HMPAtamaKodu = up.HMPAtamaKodu
   WHERE u.MalzemeAciklamasi = @malzemeAciklamasi AND up.FabrikaID = @fabrikaId AND th.THMID = @THMID and th.UNVANI = @Unvani
   GROUP BY 
    p.KontrolParametresi,
    up.AltOnaySiniri,
    up.UstOnaySiniri,
	th.UNVANI,
	up.FabrikaID,
    u.HammaddeID,
    up.Versiyon,
    u.MalzemeAciklamasi,
    up.AltSartliKabulSiniri,
    up.UstSartliKabulSiniri,
    up.HMPAtamaKodu,
    up.HmpaId,
    th.THMID
ORDER BY 
    up.HmpaId ASC;";

                var parameters = new { MalzemeAciklamasi, fabrikaId, THMID, Unvani };
                using (var connection = _context.CreateConnection())
                {
                    var values = await connection.QueryAsync<ResultGetHmpatamaByHmıdDto>(query, parameters);
                    return values.ToList();
                }
            }
        }

        public async Task<List<ResultGetHmpatamaByHmıdDto>> GetTedarikci()
        {
            string query = @"
  SELECT DISTINCT ur.MalzemeAciklamasi, par.UNVANI, par.THMID, f.FabrikaID
  FROM Hammaddeler ur
  JOIN TedarikciHammadde par ON ur.MalzemeAciklamasi = par.MALZADI
  JOIN Fabrikalar f ON F.FabrikaAdi = PAR.ISYERIADI";

         
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultGetHmpatamaByHmıdDto>(query);
                return values.ToList(); ;
            }
        }
    }
}
