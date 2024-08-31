using Dapper;
using KykKaliteApi.Dtos.GetRaporDtos;
using KykKaliteApi.Dtos.GetValueByMalzemeAciklamasiWParametreKodu;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.GetRaporRepository
{
    public class GetRaporRepository : IGetRaporRepository
    {
        private readonly Context _context;

        public GetRaporRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultGetRaporDtos>> GetRaporDtosAsync(int FabrikaId, string malzemeAciklamasi, string OlusturmaTarihi, string BitisTarihi)
        {
            string query = @"
      SELECT 
    u.NumuneID,
	uv.Value,
    u.PersonelSicilNo,
    u.AmirOnayDurumu, 
    uv.OlusturmaTarihi, 
    upa.AltSartliKabulSiniri,
    upa.AltOnaySiniri,
    upa.UstOnaySiniri,
    upa.UstSartliKabulSiniri,
    ur.MalzemeAciklamasi,
    p.KontrolParametresi
FROM 
    unumune u
JOIN 
    Urunler ur ON ur.UrunID = u.UrunID
JOIN 
    upnValue uv ON u.NumuneID = uv.NumuneID
JOIN 
    UPatamaAktif upa ON uv.UPAtamaKodu = upa.UPAtamaKodu
JOIN 
    Parametreler p ON upa.ParametreKodu = p.ParametreKodu
WHERE 
    upa.FabrikaID = @fabrikaId
    AND ur.MalzemeAciklamasi = @malzemeAciklamasi
    AND CONVERT(Date, CASE WHEN isDate (u.OlusturmaTarihi)=1 THEN u.OlusturmaTarihi ELSE NULL END) BETWEEN Convert(Date, @olusturmaTarihi ) AND CONVERT (Date, @bitisTarihi )	
	;";

            var parameters = new { FabrikaId, malzemeAciklamasi, OlusturmaTarihi, BitisTarihi };
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultGetRaporDtos>(query, parameters);
                return values.ToList();
            }
        }

        public async  Task<List<ResultGetRaporDtos>> GetRaporDtosNotKontrolAsync(int FabrikaId, string malzemeAciklamasi, string OlusturmaTarihi, string BitisTarihi, string amirOnayDurumu)
        {
            string query = @"
       SELECT 
	u.NumuneID,
	u.PersonelSicilNo,
    u.AmirOnayDurumu, 
    uv.OlusturmaTarihi, 
    upa.AltSartliKabulSiniri,
    upa.AltOnaySiniri,
    upa.UstOnaySiniri,
    upa.UstSartliKabulSiniri,
    ur.MalzemeAciklamasi,
    p.KontrolParametresi
   FROM unumune u
JOIN Urunler ur ON ur.UrunID = u.UrunID
JOIN upnValue uv ON u.numuneId = uv.numuneId
JOIN UPatamaAktif upa ON uv.UPAtamaKodu = upa.UPAtamaKodu
JOIN Parametreler p ON upa.ParametreKodu = p.ParametreKodu
WHERE upa.FabrikaID = @fabrikaId
  AND ur.MalzemeAciklamasi = @malzemeAciklamasi
    AND u.AmirOnayDurumu = @amirOnayDurumu
  AND u.olusturmaTarihi BETWEEN @olusturmaTarihi AND @bitisTarihi
  AND (TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) < upa.AltSartliKabulSiniri
       OR TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) > upa.UstSartliKabulSiniri
       OR TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) < upa.AltOnaySiniri
       OR TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) > upa.UstOnaySiniri);";

            var parameters = new { FabrikaId, malzemeAciklamasi, OlusturmaTarihi, BitisTarihi, amirOnayDurumu };
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultGetRaporDtos>(query, parameters);
                return values.ToList();
            }
        }
    }
}
