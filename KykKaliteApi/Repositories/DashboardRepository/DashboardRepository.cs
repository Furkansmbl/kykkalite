using Dapper;
using KykKaliteApi.Dtos.DashboardDtos;
using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.DashboardRepository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly Context _context;
        public DashboardRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<FabrikaOnayDto>> GetAllTedarikciSartliOnayHammaddeAsync()
        {
            string query = @"
            SELECT   
    f.FabrikaID,
    SUM(CASE WHEN u.AmirOnayDurumu = '1' THEN 1 ELSE 0 END) AS Onay,
    SUM(CASE WHEN u.AmirOnayDurumu = 'SartliOnay' THEN 1 ELSE 0 END) AS SartliOnaySayisi,
    SUM(CASE WHEN u.AmirOnayDurumu = 'Red' THEN 1 ELSE 0 END) AS RedSayisi,
    uh.UNVANI,
    -- Yeni sütun: SartliOnay oranı
    CAST(SUM(CASE WHEN u.AmirOnayDurumu = 'SartliOnay' THEN 1 ELSE 0 END) AS FLOAT) 
    / NULLIF(
        SUM(
            CASE WHEN u.AmirOnayDurumu = '1' THEN 1 ELSE 0 END
        ) +
        SUM(
            CASE WHEN u.AmirOnayDurumu = 'SartliOnay' THEN 1 ELSE 0 END
        ) +
        SUM(
            CASE WHEN u.AmirOnayDurumu = 'Red' THEN 1 ELSE 0 END
        ),
        0
    ) AS SartliOnayOrani
FROM 
    HMnumune u
JOIN 
    TedarikciHammadde uh ON uh.THMID = u.THMID
JOIN 
    Fabrikalar f ON f.FabrikaAdi = uh.ISYERIADI
GROUP BY 
    f.FabrikaID,
    uh.UNVANI
ORDER BY 
    SartliOnayOrani DESC;

            ";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<FabrikaOnayDto>(query);
                return values.ToList();
            }
        }
        public async Task<List<FabrikaOnayDto>> GetAllTedarikciRedHammaddeAsync()
        {
            string query = @"
                SELECT   
    f.FabrikaID,
    SUM(CASE WHEN u.AmirOnayDurumu = '1' THEN 1 ELSE 0 END) AS Onay,
    SUM(CASE WHEN u.AmirOnayDurumu = 'SartliOnay' THEN 1 ELSE 0 END) AS SartliOnaySayisi,
    SUM(CASE WHEN u.AmirOnayDurumu = 'Red' THEN 1 ELSE 0 END) AS RedSayisi,
    uh.UNVANI,
    -- Yeni sütun: Oran hesabı
    CAST(SUM(CASE WHEN u.AmirOnayDurumu = 'Red' THEN 1 ELSE 0 END) AS FLOAT) 
    / NULLIF(
        SUM(
            CASE WHEN u.AmirOnayDurumu = '1' THEN 1 ELSE 0 END
        ) +
        SUM(
            CASE WHEN u.AmirOnayDurumu = 'SartliOnay' THEN 1 ELSE 0 END
        ) +
        SUM(
            CASE WHEN u.AmirOnayDurumu = 'Red' THEN 1 ELSE 0 END
        ),
        0
    ) AS RedOrani
FROM 
    HMnumune u
JOIN 
    TedarikciHammadde uh ON uh.THMID = u.THMID
JOIN 
    Fabrikalar f ON f.FabrikaAdi = uh.ISYERIADI
GROUP BY 
    f.FabrikaID,
    uh.UNVANI
ORDER BY 
    RedOrani DESC;

            ";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<FabrikaOnayDto>(query);
                return values.ToList();
            }
        }
        public async Task<List<UrunDegKatDto>> GetAllDegKatAsync()
        {
            string query = @"
    WITH ValueData AS (
    SELECT 
        ur.MalzemeAciklamasi,
        upa.UPAtamaKodu,
        upa.AltOnaySiniri,
        upa.UstOnaySiniri,
        upa.FabrikaID,
        upa.ParametreKodu,
        TRY_CAST(REPLACE(uv.Value, ',', '.') AS FLOAT) AS Value,
        p.ParametreTipiOlcmeGozlem,
        ROW_NUMBER() OVER (PARTITION BY ur.MalzemeAciklamasi, upa.UPAtamaKodu, upa.ParametreKodu 
                           ORDER BY u.olusturmatarihi DESC) AS RowNum  -- Order by olusturmatarihi descending
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
        p.ParametreTipiOlcmeGozlem = '1' 
)

SELECT
    MalzemeAciklamasi,
    UPatamaKodu,
    AltOnaySiniri,
    UstOnaySiniri,
    FabrikaID,
    ParametreKodu,
    ParametreTipiOlcmeGozlem,
    STRING_AGG(CAST(Value AS NVARCHAR(MAX)), '-') WITHIN GROUP (ORDER BY RowNum ASC) AS BirlesikValue,  
    CASE 
        WHEN AVG(Value) IS NULL OR AVG(Value) = 0 THEN 0
        ELSE ROUND(STDEV(Value) / AVG(Value) * 100, 2)  -- Multiply by 100 and round to two decimal places
    END AS DegisimKatsayisi
FROM 
    ValueData
WHERE 
    Value IS NOT NULL 
GROUP BY
    MalzemeAciklamasi,
    UPatamaKodu,
    AltOnaySiniri,
    UstOnaySiniri,
    ParametreKodu,
    ParametreTipiOlcmeGozlem,
    FabrikaID
ORDER BY
    DegisimKatsayisi DESC;

";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<UrunDegKatDto>(query);
                return values.ToList();
            }
        }
        public async Task<List<UrunDegKatDto>> GetAllDegKatHmAsync()
        {
            string query = @"
                WITH ValueData AS (
    SELECT 
        ur.MalzemeAciklamasi,
        upa.HMPAtamaKodu,
        upa.AltOnaySiniri,
        upa.UstOnaySiniri,
        upa.FabrikaID,
        upa.ParametreKodu,
              TRY_CAST(REPLACE(uv.Value, ',', '.') AS FLOAT) AS Value ,
        p.ParametreTipiOlcmeGozlem,
        ROW_NUMBER() OVER (PARTITION BY ur.MalzemeAciklamasi, upa.HMPAtamaKodu, upa.ParametreKodu 
                           ORDER BY u.olusturmatarihi ASC) AS RowNum  -- Tarihe göre artan sırada
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
    WHERE 
        p.ParametreTipiOlcmeGozlem = '1' 
)

SELECT
    MalzemeAciklamasi,
    HMPAtamaKodu,
    AltOnaySiniri,
    UstOnaySiniri,
    FabrikaID,
    ParametreKodu,
    ParametreTipiOlcmeGozlem,
    STRING_AGG(CAST(Value AS NVARCHAR(MAX)), '-') WITHIN GROUP (ORDER BY RowNum DESC) AS BirlesikValue,  -- RowNum'a göre artan sıralama
    CASE 
        WHEN AVG(Value) IS NULL OR AVG(Value) = 0 THEN 0
        ELSE STDEV(Value) / AVG(Value) 
    END AS DegisimKatsayisi
FROM 
    ValueData
WHERE 
    Value IS NOT NULL 
GROUP BY
    MalzemeAciklamasi,
    HMPAtamaKodu,
    AltOnaySiniri,
    UstOnaySiniri,
    ParametreKodu,
    ParametreTipiOlcmeGozlem,
    FabrikaID
ORDER BY
    DegisimKatsayisi DESC;

            ";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<UrunDegKatDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<FabrikaOnayDto>> GetAllFabrikaOnayAsync()
        {
            string query = @"
                    SELECT  f.FabrikaID,
    SUM(CASE WHEN u.AmirOnayDurumu = '1' THEN 1 ELSE 0 END) AS Onay,
    SUM(CASE WHEN u.AmirOnayDurumu = 'SartliOnay' THEN 1 ELSE 0 END) AS SartliOnaySayisi,
    SUM(CASE WHEN u.AmirOnayDurumu = 'Red' THEN 1 ELSE 0 END) AS RedSayisi
FROM 
    unumune u
JOIN 
    UretimHatlari uh ON uh.HatAdiAciklamasi = u.HatAdiAciklamasi
JOIN 
    Fabrikalar f ON f.FabrikaID = uh.FabrikaID
GROUP BY 
    f.FabrikaID 
            ";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<FabrikaOnayDto>(query);
                return values.ToList();
            }
        }
        public async Task<List<FabrikaOnayDto>> GetAllFabrikaOnayHammaddeAsync()
        {
            string query = @"
                   Select   f.FabrikaID,
    SUM(CASE WHEN u.AmirOnayDurumu = '1' THEN 1 ELSE 0 END) AS Onay,
    SUM(CASE WHEN u.AmirOnayDurumu = 'SartliOnay' THEN 1 ELSE 0 END) AS SartliOnaySayisi,
    SUM(CASE WHEN u.AmirOnayDurumu = 'Red' THEN 1 ELSE 0 END) AS RedSayisi
FROM 
    HMnumune u
JOIN 
    TedarikciHammadde uh ON uh.THMID = u.THMID
JOIN 
    Fabrikalar f ON f.FabrikaAdi = uh.ISYERIADI
GROUP BY 
    f.FabrikaID 
            ";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<FabrikaOnayDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<UrunOranDto>> GetAllUrunOranAsync()
        {
            string query = @"
WITH ValueData AS (
    SELECT 
        ur.MalzemeAciklamasi,
        upa.UPAtamaKodu,
        upa.AltOnaySiniri,
        upa.UstOnaySiniri,
        upa.FabrikaID,
        upa.ParametreKodu,
      TRY_CAST(REPLACE(uv.Value, ',', '.') AS FLOAT) AS Value ,
        p.ParametreTipiOlcmeGozlem,
        u.NumuneSeriNoSarjNo,
        u.OlusturmaTarihi  -- Include creation date for ordering
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
        p.ParametreTipiOlcmeGozlem = '1' 
),
AggregatedData AS (
    SELECT
        MalzemeAciklamasi,
        UPatamaKodu,
        AltOnaySiniri,
        UstOnaySiniri,
        FabrikaID,
        ParametreKodu,
        ParametreTipiOlcmeGozlem,
        STRING_AGG(CAST(Value AS NVARCHAR(MAX)), '-') WITHIN GROUP (ORDER BY OlusturmaTarihi) AS BirlesikValue,
        STRING_AGG(CAST(NumuneSeriNoSarjNo AS NVARCHAR(MAX)), '-') WITHIN GROUP (ORDER BY OlusturmaTarihi) AS BirlesikNumuneSeriNo,
        AVG(Value) AS AverageValue,
        STDEV(Value) AS StandardDeviation,
        COUNT(Value) AS ValueCount
    FROM 
        ValueData
    WHERE 
        Value IS NOT NULL 
    GROUP BY
        MalzemeAciklamasi,
        UPatamaKodu,
        AltOnaySiniri,
        UstOnaySiniri,
        ParametreKodu,
        ParametreTipiOlcmeGozlem,
        FabrikaID
),
FinalData AS (
    SELECT 
        MalzemeAciklamasi,
        UPatamaKodu,
        AltOnaySiniri,
        UstOnaySiniri,
        FabrikaID,
        ParametreKodu,
        ParametreTipiOlcmeGozlem,
        BirlesikValue,
        BirlesikNumuneSeriNo,
        AverageValue,
        StandardDeviation,
        ValueCount,
        CASE 
            WHEN AverageValue IS NULL OR AverageValue = 0 THEN 0
            ELSE (StandardDeviation / AverageValue)
        END AS DegisimKatsayisi,
        AverageValue * 0.10 AS ErrorLimit,
        CASE 
            WHEN FabrikaID = 1 THEN 135
            WHEN FabrikaID = 2 THEN 133
            WHEN FabrikaID = 3 THEN 175
            WHEN FabrikaID = 4 THEN 216
            ELSE 135
        END AS A,
        (
            SELECT SUM(
                CASE 
                    WHEN nextValue > currentValue THEN (nextValue - currentValue) 
                    ELSE nextValue 
                END)
            FROM (
                SELECT 
                    TRY_CAST(value AS FLOAT) AS currentValue,
                    LEAD(TRY_CAST(value AS FLOAT)) OVER (ORDER BY (SELECT NULL)) AS nextValue
                FROM STRING_SPLIT(CAST(BirlesikNumuneSeriNo AS NVARCHAR(MAX)), '-')
                WHERE TRY_CAST(value AS FLOAT) IS NOT NULL
            ) AS SubQuery
        ) + 
        (
            SELECT TOP 1 TRY_CAST(value AS FLOAT) 
            FROM STRING_SPLIT(CAST(BirlesikNumuneSeriNo AS NVARCHAR(MAX)), '-')
            WHERE TRY_CAST(value AS FLOAT) IS NOT NULL
            ORDER BY (SELECT NULL)
        ) AS N,
        (
            SELECT COUNT(1)
            FROM (
                SELECT 
                    TRY_CAST(value AS FLOAT) AS currentValue,
                    LEAD(TRY_CAST(value AS FLOAT)) OVER (ORDER BY (SELECT NULL)) AS nextValue
                FROM STRING_SPLIT(CAST(BirlesikNumuneSeriNo AS NVARCHAR(MAX)), '-')
                WHERE TRY_CAST(value AS FLOAT) IS NOT NULL
            ) AS SerialNumbers
            WHERE nextValue IS NOT NULL
            AND currentValue IS NOT NULL
            AND (nextValue - currentValue) IS NOT NULL
        ) AS NumuneSayisi,
        (
            SELECT SUM(
                CASE 
                    WHEN nextValue IS NOT NULL THEN 
                        CASE 
                            WHEN nextValue < currentValue THEN nextValue
                            ELSE (nextValue - currentValue) 
                        END
                    ELSE 0
                END
            )
            FROM (
                SELECT 
                    TRY_CAST(value AS FLOAT) AS currentValue,
                    LEAD(TRY_CAST(value AS FLOAT)) OVER (ORDER BY (SELECT NULL)) AS nextValue
                FROM STRING_SPLIT(CAST(BirlesikNumuneSeriNo AS NVARCHAR(MAX)), '-')
                WHERE TRY_CAST(value AS FLOAT) IS NOT NULL
            ) AS NumuneFarklar
        ) AS Sayi
    FROM 
        AggregatedData
)
SELECT 
    MalzemeAciklamasi,
    UPatamaKodu,
    AltOnaySiniri,
    UstOnaySiniri,
    FabrikaID,
    ParametreKodu,
    ParametreTipiOlcmeGozlem,
    BirlesikValue,
    BirlesikNumuneSeriNo,
    AverageValue,
    StandardDeviation,
    DegisimKatsayisi,
    ErrorLimit,
    A,
    CASE 
        WHEN ValueCount > 1 THEN 
            CASE 
                WHEN AverageValue IS NULL OR AverageValue = 0 OR StandardDeviation = 0 THEN 0
                ELSE 
                    ( 
                        CASE 
                            WHEN ((ValueCount - 1) * ErrorLimit * ErrorLimit) + (2.58 * 2.58 * StandardDeviation * StandardDeviation) = 0 THEN 0
                            ELSE 
                                ( 
                                    CASE 
                                        WHEN ValueCount IS NULL OR ValueCount = 0 THEN NULL
                                        ELSE 
                                            CASE 
                                                WHEN A IS NULL OR StandardDeviation IS NULL OR ErrorLimit IS NULL THEN NULL
                                                ELSE 
                                                    A / 
                                                    (
                                                        (N * 2.58 * 2.58 * StandardDeviation * StandardDeviation) / 
                                                        NULLIF((((N - 1) * ErrorLimit * ErrorLimit) + (2.58 * 2.58 * StandardDeviation * StandardDeviation)), 0)
                                                    )
                                            END
                                        END
                                )
                        END
                    )
            END 
        ELSE NULL
    END AS AlinmasiGerNu,
    NumuneSayisi,
    Sayi,
    CASE 
        WHEN ValueCount > 1 THEN 
            CASE 
                WHEN AverageValue IS NULL OR AverageValue = 0 OR StandardDeviation = 0 THEN 0
                ELSE 
                    ( 
                        A / 
                        (
                            (N * 2.58 * 2.58 * StandardDeviation * StandardDeviation) / 
                            NULLIF((((N - 1) * ErrorLimit * ErrorLimit) + (2.58 * 2.58 * StandardDeviation * StandardDeviation)), 0)
                        )
                    ) * NumuneSayisi
            END
        ELSE NULL
    END AS Sonuc,
    CASE 
        WHEN Sayi > 0 THEN
            CASE 
                WHEN ValueCount > 1 THEN 
                    CASE 
                        WHEN AverageValue IS NULL OR AverageValue = 0 OR StandardDeviation = 0 THEN 0
                        ELSE 
                            1 / (((
                                A / 
                                (
                                    (N * 2.58 * 2.58 * StandardDeviation * StandardDeviation) / 
                                    NULLIF((((N - 1) * ErrorLimit * ErrorLimit) + (2.58 * 2.58 * StandardDeviation * StandardDeviation)), 0)
                                )
                            ) * ( NumuneSayisi + 1 )) / Sayi)  
                    END
                ELSE NULL
            END
        ELSE NULL
    END AS Oran  
FROM 
    FinalData
ORDER BY 
    AlinmasiGerNu DESC;

;
            ";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<UrunOranDto>(query);
                return values.ToList();
            }
        }
    }
}
