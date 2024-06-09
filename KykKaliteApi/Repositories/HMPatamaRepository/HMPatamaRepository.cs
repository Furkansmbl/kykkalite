using Dapper;
using KykKaliteApi.Dtos.HMPatamaDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.HMPatamaRepository
{
    public class HMPatamaRepository : IHMPatamaRepository
    {
        private readonly Context _context;
        public HMPatamaRepository(Context context)
        {
            _context = context;
        }

        public async void CreateHMPatama(CreateHMPatamaDto createHMPatamaDto)
        {
            string query = "insert into HMPatama (ParametreKodu,HammaddeId,HmpatamaKodu,ParametreKritiklikSeviyesi,KontrolDegeriNominal,AltOnaySiniri,UstOnaySiniri,AltSartliKabulSiniri,UstSartliKabulSiniri,CihazId,ReferansDokuman,Aciklama,FabrikaId,PersonelSicilNo,EklenmeTarihi,KullanimDurumu) values (@parametreKodu,@hammaddeId,@hmpatamaKodu,@parametreKritiklikSeviyesi,@kontrolDegeriNominal,@altOnaySiniri,@ustOnaySiniri,@altSartliKabulSiniri,@ustSartliKabulSiniri,@cihazId,@referansDokuman,@aciklama,@fabrikaId,@personelSicilNo,@eklenmeTarihi,@kullanimDurumu)";
            var parameters = new DynamicParameters();
            parameters.Add("@parametreKodu", createHMPatamaDto.ParametreKodu);
            parameters.Add("@hammaddeId", createHMPatamaDto.HammaddeId);
            parameters.Add("@hmpatamaKodu", createHMPatamaDto.HmpatamaKodu);
            parameters.Add("@parametreKritiklikSeviyesi", createHMPatamaDto.ParametreKritiklikSeviyesi);
            parameters.Add("@kontrolDegeriNominal", createHMPatamaDto.KontrolDegeriNominal);
            parameters.Add("@altOnaySiniri", createHMPatamaDto.AltOnaySiniri);
            parameters.Add("@ustOnaySiniri", createHMPatamaDto.UstOnaySiniri);
            parameters.Add("@altSartliKabulSiniri", createHMPatamaDto.AltSartliKabulSiniri);
            parameters.Add("@ustSartliKabulSiniri", createHMPatamaDto.UstSartliKabulSiniri);
            parameters.Add("@cihazId", createHMPatamaDto.CihazId);
            parameters.Add("@referansDokuman", createHMPatamaDto.ReferansDokuman);
            parameters.Add("@fabrikaId", createHMPatamaDto.FabrikaId);
            parameters.Add("@personelSicilNo", createHMPatamaDto.PersonelSicilNo);
            parameters.Add("@eklenmeTarihi", createHMPatamaDto.EklenmeTarihi);
            parameters.Add("@kullanimdurumu", true); ;
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultHMPatamaDto>> GetAllHMPatamaAsync()
        {
            string query = "Select * From HMPatama";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultHMPatamaDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateHMPatama(UpdateHMPatamaDto updateHMPatamaDto)
        {
            string query = @"UPDATE HMPatama 
                     SET 
                        HmpaId = @hmpaId,
                        ParametreKodu = @parametreKodu,
                        HammaddeId = @hammaddeId,
                        HmpatamaKodu = @hmpatamaKodu,
                        ParametreKritiklikSeviyesi = @parametreKritiklikSeviyesi,
                        KontrolDegeriNominal = @kontrolDegeriNominal,
                        AltOnaySiniri = @altOnaySiniri,
                        UstOnaySiniri = @ustOnaySiniri,
                        AltSartliKabulSiniri = @altSartliKabulSiniri,
                        UstSartliKabulSiniri = @ustSartliKabulSiniri,
                        CihazId = @cihazId,
                        ReferansDokuman = @referansDokuman,
                        FabrikaId = @fabrikaId,
                        PersonelSicilNo = @personelSicilNo,
                        EklenmeTarihi = @eklenmeTarihi,
                        KullanimDurumu = @kullanimDurumu
                     WHERE 
                        HmpaId = @hmpaId";

            var parameters = new DynamicParameters();
            parameters.Add("@hmpaId", updateHMPatamaDto.HmpaId);
            parameters.Add("@parametreKodu", updateHMPatamaDto.ParametreKodu);
            parameters.Add("@hammaddeId", updateHMPatamaDto.HammaddeId);
            parameters.Add("@hmpatamaKodu", updateHMPatamaDto.HmpatamaKodu);
            parameters.Add("@ParametreKritiklikSeviyesi", true);
            parameters.Add("@kontrolDegeriNominal", updateHMPatamaDto.KontrolDegeriNominal);
            parameters.Add("@altOnaySiniri", updateHMPatamaDto.AltOnaySiniri);
            parameters.Add("@ustOnaySiniri", updateHMPatamaDto.UstOnaySiniri);
            parameters.Add("@altSartliKabulSiniri", updateHMPatamaDto.AltSartliKabulSiniri);
            parameters.Add("@ustSartliKabulSiniri", updateHMPatamaDto.UstSartliKabulSiniri);
            parameters.Add("@cihazId", updateHMPatamaDto.CihazId);
            parameters.Add("@referansDokuman", updateHMPatamaDto.ReferansDokuman);
            parameters.Add("@fabrikaId", updateHMPatamaDto.FabrikaId);
            parameters.Add("@personelSicilNo", updateHMPatamaDto.PersonelSicilNo);
            parameters.Add("@eklenmeTarihi", updateHMPatamaDto.EklenmeTarihi);
            parameters.Add("@kullanimDurumu", true);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
