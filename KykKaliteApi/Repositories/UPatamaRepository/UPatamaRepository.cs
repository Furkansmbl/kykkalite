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
            string query = "insert into UPatamaAktif (ParametreKodu,UrunId,UpatamaKodu,ParametreKritiklikSeviyesi,KontrolDegeriNominal," +
                "AltOnaySiniri,UstOnaySiniri,AltSartliKabulSiniri,UstSartliKabulSiniri,CihazId,ReferansDokuman,Aciklama,OrneklemSikligi," +
                "OrneklemSiklikBirim,FabrikaId,PersonelSicilNo,OlusturmaTarihi,ParametreId,Versiyon,Tolerans,ParametreYonu,KullanimDurumu) values " +
                "(@parametreKodu,@urunId,@upatamaKodu,@parametreKritiklikSeviyesi,@kontrolDegeriNominal,@altOnaySiniri,@ustOnaySiniri,@altSartliKabulSiniri," +
                "@ustSartliKabulSiniri,@cihazId,@referansDokuman,@aciklama,@orneklemSikligi,@orneklemSiklikBirim,@fabrikaId,@personelSicilNo,@olusturmaTarihi,@parametreId,@versiyon,@tolerans,@parametreYonu,@kullanimDurumu)";
            var parameters = new DynamicParameters();
            parameters.Add("@parametreKodu", createUPatamaDto.ParametreKodu);
            parameters.Add("@urunId", createUPatamaDto.UrunId);
            parameters.Add("@parametreId", createUPatamaDto.ParametreId);
            parameters.Add("@fabrikaId", createUPatamaDto.FabrikaId);
            parameters.Add("@upatamaKodu", createUPatamaDto.UpatamaKodu);
            parameters.Add("@versiyon", createUPatamaDto.Versiyon);
            parameters.Add("@parametreKritiklikSeviyesi", createUPatamaDto.ParametreKritiklikSeviyesi);
            parameters.Add("@tolerans", createUPatamaDto.Tolerans);
            parameters.Add("@parametreYonu", createUPatamaDto.ParametreYonu);
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
            parameters.Add("@personelSicilNo", createUPatamaDto.PersonelSicilNo);
            parameters.Add("@olusturmaTarihi", createUPatamaDto.OlusturmaTarihi) ;
            parameters.Add("@kullanimDurumu", createUPatamaDto.KullanimDurumu);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void CreateUPatamaPasif(CreateUpAtamaPasifDto createUpAtamaPasifDto)
        {
            string query = "insert into UPatamaPasif (ParametreKodu,UrunId,UpatamaKodu,ParametreKritiklikSeviyesi,KontrolDegeriNominal,AltOnaySiniri,UstOnaySiniri,AltSartliKabulSiniri,UstSartliKabulSiniri,CihazId,ReferansDokuman,Aciklama,OrneklemSikligi,OrneklemSiklikBirim,FabrikaId,PersonelSicilNo,OlusturmaTarihi,ParametreId,Versiyon,Tolerans,ParametreYonu,PasifeAlanPersonelSicilNo,PasifeAlinmaTarihi) values (@parametreKodu,@urunId,@upatamaKodu,@parametreKritiklikSeviyesi,@kontrolDegeriNominal,@altOnaySiniri,@ustOnaySiniri,@altSartliKabulSiniri,@ustSartliKabulSiniri,@cihazId,@referansDokuman,@aciklama,@orneklemSikligi,@orneklemSiklikBirim,@fabrikaId,@personelSicilNo,@olusturmaTarihi,@parametreId,@versiyon,@tolerans,@parametreYonu,@pasifeAlanPersonelSicilNo,@pasifeAlinmaTarihi)";
            var parameters = new DynamicParameters();
            parameters.Add("@parametreKodu", createUpAtamaPasifDto.ParametreKodu);
            parameters.Add("@urunId", createUpAtamaPasifDto.UrunId);
            parameters.Add("@parametreId", createUpAtamaPasifDto.ParametreId);
            parameters.Add("@fabrikaId", createUpAtamaPasifDto.FabrikaId);
            parameters.Add("@upatamaKodu", createUpAtamaPasifDto.UpatamaKodu);
            parameters.Add("@versiyon", createUpAtamaPasifDto.Versiyon);
            parameters.Add("@parametreKritiklikSeviyesi", createUpAtamaPasifDto.ParametreKritiklikSeviyesi);
            parameters.Add("@tolerans", createUpAtamaPasifDto.Tolerans);
            parameters.Add("@parametreYonu", createUpAtamaPasifDto.ParametreYonu);
            parameters.Add("@kontrolDegeriNominal", createUpAtamaPasifDto.KontrolDegeriNominal);
            parameters.Add("@altOnaySiniri", createUpAtamaPasifDto.AltOnaySiniri);
            parameters.Add("@ustOnaySiniri", createUpAtamaPasifDto.UstOnaySiniri);
            parameters.Add("@altSartliKabulSiniri", createUpAtamaPasifDto.AltSartliKabulSiniri);
            parameters.Add("@ustSartliKabulSiniri", createUpAtamaPasifDto.UstSartliKabulSiniri);
            parameters.Add("@cihazId", createUpAtamaPasifDto.CihazId);
            parameters.Add("@referansDokuman", createUpAtamaPasifDto.ReferansDokuman);
            parameters.Add("@aciklama", createUpAtamaPasifDto.Aciklama);
            parameters.Add("@orneklemSikligi", createUpAtamaPasifDto.OrneklemSikligi);
            parameters.Add("@OrneklemSiklikBirim", createUpAtamaPasifDto.OrneklemSiklikBirim);
            parameters.Add("@personelSicilNo", createUpAtamaPasifDto.PersonelSicilNo);
            parameters.Add("@olusturmaTarihi", createUpAtamaPasifDto.OlusturmaTarihi);
            parameters.Add("@pasifeAlanPersonelSicilNo", createUpAtamaPasifDto.PasifeAlanPersonelSicilNo);
            parameters.Add("@pasifeAlinmaTarihi", createUpAtamaPasifDto.PasifeAlinmaTarihi);
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
            string query = @"SELECT UA.*, 
       P.KontrolParametresi, 
       U.malzemeaciklamasi 
FROM UPatamaAktif UA 
JOIN Parametreler P 
    ON CAST(PARSENAME(REPLACE(UA.upAtamaKodu, '+', '.'), 2) AS INT) = P.ParametreId
JOIN Urunler U 
    ON CAST(PARSENAME(REPLACE(UA.upAtamaKodu, '+', '.'), 3) AS INT) = U.UrunId;

";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultUPatamaDto>(query);
                return values.ToList();
            }
        }


        public async void UpdateUPatama(UpdateUPatamaDto updateUPatamaDto)
        {
            string query = "UPDATE UPatamaAktif SET ParametreKodu = @parametreKodu , UrunId = @urunId, ParametreId = @parametreId , FabrikaId = @fabrikaId , UpAtamaKodu = @upAtamaKodu  , Versiyon = @versiyon , ParametreKritiklikSeviyesi = @parametreKritiklikSeviyesi , " +
                " Tolerans = @tolerans, ParametreYonu = @parametreYonu ,  KontrolDegeriNominal = @kontrolDegeriNominal, AltOnaySiniri = @altOnaySiniri , UstOnaySiniri = @ustOnaySiniri , AltSartliKabulSiniri = @altSartliKabulSiniri ,UstSartliKabulSiniri =@ustSartliKabulSiniri," +
                " CihazId =@cihazId, ReferansDokuman = @referansDokuman , Aciklama = @aciklama , OrneklemSikligi = @orneklemSikligi , OrneklemSiklikBirim =@OrneklemSiklikBirim ,PersonelSicilNo = @personelSicilNo, OlusturmaTarihi = @olusturmaTarihi, KullanimDurumu = @kullanimDurumu WHERE UpaId = @upaId ";
            var parameters = new DynamicParameters();
            parameters.Add("@upaId", updateUPatamaDto.UpaId);
            parameters.Add("@parametreKodu", updateUPatamaDto.ParametreKodu);
            parameters.Add("@urunId", updateUPatamaDto.UrunId);
            parameters.Add("@parametreId", updateUPatamaDto.ParametreId);
            parameters.Add("@fabrikaId", updateUPatamaDto.FabrikaId);
            parameters.Add("@upatamaKodu", updateUPatamaDto.UpatamaKodu);
            parameters.Add("@versiyon", updateUPatamaDto.Versiyon);
            parameters.Add("@parametreKritiklikSeviyesi", updateUPatamaDto.ParametreKritiklikSeviyesi);
            parameters.Add("@tolerans", updateUPatamaDto.Tolerans);
            parameters.Add("@parametreYonu", updateUPatamaDto.ParametreYonu);
            parameters.Add("@kontrolDegeriNominal", updateUPatamaDto.KontrolDegeriNominal);
            parameters.Add("@altOnaySiniri", updateUPatamaDto.AltOnaySiniri);
            parameters.Add("@ustOnaySiniri", updateUPatamaDto.UstOnaySiniri);
            parameters.Add("@altSartliKabulSiniri", updateUPatamaDto.AltSartliKabulSiniri);
            parameters.Add("@ustSartliKabulSiniri", updateUPatamaDto.UstSartliKabulSiniri);
            parameters.Add("@cihazId", updateUPatamaDto.CihazId);
            parameters.Add("@referansDokuman", updateUPatamaDto.ReferansDokuman);
            parameters.Add("@aciklama", updateUPatamaDto.Aciklama);
            parameters.Add("@orneklemSikligi", updateUPatamaDto.OrneklemSikligi);
            parameters.Add("@OrneklemSiklikBirim", updateUPatamaDto.OrneklemSiklikBirim);
            parameters.Add("@personelSicilNo", updateUPatamaDto.PersonelSicilNo);
            parameters.Add("@olusturmaTarihi", updateUPatamaDto.OlusturmaTarihi);
            parameters.Add("@kullanimDurumu", updateUPatamaDto.KullanimDurumu);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
