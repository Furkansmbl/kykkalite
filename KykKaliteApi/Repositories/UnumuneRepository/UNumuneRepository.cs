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

        INSERT INTO Unumune (urunID, SiraNo, UretimTarihi, KontrolSaati, NumuneSeriNoSarjNo, MudahaleVarmi, Aciklama, OnayDurumu, AmirOnayDurumu, EklenmeTarihi, PersonelSicilNo,Token)
        VALUES (@urunId, @siraNo, @UretimTarihi, @kontrolSaati, @numuneSeriNoSarjNo, @mudahaleVarmi, @aciklama, @onayDurumu, @amirOnayDurumu, @eklenmeTarihi, @personelSicilNo , @token);

        SET @numuneID = SCOPE_IDENTITY();

        INSERT INTO UPNvalue (UPAtamaKodu, NumuneID, Value, EklenmeTarihi, PersonelSicilNo)
        SELECT UpatamaKodu, @numuneID, v.[value], @eklenmeTarihi, @personelSicilNo
        FROM (
            SELECT up.UPAtamaKodu, ROW_NUMBER() OVER (ORDER BY up.EklenmeTarihi) AS RowNum
            FROM urunler AS u
            INNER JOIN upatama AS up ON u.urunID = up.urunID AND KullanımDurumu = 1
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
            parameters.Add("@eklenmeTarihi", createUnumuneDto.EklenmeTarihi);
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
            string query = "UPDATE Unumune SET AmirOnayDurumu = 1 WHERE Token = @token;";
            var parameters = new DynamicParameters();
            parameters.Add("@token", amirOnayDurumuUnumuneDto.Token);         
            using (var connectiont = _context.CreateConnection())
            {
                await connectiont.ExecuteAsync(query, parameters);
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
