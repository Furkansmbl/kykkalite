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

        public async Task<List<ResultGetValueDto>> GetValueByMalzemeAciklamasiWParametreKoduAsync(string malzemeaciklamasi, string kontrolparametresi, string baslangicTarihi, string bitisTarihi)
        {
            string query = @"
SELECT TOP 49 
    upn.Value,
    upn.OlusturmaTarihi,
    upa.AltOnaySiniri,
    upa.AltSartliKabulSiniri,
    upa.UstSartliKabulSiniri,
    upa.UstOnaySiniri,
    par.ParametreTipiOlcmeGozlem
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
    AND upn.OlusturmaTarihi BETWEEN @baslangicTarihi AND @bitisTarihi
ORDER BY 
    upn.OlusturmaTarihi DESC;";

            var parameters = new { malzemeaciklamasi, kontrolparametresi, baslangicTarihi, bitisTarihi };
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultGetValueDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task<List<ResultGetValueDto>> GetValueByMalzemeAciklamasiWParametreKoduHammaddeAsync(string malzemeaciklamasi, string kontrolparametresi, string baslangicTarihi, string bitisTarihi)
        {
            string query = @"
SELECT TOP 49 
    hpn.Value,
    hpn.OlusturmaTarihi,
	hpa.AltOnaySiniri,
	hpa.AltSartliKabulSiniri,
    hpa.UstSartliKabulSiniri,
    hpa.UstOnaySiniri,
    par.ParametreTipiOlcmeGozlem
FROM 
    HMPNvalue hpn
JOIN 
    HMPatamaAktif hpa ON hpn.HMPAtamaKodu = hpa.HMPAtamaKodu
JOIN 
    Hammaddeler h ON hpa.HammaddeID = h.HammaddeID
JOIN 
    parametreler par ON hpa.ParametreKodu = par.ParametreKodu
WHERE 
	
    h.malzemeaciklamasi = @malzemeaciklamasi
    AND par.kontrolparametresi = @kontrolparametresi
    AND hpn.OlusturmaTarihi BETWEEN @baslangicTarihi AND @bitisTarihi
ORDER BY 
    hpn.OlusturmaTarihi DESC;";

            var parameters = new { malzemeaciklamasi, kontrolparametresi, baslangicTarihi, bitisTarihi };
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultGetValueDto>(query, parameters);
                return values.ToList();
            }
        }
    }
}
