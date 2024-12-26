using Dapper;
using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.HammaddeGruplariDtos;
using KykKaliteApi.Dtos.HammaddelerDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.HammaddelerRepository
{
    public class HammaddelerRepository : IHammaddelerRepository
    {
        private readonly Context _context;
        public HammaddelerRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultHammaddelerDto>> GetAllHammaddelerAsync()
        {
            string query = "Select * From Hammaddeler";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultHammaddelerDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateHammadde(UpdateHammaddelerDto updateHammaddelerDto)
        {
            string query = "Update Hammaddeler Set HammaddeId=@hammaddeId,HammaddeGrupID=@hammaddeGrupId,MalzemeKodu=@malzemeKodu,MalzemeAciklamasi=@malzemeAciklamasi,PersonelSicilNo=@personelSicilNo,EklenmeGuncellenmeTarihi=@eklenmeGüncellenmeTarihi,KullanımDurumu=@kullanımDurumu where HammaddeId=@hammaddeId";
            var parameters = new DynamicParameters();
            parameters.Add("@hammaddeId", updateHammaddelerDto.HammaddeId);
            parameters.Add("@hammaddeGrupId", updateHammaddelerDto.HammaddeGrupId);
            parameters.Add("@malzemeKodu",updateHammaddelerDto.MalzemeKodu);
            parameters.Add("@malzemeAciklamasi", updateHammaddelerDto.MalzemeAciklamasi);
            parameters.Add("@personelSicilNo", updateHammaddelerDto.PersonelSicilNo);
            parameters.Add("@eklenmeGüncellenmeTarihi", updateHammaddelerDto.EklenmeGuncellenmeTarihi);
            parameters.Add("@kullanımdurumu", true);

            using (var connectiont = _context.CreateConnection())
            {
                await connectiont.ExecuteAsync(query, parameters);
            }
        }
    }
}
