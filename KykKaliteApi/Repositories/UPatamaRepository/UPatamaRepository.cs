using Dapper;
using KykKaliteApi.Dtos.HMnumuneDtos;
using KykKaliteApi.Dtos.HMPatamaDtos;
using KykKaliteApi.Dtos.UnumuneDtos;
using KykKaliteApi.Dtos.UPatamaDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.UPatamaRepository
{
    public class UPatamaRepository : IUPatamaRepository
    {
        private readonly Context _context;
        public UPatamaRepository(Context context)
        {
            _context = context;
        }

        public async void CreateUPatama(CreateUPatamaDto createUPatamaDto)
        {
            string query = "insert into UPatama (ParametreKodu,UrunId,UpatamaKodu,ParametreKritiklikSeviyesi,KontrolDegeriNominal,AltOnaySiniri,UstOnaySiniri,AltSartliKabulSiniri,UstSartliKabulSiniri,CihazId,ReferansDokuman,Aciklama,OrneklemSikligi,OrneklemSiklikBirim,FabrikaId,PersonelSicilNo,EklenmeTarihi,KullanımDurumu) values (@parametreKodu,@urunId,@upatamaKodu,@parametreKritiklikSeviyesi,@kontrolDegeriNominal,@altOnaySiniri,@ustOnaySiniri,@altSartliKabulSiniri,@ustSartliKabulSiniri,@cihazId,@referansDokuman,@aciklama,@orneklemSikligi,@orneklemSiklikBirim,@fabrikaId,@personelSicilNo,@eklenmeTarihi,@kullanımDurumu)";
            var parameters = new DynamicParameters();
            parameters.Add("@parametreKodu", createUPatamaDto.ParametreKodu);
            parameters.Add("@urunId", createUPatamaDto.UrunId);
            parameters.Add("@upatamaKodu", createUPatamaDto.UpatamaKodu);
            parameters.Add("@parametreKritiklikSeviyesi", true);
            parameters.Add("@kontrolDegeriNominal", createUPatamaDto.KontrolDegeriNominal);
            parameters.Add("@altOnaySiniri", createUPatamaDto.AltOnaySiniri);
            parameters.Add("@ustOnaySiniri", createUPatamaDto.UstOnaySiniri);
            parameters.Add("@altSartliKabulSiniri", createUPatamaDto.AltSartliKabulSiniri);
            parameters.Add("@ustSartliKabulSiniri", createUPatamaDto.UstSartliKabulSiniri);
            parameters.Add("@cihazId", createUPatamaDto.CihazId);
            parameters.Add("@referansDokuman", createUPatamaDto.ReferansDokuman);
            parameters.Add("@aciklama", createUPatamaDto.Aciklama);
            parameters.Add("@orneklemSikligi", createUPatamaDto.OrneklemSikligi);
            parameters.Add("@OrneklemSiklikBirim", createUPatamaDto.OrneklemSiklikBirim);
            parameters.Add("@fabrikaId", createUPatamaDto.FabrikaId);
            parameters.Add("@personelSicilNo", createUPatamaDto.PersonelSicilNo);
            parameters.Add("@eklenmeTarihi", createUPatamaDto.OrneklemSiklikBirim);
            parameters.Add("@OrneklemSiklikBirim", createUPatamaDto.OrneklemSiklikBirim);
            parameters.Add("@kullanımDurumu", true);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteUPatama(int id)
        {
            string query = "Delete From UPatama Where UpaId=@upaId";
            var parameters = new DynamicParameters();
            parameters.Add("upaId", id);
            using (var connection = _context.CreateConnection())
            {
                int v = await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultUPatamaDto>> GetAllUPatamaAsync()
        {
            string query = "Select * From UPatama";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultUPatamaDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateUPatama(UpdateUPatamaDto updateUPatamaDto)
        {
            string query = "INSERT INTO UPatama (UpaId, ParametreKodu, UrunId, UpatamaKodu, ParametreKritiklikSeviyesi, KontrolDegeriNominal, AltOnaySiniri, UstOnaySiniri, AltSartliKabulSiniri, UstSartliKabulSiniri, CihazId, ReferansDokuman, Aciklama, OrneklemSikligi, OrneklemSiklikBirim, FabrikaId, PersonelSicilNo, EklenmeTarihi, KullanımDurumu) VALUES (@upaId, @parametreKodu, @urunId, @upatamaKodu, @parametreKritiklikSeviyesi, @kontrolDegeriNominal, @altOnaySiniri, @ustOnaySiniri, @altSartliKabulSiniri, @ustSartliKabulSiniri, @cihazId, @referansDokuman, @aciklama, @orneklemSikligi, @orneklemSiklikBirim, @fabrikaId, @personelSicilNo, @eklenmeTarihi, @kullanımDurumu)";
            var parameters = new DynamicParameters();
            parameters.Add("@upaId", updateUPatamaDto.UpaId);
            parameters.Add("@parametreKodu", updateUPatamaDto.ParametreKodu);
            parameters.Add("@urunId", updateUPatamaDto.UrunId);
            parameters.Add("@upatamaKodu", updateUPatamaDto.UpatamaKodu);
            parameters.Add("@parametreKritiklikSeviyesi", true);
            parameters.Add("@kontrolDegeriNominal", updateUPatamaDto.KontrolDegeriNominal);
            parameters.Add("@altOnaySiniri", updateUPatamaDto.AltOnaySiniri);
            parameters.Add("@ustOnaySiniri", updateUPatamaDto.UstOnaySiniri);
            parameters.Add("@altSartliKabulSiniri", updateUPatamaDto.AltSartliKabulSiniri);
            parameters.Add("@ustSartliKabulSiniri", updateUPatamaDto.UstSartliKabulSiniri);
            parameters.Add("@cihazId", updateUPatamaDto.CihazId);
            parameters.Add("@referansDokuman", updateUPatamaDto.ReferansDokuman);
            parameters.Add("@aciklama", updateUPatamaDto.Aciklama);
            parameters.Add("@orneklemSikligi", updateUPatamaDto.OrneklemSikligi);
            parameters.Add("@orneklemSiklikBirim", updateUPatamaDto.OrneklemSiklikBirim);
            parameters.Add("@fabrikaId", updateUPatamaDto.FabrikaId);
            parameters.Add("@personelSicilNo", updateUPatamaDto.PersonelSicilNo);
            parameters.Add("@eklenmeTarihi", updateUPatamaDto.EklenmeTarihi); // Değişiklik burada
            parameters.Add("@kullanımDurumu", true);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
