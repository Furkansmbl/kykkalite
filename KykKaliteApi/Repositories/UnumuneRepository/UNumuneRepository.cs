using Dapper;
using KykKaliteApi.Dtos.HMnumuneDtos;
using KykKaliteApi.Dtos.HMPNvalueDtos;
using KykKaliteApi.Dtos.ParametrelerDtos;
using KykKaliteApi.Dtos.UnumuneDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.UnumuneRepository
{
    public class UNumuneRepository : IUnumuneRepository
    {
        private readonly Context _context;
        public UNumuneRepository(Context context)
        {
            _context = context;
        }

        public async void CreateUnumune(CreateUnumuneDto createUnumuneDto)
        {
          
            string query = "insert into Unumune (UrunId,SiraNo,UretimTarihi,KontrolSaati,NumuneSeriNoSarjNo,MudahaleVarmi,Aciklama,OnayDurumu,AmirOnayDurumu,EklenmeTarihi,PersonelSicilNo) values (@urunId,@siraNo,@UretimTarihi,@kontrolSaati,@numuneSeriNoSarjNo,@mudahaleVarmi,@aciklama,@onayDurumu,@amirOnayDurumu,@eklenmeTarihi,@personelSicilNo)";
            var parameters = new DynamicParameters();
            parameters.Add("@urunId", createUnumuneDto.UrunId);
            parameters.Add("@siraNo", createUnumuneDto.SiraNo);
            parameters.Add("@uretimTarihi", createUnumuneDto.UretimTarihi);
            parameters.Add("@kontrolSaati", createUnumuneDto.KontrolSaati);
            parameters.Add("@numuneSeriNoSarjNo", createUnumuneDto.NumuneSeriNoSarjNo);
            parameters.Add("@mudahaleVarmi", true);
            parameters.Add("@aciklama", createUnumuneDto.Aciklama);
            parameters.Add("@onayDurumu", true);
            parameters.Add("@amirOnayDurumu", createUnumuneDto.AmirOnayDurumu);
            parameters.Add("@eklenmeTarihi", createUnumuneDto.EklenmeTarihi);
            parameters.Add("@personelSicilNo", createUnumuneDto.PersonelSicilNo);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteUnumune(int id)
        {
            string query = "Delete From Unumune Where NumuneId=@numuneId";
            var parameters = new DynamicParameters();
            parameters.Add("numuneId", id);
            using (var connection = _context.CreateConnection())
            {
                int v = await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultUnumuneDto>> GetAllUnumuneAsync()
        {
            string query = "Select * From Unumune";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultUnumuneDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateUnumune(UpdateUnumuneDto updateUnumuneDto)
        {
            string query = "Update HMPNvalue Set UrunId=@urunId,SiraNo=@siraNo,UretimTarihi=@uretimTarihi,NumuneSeriNoSarjNo=@numuneSeriNoSarjNo,MudahaleVarmi=@mudahaleVarmi,Aciklama=@aciklama,OnayDurumu=@onaydurumu,AmirOnayDurumu=@amirOnayDurumu,EklenmeTarihi=@eklenmeTarihi,PersonelSicilNo=@personelSicilNo where HMPNValueId=@hMPNValueId";
            var parameters = new DynamicParameters();
            parameters.Add("@numuneId", updateUnumuneDto.NumuneId);
            parameters.Add("@urunId", updateUnumuneDto.UrunId);
            parameters.Add("@siraNo", updateUnumuneDto.SiraNo);
            parameters.Add("@uretimTarihi", updateUnumuneDto.UretimTarihi);
            parameters.Add("@numuneSeriNoSarjNo", updateUnumuneDto.NumuneSeriNoSarjNo);
            parameters.Add("@mudahaleVarmi", true);
            parameters.Add("@aciklama", updateUnumuneDto.Aciklama);
            parameters.Add("@onayDurumu", true);
            parameters.Add("@amirOnayDurumu", updateUnumuneDto.AmirOnayDurumu);
            parameters.Add("@eklenmeTarihi", updateUnumuneDto.EklenmeTarihi);
            parameters.Add("@personelSicilNo", updateUnumuneDto.PersonelSicilNo);

            using (var connectiont = _context.CreateConnection())
            {
                await connectiont.ExecuteAsync(query, parameters);
            }
        }
    }
}
