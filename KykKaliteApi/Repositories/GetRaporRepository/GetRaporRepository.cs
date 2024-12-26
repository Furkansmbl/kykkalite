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
        public async Task<List<ResultGetRaporDtos>> GetRaporHammaddeDtosAsync(int FabrikaId, string malzemeAciklamasi,string OlusturmaTarihi, string BitisTarihi ,string UNVANI)
        {
            string query = @"
WITH DurumCounts AS (
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
        p.KontrolParametresi,
        p.ParametreTipiOlcmeGozlem,
        th.UNVANI,
        CASE 
            WHEN TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) < upa.AltSartliKabulSiniri 
                 OR TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) > upa.UstSartliKabulSiniri 
                THEN 'Red'
            WHEN (TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) >= upa.AltSartliKabulSiniri 
                  AND TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) <= upa.UstSartliKabulSiniri)
                 AND (TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) < upa.AltOnaySiniri 
                      OR TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) > upa.UstOnaySiniri)
                THEN 'SartliOnay'
            WHEN TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) >= upa.AltOnaySiniri 
                 AND TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) <= upa.UstOnaySiniri
                THEN 'Onay'
        END AS Durum,
        SUM(CASE 
            WHEN TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) >= upa.AltOnaySiniri 
                 AND TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) <= upa.UstOnaySiniri 
            THEN 1 ELSE 0 END) OVER (PARTITION BY p.KontrolParametresi) AS Onay,
        SUM(CASE 
            WHEN (TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) >= upa.AltSartliKabulSiniri 
                  AND TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) <= upa.UstSartliKabulSiniri)
                 AND (TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) < upa.AltOnaySiniri 
                      OR TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) > upa.UstOnaySiniri) 
            THEN 1 ELSE 0 END) OVER (PARTITION BY p.KontrolParametresi) AS SartliOnay,
        SUM(CASE 
            WHEN TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) < upa.AltSartliKabulSiniri 
                 OR TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) > upa.UstSartliKabulSiniri 
            THEN 1 ELSE 0 END) OVER (PARTITION BY p.KontrolParametresi) AS Red,
        SUM(CASE 
            WHEN TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) >= upa.AltOnaySiniri 
                 AND TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) <= upa.UstOnaySiniri 
            THEN 1 ELSE 0 END 
            + CASE WHEN (TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) >= upa.AltSartliKabulSiniri 
                    AND TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) <= upa.UstSartliKabulSiniri) 
                    AND (TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) < upa.AltOnaySiniri 
                         OR TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) > upa.UstOnaySiniri) 
                   THEN 1 ELSE 0 END 
            + CASE WHEN TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) < upa.AltSartliKabulSiniri 
                    OR TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) > upa.UstSartliKabulSiniri 
                   THEN 1 ELSE 0 END) 
        OVER (PARTITION BY p.KontrolParametresi) AS Genel,
        ROW_NUMBER() OVER (PARTITION BY u.NumuneID ORDER BY uv.OlusturmaTarihi DESC) AS rn
    FROM 
        HMnumune u
    JOIN 
        Hammaddeler ur ON ur.HammaddeID = u.HammaddeID
    JOIN 
        HMPNvalue uv ON u.NumuneID = uv.NumuneID
    JOIN 
        HMPatamaAktif upa ON uv.HMPAtamaKodu = upa.HMPAtamaKodu
    JOIN 
        Parametreler p ON upa.ParametreKodu = p.ParametreKodu
    JOIN
        TedarikciHammadde th ON th.THMID = u.THMID
    WHERE 
        ur.MalzemeAciklamasi = @malzemeAciklamasi
        AND LEFT(uv.OlusturmaTarihi, 10) BETWEEN @OlusturmaTarihi AND @BitisTarihi
        AND upa.FabrikaID = @FabrikaId
        AND p.ParametreTipiOlcmeGozlem = 1
        AND th.UNVANI = @UNVANI
)
SELECT 
    NumuneID,
    Value,
    PersonelSicilNo,
    ParametreTipiOlcmeGozlem,
    AmirOnayDurumu, 
    OlusturmaTarihi, 
    AltSartliKabulSiniri,
    AltOnaySiniri,
    UstOnaySiniri,
    UstSartliKabulSiniri,
    MalzemeAciklamasi,
    KontrolParametresi,
    Durum,
    UNVANI,
    Onay,
    SartliOnay,
    Red,
    Genel
