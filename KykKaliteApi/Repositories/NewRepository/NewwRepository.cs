using Dapper;
using KykKaliteApi.Dtos.NewDtos;
using KykKaliteApi.Dtos.ParametrelerDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.NewRepository
{
    public class NewwRepository : INewwRepository
    {
        private readonly Context _context;
        public NewwRepository(Context context)
        {
            _context = context;
        }

       public async Task<List<ResultNewwDtp>> GetAllNewlerAsync()
{
    string query = @"
        SELECT 
    U.UrunID, 
    U.MalzemeKodu, 
    U.MalzemeAciklamasi, 
    UP.UPAtamaKodu, 
    UP.ParametreKodu,
    UP.UstOnaySiniri,
    UP.AltOnaySiniri,
    UP.AltSartliKabulSiniri,
    UP.UstSartliKabulSiniri,
    UP.ParametreKritiklikSeviyesi,
    UP.DeneyReferansDokuman,
    UP.Aciklama,
    UP.EklenmeTarihi,
    UP.KullanımDurumu,
    UP.PersonelSicilNo,
    UP.FabrikaID,
    UP.OrneklemSikligi,
    UP.OrneklemSiklikBirim,
    UP.KontrolDegeriNominal,
    UP.UpaId,
    UP.CihazID,
    N.NumuneID,
    V.Value
    FROM 
        Urunler U WITH(NOLOCK) 
    JOIN 
        UPatama UP WITH(NOLOCK) ON U.UrunID = UP.UrunID
    JOIN 
        Unumune N WITH(NOLOCK) ON U.UrunID = N.UrunID
    JOIN
        UPNvalue V WITH(NOLOCK) ON V.UPAtamaKodu = UP.UPAtamaKodu";

    using (var connection = _context.CreateConnection())
    {
        var values = await connection.QueryAsync<ResultNewwDtp>(query);
        return values.ToList(); 
    }
}
    }
}
