using Dapper;
using KykKaliteApi.Dtos.HammaddelerDtos;
using KykKaliteApi.Dtos.HMnumuneDtos;
using KykKaliteApi.Dtos.UnumuneDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.HMnumuneRepository
{
    public class HMnumuneRepository : IHMnumuneRepository
    {
        private readonly Context _context;
        public HMnumuneRepository(Context context)
        {
            _context = context;
        }

        public async void CreateHMnumune(CreateHMnumuneDto createHMnumuneDto)
        {
            string query = @"
       DECLARE @numuneID INT;

INSERT INTO HMnumune(HammaddeID, SiraNo, Tarihi, Saat, IrsaliyeNo, MalzemeLotSeriNo, KYKBarkodNo, MalzemeUretimTarihi, MalzemeSKT, MalzemeMiktarı, MiktarBirimi,Aciklama,OnayDurumu,AmirOnayDurumu,OlusturmaTarihi,PersonelSicilNo)
VALUES (@hammaddeID, @siraNo, @tarihi, @saat, @irsaliyeNo, @malzemeLotSeriNo,@kYKBarkodNo, @malzemeUretimTarihi, @malzemeSKT, @malzemeMiktarı, @miktarBirimi, @aciklama, @onayDurumu, @amirOnayDurumu, @olusturmaTarihi, @personelSicilNo);

SET @numuneID = SCOPE_IDENTITY();
INSERT INTO HMPNvalue(HMPAtamaKodu, NumuneID,Versiyon, Value, OlusturmaTarihi, PersonelSicilNo)
SELECT HMPAtamaKodu, @numuneID, v.[value],@versiyon, @olusturmaTarihi, @personelSicilNo
FROM (
    SELECT hp.HMPAtamaKodu, ROW_NUMBER() OVER (ORDER BY hp.OlusturmaTarihi) AS RowNum
    FROM Hammaddeler AS h
    INNER JOIN HMPatamaAktif AS hp ON h.HammaddeID = hp.HammaddeID 
    WHERE h.HammaddeID = @hammaddeID
) AS upWithRowNum
        JOIN (VALUES (@value1, 1), (@value2, 2), (@value3, 3), (@value4, 4), (@value5, 5), (@value6, 6), (@value7, 7), (@value8, 8), (@value9, 9), (@value10, 10), (@value11, 11), (@value12, 12), (@value13, 13),(@value14, 14), (@value15, 15)) AS v([value], RowNum) ON upWithRowNum.RowNum = v.RowNum;

    ";
            var parameters = new DynamicParameters();
            parameters.Add("@hammaddeId", createHMnumuneDto.HammaddeId);
            parameters.Add("@siraNo", createHMnumuneDto.SiraNo);
            parameters.Add("@saat", createHMnumuneDto.Saat);
            parameters.Add("@tarihi", createHMnumuneDto.Tarihi);
            parameters.Add("@irsaliyeNo", createHMnumuneDto.IrsaliyeNo);
            parameters.Add("@malzemeLotSeriNo", createHMnumuneDto.MalzemeLotSeriNo);
            parameters.Add("@kykbarkodNo", createHMnumuneDto.KykbarkodNo);
            parameters.Add("@malzemeUretimTarihi", createHMnumuneDto.MalzemeUretimTarihi);
            parameters.Add("@malzemeSkt", createHMnumuneDto.MalzemeSkt);
            parameters.Add("@malzemeMiktarı", createHMnumuneDto.MalzemeMiktarı);
            parameters.Add("@miktarBirimi", createHMnumuneDto.MiktarBirimi);
            parameters.Add("@aciklama", createHMnumuneDto.Aciklama);
            parameters.Add("@onayDurumu", createHMnumuneDto.OnayDurumu);
            parameters.Add("@amirOnayDurumu", createHMnumuneDto.AmirOnayDurumu);
            parameters.Add("@olusturmaTarihi", createHMnumuneDto.OlusturmaTarihi);
            parameters.Add("@versiyon", createHMnumuneDto.Versiyon);
            parameters.Add("@personelSicilNo", createHMnumuneDto.PersonelSicilNo);
            parameters.Add("@value", createHMnumuneDto.Value);
            parameters.Add("@value1", createHMnumuneDto.Value1);
            parameters.Add("@value2", createHMnumuneDto.Value2);
            parameters.Add("@value3", createHMnumuneDto.Value3);
            parameters.Add("@value4", createHMnumuneDto.Value4);
            parameters.Add("@value5", createHMnumuneDto.Value5);
            parameters.Add("@value6", createHMnumuneDto.Value6);
            parameters.Add("@value7", createHMnumuneDto.Value7);
            parameters.Add("@value8", createHMnumuneDto.Value8);
            parameters.Add("@value9", createHMnumuneDto.Value9);
            parameters.Add("@value10", createHMnumuneDto.Value10);
            parameters.Add("@value11", createHMnumuneDto.Value11);
            parameters.Add("@value12", createHMnumuneDto.Value12);
            parameters.Add("@value13", createHMnumuneDto.Value13);
            parameters.Add("@value14", createHMnumuneDto.Value14);
            parameters.Add("@value15", createHMnumuneDto.Value15);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteHMnumune(int id)
        {
            string query = "Delete From HMnumune Where NumuneID=@numuneID";
            var parameters = new DynamicParameters();
            parameters.Add("@numuneID", id);
            using (var connection = _context.CreateConnection())
            {
                int v = await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultHMnumuneDto>> GetAllHMnumuneAsync()
        {
            string query = "Select * From HMnumune";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultHMnumuneDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateHMnumune(UpdateHMnumuneDto updateHMnumuneDto)
        {
            string query = @"UPDATE HMnumune 
                     SET 
                        SiraNo = @siraNo,
                        Tarihi = @tarihi,
                        IrsaliyeNo = @ırsaliyeNo,
                        MalzemeLotSeriNo = @malzemeLotSeriNo,
                        KykbarkodNo = @kykbarkodNo,
                        MalzemeUretimTarihi = @malzemeUretimTarihi,
                        MalzemeSkt = @malzemeSkt,
                        MalzemeMiktarı = @malzemeMiktarı,
                        MiktarBirimi = @miktarBirimi,
                        Aciklama = @aciklama,
                        AmirOnayDurumu = @amirOnayDurumu,
                        EklenmeTarihi = @eklenmeTarihi,
                        PersonelSicilNo = @personelSicilNo,
                        GuncellenmeTarihi = @guncellenmeTarihi
                     WHERE 
                        HammaddeId = @hammaddeId";

            var parameters = new DynamicParameters();
            parameters.Add("@siraNo", updateHMnumuneDto.SiraNo);
            parameters.Add("@tarihi", updateHMnumuneDto.Tarihi);
            parameters.Add("@ırsaliyeNo", updateHMnumuneDto.IrsaliyeNo);
            parameters.Add("@malzemeLotSeriNo", updateHMnumuneDto.MalzemeLotSeriNo);
            parameters.Add("@kykbarkodNo", updateHMnumuneDto.KykbarkodNo);
            parameters.Add("@malzemeUretimTarihi", updateHMnumuneDto.MalzemeUretimTarihi);
            parameters.Add("@malzemeSkt", updateHMnumuneDto.MalzemeSkt);
            parameters.Add("@malzemeMiktarı", updateHMnumuneDto.MalzemeMiktarı);
            parameters.Add("@miktarBirimi", updateHMnumuneDto.MiktarBirimi);
            parameters.Add("@aciklama", updateHMnumuneDto.Aciklama);
            parameters.Add("@amirOnayDurumu", true);
            parameters.Add("@eklenmeTarihi", updateHMnumuneDto.EklenmeTarihi);
            parameters.Add("@personelSicilNo", updateHMnumuneDto.PersonelSicilNo);
            parameters.Add("@hammaddeId", updateHMnumuneDto.HammaddeId);
            parameters.Add("@guncellenmeTarihi", updateHMnumuneDto.GuncellenmeTarihi);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }

   }