FROM 
    DurumCounts
WHERE 
    rn = 1;

 	;";

            var parameters = new { FabrikaId, malzemeAciklamasi, OlusturmaTarihi, BitisTarihi, UNVANI };
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultGetRaporDtos>(query, parameters);
                return values.ToList();
            }
        }
        public async Task<List<ResultGetRaporDtos>> GetRaporDtosAsync(int FabrikaId, string malzemeAciklamasi , string OlusturmaTarihi, string BitisTarihi)
        {
            string query = @"
WITH DurumCounts AS (
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
        p.KontrolParametresi,
        p.ParametreTipiOlcmeGozlem,
        CASE 
            WHEN TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) < upa.AltSartliKabulSiniri 
                 OR TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) > upa.UstSartliKabulSiniri 
                THEN 'Red'
            WHEN (TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) >= upa.AltSartliKabulSiniri 
                  AND TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) <= upa.UstSartliKabulSiniri)
                 AND (TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) < upa.AltOnaySiniri 
                      OR TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) > upa.UstOnaySiniri)
                THEN 'SartliOnay'
            WHEN TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) >= upa.AltOnaySiniri 
                 AND TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) <= upa.UstOnaySiniri
                THEN 'Onay'
        END AS Durum,
        SUM(CASE 
            WHEN TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) >= upa.AltOnaySiniri 
                 AND TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) <= upa.UstOnaySiniri 
            THEN 1 ELSE 0 END) OVER (PARTITION BY p.KontrolParametresi) AS Onay,
        SUM(CASE 
            WHEN (TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) >= upa.AltSartliKabulSiniri 
                  AND TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) <= upa.UstSartliKabulSiniri)
                 AND (TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) < upa.AltOnaySiniri 
                      OR TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) > upa.UstOnaySiniri) 
            THEN 1 ELSE 0 END) OVER (PARTITION BY p.KontrolParametresi) AS SartliOnay,
        SUM(CASE 
            WHEN TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) < upa.AltSartliKabulSiniri 
                 OR TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) > upa.UstSartliKabulSiniri 
            THEN 1 ELSE 0 END) OVER (PARTITION BY p.KontrolParametresi) AS Red,
        -- Genel toplam sütunu
        SUM(CASE 
            WHEN TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) >= upa.AltOnaySiniri 
                 AND TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) <= upa.UstOnaySiniri 
            THEN 1 ELSE 0 END 
            + CASE WHEN (TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) >= upa.AltSartliKabulSiniri 
                    AND TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) <= upa.UstSartliKabulSiniri) 
                    AND (TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) < upa.AltOnaySiniri 
                         OR TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) > upa.UstOnaySiniri) 
                   THEN 1 ELSE 0 END 
            + CASE WHEN TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) < upa.AltSartliKabulSiniri 
                    OR TRY_CAST(REPLACE(uv.Value, ',', '.') AS float) > upa.UstSartliKabulSiniri 
                   THEN 1 ELSE 0 END) 
        OVER (PARTITION BY p.KontrolParametresi) AS Genel,
        -- Her KontrolParametresi için en güncel kayıt için satır numarası
        ROW_NUMBER() OVER (PARTITION BY p.KontrolParametresi ORDER BY uv.OlusturmaTarihi DESC) AS rn
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
        ur.MalzemeAciklamasi = @malzemeAciklamasi
        AND LEFT(uv.OlusturmaTarihi, 10) BETWEEN @OlusturmaTarihi AND @BitisTarihi
        AND upa.FabrikaID = @FabrikaID
        AND p.ParametreTipiOlcmeGozlem = 1
)
SELECT 
    NumuneID,
    Value,
    PersonelSicilNo,
    ParametreTipiOlcmeGozlem,
    AmirOnayDurumu, 
    OlusturmaTarihi, 
    AltSartliKabulSiniri,
    AltOnaySiniri,
    UstOnaySiniri,
    UstSartliKabulSiniri,
    MalzemeAciklamasi,
    KontrolParametresi,
    Durum,
    Onay,
    SartliOnay,
    Red,
    Genel
FROM 
    DurumCounts;

	
	

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
