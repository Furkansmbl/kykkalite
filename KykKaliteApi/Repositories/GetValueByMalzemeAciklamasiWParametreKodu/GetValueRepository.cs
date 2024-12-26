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

        public async Task<List<ResultGetValueDto>> GetKontrolParametresi()
        {
         
                string query = @"
   SELECT DISTINCT ur.MalzemeAciklamasi , par.kontrolparametresi
FROM urunler ur
JOIN upatamaAktif upa ON ur.UrunID = upa.UrunID
JOIN parametreler par ON upa.ParametreKodu = par.ParametreKodu";

                using (var connection = _context.CreateConnection())
                {
                    var values = await connection.QueryAsync<ResultGetValueDto>(query);
                    return values.ToList(); ;
                }
            
        }
        public async Task<List<ResultGetValueDto>> GetKontrolParametresiHammadde()
        {

            string query = @"
    SELECT DISTINCT par.kontrolparametresi , ur.MalzemeAciklamasi, TH.UNVANI
FROM Hammaddeler ur
JOIN HMPatamaAktif upa ON  ur.HammaddeID = upa.HammaddeID
JOIN TedarikciHammadde th on ur.MalzemeAciklamasi = TH.MALZADI
JOIN parametreler par ON upa.ParametreKodu = par.ParametreKodu";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultGetValueDto>(query);
                return values.ToList(); ;
            }

        }
        public async Task<List<ResultGetValueDto>> GetValueByMalzemeAciklamasiWParametreKoduAsync(string malzemeaciklamasi, string kontrolparametresi, string baslangicTarihi, string bitisTarihi, int fabrikaId)
        {
            string query = @"
SELECT TOP 49 
    upn.Value,
    LEFT(upn.OlusturmaTarihi, 10) AS OlusturmaTarihi, 
    upa.AltOnaySiniri,
    upa.AltSartliKabulSiniri,
    upa.UstSartliKabulSiniri,
    upa.UstOnaySiniri,
    par.ParametreTipiOlcmeGozlem,
    ur.malzemeaciklamasi,
    par.KontrolParametresi
FROM 
    upnvalue upn
JOIN 
    upatamaAktif upa ON upn.upatamakodu = upa.upatamakodu
JOIN 
    urunler ur ON upa.UrunID = ur.UrunID
JOIN 
    parametreler par ON upa.ParametreKodu = par.ParametreKodu
WHERE 
    ur.malzemeaciklamasi = @malzemeaciklamasi
    AND par.kontrolparametresi = @kontrolparametresi
      AND LEFT(upn.OlusturmaTarihi, 10) 
        BETWEEN @baslangicTarihi AND @bitisTarihi 
    AND upa.FabrikaID = @fabrikaId
ORDER BY 
    LEFT(upn.OlusturmaTarihi, 10) DESC ";

            var parameters = new { malzemeaciklamasi, kontrolparametresi, baslangicTarihi, bitisTarihi, fabrikaId };
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultGetValueDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task<List<ResultGetValueDto>> GetValueByMalzemeAciklamasiWParametreKoduHammaddeAsync(string malzemeaciklamasi, string kontrolparametresi, string baslangicTarihi, string bitisTarihi, string UNVANI)
        {
            string query = @"
SELECT TOP 49 
    hmpn.Value,
    LEFT(hmpn.OlusturmaTarihi, 10) AS OlusturmaTarihi, 
    hma.AltOnaySiniri,
    hma.AltSartliKabulSiniri,
    hma.UstSartliKabulSiniri,
    hma.UstOnaySiniri,
    par.ParametreTipiOlcmeGozlem,
    hm.malzemeaciklamasi,
    par.KontrolParametresi,
	TH.UNVANI
FROM 
    HMPNvalue hmpn
	JOIN HMnumune hmn ON hmpn.NumuneID = hmn.NumuneID
	JOIN TedarikciHammadde th ON hmn.THMID = th.THMID
	JOIN HMPatamaAktif hma ON hmpn.HMPAtamaKodu = hma.HMPAtamaKodu
	JOIN Hammaddeler hm ON hma.HammaddeID = hm.HammaddeID
	JOIN parametreler par ON hma.ParametreKodu = par.ParametreKodu
WHERE 
    hm.malzemeaciklamasi = @malzemeaciklamasi
    AND par.kontrolparametresi = @kontrolparametresi
	AND th.UNVANI = @UNVANI
    AND LEFT(hmpn.OlusturmaTarihi, 10) 
        BETWEEN @baslangicTarihi AND @bitisTarihi
ORDER BY 
    LEFT(hmpn.OlusturmaTarihi, 10) DESC;
";

            var parameters = new { malzemeaciklamasi, kontrolparametresi, baslangicTarihi, bitisTarihi, UNVANI };
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultGetValueDto>(query, parameters);
                return values.ToList();
            }
        }
    }
}
