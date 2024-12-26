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
            string query = "insert into HMnumune (SiraNo,Tarihi,IrsaliyeNo,MalzemeLotSeriNo,KykbarkodNo,MalzemeUretimTarihi,MalzemeSkt,MalzemeMiktarı,MiktarBirimi,Aciklama,OnayDurumu,AmirOnayDurumu,EklenmeTarihi,PersonelSicilNo) values (@siraNo,@tarihi,@ırsaliyeNo,@malzemeLotSeriNo,@kykbarkodNo,@malzemeUretimTarihi,@malzemeSkt,@malzemeMiktarı,@miktarBirimi,@aciklama,@onayDurumu,@amirOnayDurumu,@eklenmeTarihi,@personelSicilNo)";
            var parameters = new DynamicParameters();
            parameters.Add("@siraNo", createHMnumuneDto.SiraNo);
            parameters.Add("@tarihi", createHMnumuneDto.Tarihi);
            parameters.Add("@ırsaliyeNo", createHMnumuneDto.IrsaliyeNo);
            parameters.Add("@malzemeLotSeriNo", createHMnumuneDto.MalzemeLotSeriNo);
            parameters.Add("@kykbarkodNo", createHMnumuneDto.KykbarkodNo);
            parameters.Add("@malzemeUretimTarihi", createHMnumuneDto.MalzemeUretimTarihi);
            parameters.Add("@malzemeSkt", createHMnumuneDto.MalzemeSkt);
            parameters.Add("@malzemeMiktarı", createHMnumuneDto.MalzemeMiktarı);
            parameters.Add("@miktarBirimi", createHMnumuneDto.MiktarBirimi);
            parameters.Add("@aciklama", createHMnumuneDto.Aciklama);
            parameters.Add("@amirOnayDurumu", true);
            parameters.Add("@eklenmeTarihi", createHMnumuneDto.EklenmeTarihi);
            parameters.Add("@personelSicilNo", createHMnumuneDto.PersonelSicilNo);
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
                        PersonelSicilNo = @personelSicilNo
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

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }

    }
