using Dapper;
using KykKaliteApi.Dtos.HMnumuneDtos;
using KykKaliteApi.Dtos.HMPNvalueDtos;
using KykKaliteApi.Dtos.ParametrelerDtos;
using KykKaliteApi.Dtos.UnumuneDtos;
using KykKaliteApi.Dtos.UPNvalueDtos;
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
            string query = @"
        DECLARE @numuneID INT;

        INSERT INTO Unumune (urunID, SiraNo, UretimTarihi, KontrolSaati, NumuneSeriNoSarjNo, MudahaleVarmi, Aciklama, OnayDurumu, AmirOnayDurumu, OlusturmaTarihi, PersonelSicilNo, Token, Trend)
        VALUES (@urunId, @siraNo, @UretimTarihi, @kontrolSaati, @numuneSeriNoSarjNo, @mudahaleVarmi, @aciklama, @onayDurumu, @amirOnayDurumu, @olusturmaTarihi, @personelSicilNo  ,  @token , @trend);

        SET @numuneID = SCOPE_IDENTITY();

        INSERT INTO UPNvalue (UPAtamaKodu, NumuneID,Versiyon, Value, OlusturmaTarihi, PersonelSicilNo)
        SELECT UpatamaKodu, @numuneID, @versiyon, v.[value], @olusturmaTarihi, @personelSicilNo
        FROM (
            SELECT up.UPAtamaKodu, ROW_NUMBER() OVER (ORDER BY up.OlusturmaTarihi) AS RowNum
            FROM urunler AS u
            INNER JOIN upatamaAktif AS up ON u.urunID = up.urunID 
            WHERE u.urunID = @urunID
        ) AS upWithRowNum
        JOIN (VALUES (@value1, 1), (@value2, 2), (@value3, 3), (@value4, 4), (@value5, 5), (@value6, 6), (@value7, 7), (@value8, 8), (@value9, 9), (@value10, 10), (@value11, 11), (@value12, 12), (@value13, 13),(@value14, 14), (@value15, 15)) AS v([value], RowNum) ON upWithRowNum.RowNum = v.RowNum;
    ";

            var parameters = new DynamicParameters();
            parameters.Add("@urunId", createUnumuneDto.UrunId);
            parameters.Add("@siraNo", createUnumuneDto.SiraNo);
            parameters.Add("@UretimTarihi", createUnumuneDto.UretimTarihi);
            parameters.Add("@kontrolSaati", createUnumuneDto.KontrolSaati);
            parameters.Add("@numuneSeriNoSarjNo", createUnumuneDto.NumuneSeriNoSarjNo);
            parameters.Add("@mudahaleVarmi", createUnumuneDto.MudahaleVarmi);
            parameters.Add("@aciklama", createUnumuneDto.Aciklama);
            parameters.Add("@onayDurumu", createUnumuneDto.OnayDurumu);
            parameters.Add("@amirOnayDurumu", createUnumuneDto.AmirOnayDurumu);
            parameters.Add("@olusturmaTarihi", createUnumuneDto.OlusturmaTarihi);
            parameters.Add("@versiyon", createUnumuneDto.Versiyon);
            parameters.Add("@personelSicilNo", createUnumuneDto.PersonelSicilNo);
            parameters.Add("@value", createUnumuneDto.Value);
            parameters.Add("@value1", createUnumuneDto.Value1);
            parameters.Add("@value2", createUnumuneDto.Value2);
            parameters.Add("@value3", createUnumuneDto.Value3);
            parameters.Add("@value4", createUnumuneDto.Value4);
            parameters.Add("@value5", createUnumuneDto.Value5);
            parameters.Add("@value6", createUnumuneDto.Value6);
            parameters.Add("@value7", createUnumuneDto.Value7);
            parameters.Add("@value8", createUnumuneDto.Value8);
            parameters.Add("@value9", createUnumuneDto.Value9);
            parameters.Add("@value10", createUnumuneDto.Value10);
            parameters.Add("@value11", createUnumuneDto.Value11);
            parameters.Add("@value12", createUnumuneDto.Value12);
            parameters.Add("@value13", createUnumuneDto.Value13);
            parameters.Add("@value14", createUnumuneDto.Value14);
            parameters.Add("@value15", createUnumuneDto.Value15);
            parameters.Add("@token", createUnumuneDto.Token);
            parameters.Add("@trend", createUnumuneDto.Trend);

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

        public async Task<AmirOnayDurumuUnumuneDto> GetDataByToken(string token)
        {
            string query = "SELECT * FROM Unumune WHERE Token = @token;";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<AmirOnayDurumuUnumuneDto>(query, new { token });
            }
        }

        public async Task UpdateAmir(AmirOnayDurumuUnumuneDto amirOnayDurumuUnumuneDto)
        {
            string query = "UPDATE Unumune SET AmirOnayDurumu = 'SartliOnay' WHERE Token = @token;";
            var parameters = new DynamicParameters();
            parameters.Add("@token", amirOnayDurumuUnumuneDto.Token);         
            using (var connectiont = _context.CreateConnection())
            {
                await connectiont.ExecuteAsync(query, parameters);
            }
        }

        public async void UpdateUnumune(UpdateUnumuneDto updateUnumuneDto)
        {
            string query = "Update Unumune Set UrunId=@urunId,SiraNo=@siraNo,UretimTarihi=@uretimTarihi,KontrolSaati=@kontrolSaati,NumuneSeriNoSarjNo=@numuneSeriNoSarjNo,MudahaleVarmi=@mudahaleVarmi,Aciklama=@aciklama,OnayDurumu=@onaydurumu,AmirOnayDurumu=@amirOnayDurumu,OlusturmaTarihi=@olusturmaTarihi,GuncellenmeTarihi = @guncellenmeTarihi ,PersonelSicilNo=@personelSicilNo where NumuneId=@numuneId";
            var parameters = new DynamicParameters();
            parameters.Add("@numuneId", updateUnumuneDto.NumuneId);
            parameters.Add("@urunId", updateUnumuneDto.UrunId);
            parameters.Add("@siraNo", updateUnumuneDto.SiraNo);
            parameters.Add("@uretimTarihi", updateUnumuneDto.UretimTarihi);
            parameters.Add("@kontrolSaati", updateUnumuneDto.KontrolSaati);
            parameters.Add("@numuneSeriNoSarjNo", updateUnumuneDto.NumuneSeriNoSarjNo);
            parameters.Add("@mudahaleVarmi", updateUnumuneDto.MudahaleVarmi);
            parameters.Add("@aciklama", updateUnumuneDto.Aciklama);
            parameters.Add("@onayDurumu", updateUnumuneDto.OnayDurumu);
            parameters.Add("@amirOnayDurumu", updateUnumuneDto.AmirOnayDurumu);
            parameters.Add("@olusturmaTarihi", updateUnumuneDto.OlusturmaTarihi);
            parameters.Add("@guncellenmeTarihi", updateUnumuneDto.GuncellenmeTarihi);
            parameters.Add("@personelSicilNo", updateUnumuneDto.PersonelSicilNo);

            using (var connectiont = _context.CreateConnection())
            {
                await connectiont.ExecuteAsync(query, parameters);
            }
        }
    }
}
