using Dapper;
using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.GetUpatamaKodlariByUrunIDDtos;
using KykKaliteApi.Dtos.HammaddeGruplariDtos;
using KykKaliteApi.Models.DapperContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace KykKaliteApi.Repositories.GetUpatamaKodlariByUrunIDRepository
{
    public class GetUpatamaKodlariByUrunIDRepository : IGetUpatamaKodlariByUrunIDRepository
    {
        private readonly Context _context;

        public GetUpatamaKodlariByUrunIDRepository(Context context)
        {
            _context = context;
        }

       

        public async Task<List<ResultGetUpatamaKodlariByUrunIDDto>> GetUpatamaKodlariByUrunIDAsync(string MalzemeAciklamasi)
        {
            {
                string query = @"
       SELECT p.KontrolParametresi,
   up.AltOnaySiniri,
   up.UstOnaySiniri,
    u.MalzemeAciklamasi,
   u.UrunID,
   up.AltSartliKabulSiniri,
   up.UstSartliKabulSiniri,
    up.Versiyon,
        STRING_AGG(CAST(upn.Value AS NVARCHAR(MAX)), '-') AS Value,
    STRING_AGG(CAST(un.UretimTarihi AS NVARCHAR(MAX)), '*') AS UretimTarihi,
    STRING_AGG(CAST(un.SiraNo AS NVARCHAR(MAX)), '-') AS SiraNo,
    STRING_AGG(CAST(un.KontrolSaati AS NVARCHAR(MAX)), '*') AS KontrolSaati,
    STRING_AGG(CAST(un.NumuneSeriNoSarjNo AS NVARCHAR(MAX)), '-') AS NumuneSeriNoSarjNo,
    STRING_AGG(CAST(un.AmirOnayDurumu AS NVARCHAR(MAX)), '-') AS AmirOnayDurumu,
    STRING_AGG(CAST(un.MudahaleVarmi AS NVARCHAR(MAX)), '-') AS MudahaleVarmi,
    STRING_AGG(CAST(un.Aciklama AS NVARCHAR(MAX)), '-') AS Aciklama,
      up.UpaId,
   u.malzemeAciklamasi
   FROM upatamaAktif up
   INNER JOIN urunler u ON u.urunıd = up.UrunID
   INNER JOIN Parametreler p ON p.ParametreKodu = up.ParametreKodu 
   INNER JOIN 
    Unumune un ON un.UrunID = u.UrunID
INNER JOIN 
    UPNvalue upn ON upn.NumuneID = un.NumuneID and upn.UPAtamaKodu = up.UPAtamaKodu
   WHERE u.MalzemeAciklamasi = @malzemeAciklamasi
   GROUP BY 
    p.KontrolParametresi,
    up.AltOnaySiniri,
    up.UstOnaySiniri,
    u.UrunID,
    up.Versiyon,
    u.MalzemeAciklamasi,
    up.AltSartliKabulSiniri,
    up.UstSartliKabulSiniri,
    up.UPAtamaKodu,
    up.UpaId
ORDER BY 
    up.UpaId ASC;";

                var parameters = new { MalzemeAciklamasi };
                using (var connection = _context.CreateConnection())
                {
                    var values = await connection.QueryAsync<ResultGetUpatamaKodlariByUrunIDDto>(query, parameters);
                    return values.ToList();
                }
            }
        }
    }

}