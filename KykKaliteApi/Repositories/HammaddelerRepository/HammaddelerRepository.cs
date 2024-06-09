using Dapper;
using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.HammaddeGruplariDtos;
using KykKaliteApi.Dtos.HammaddelerDtos;
using KykKaliteApi.Dtos.UrunlerDtos;
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

        public async void CreateHammaddeler(CreateHammaddelerDto createHammaddelerDto)
        {
            string query = "insert into Hammaddeler (HammaddeGrupId,MalzemeKodu,MalzemeAciklamasi,PersonelSicilNo,EklenmeGuncellenmeTarihi,KullanımDurumu) values (@hammaddeGrupId,@malzemeKodu,@malzemeAciklamasi,@personelSicilNo,@eklenmeGuncellenmeTarihi,@kullanımDurumu)";
            var parameters = new DynamicParameters();
            parameters.Add("@hammaddeGrupId", createHammaddelerDto.HammaddeGrupId);
            parameters.Add("@malzemeKodu", createHammaddelerDto.MalzemeKodu);
            parameters.Add("@malzemeAciklamasi", createHammaddelerDto.MalzemeAciklamasi);
            parameters.Add("@personelSicilNo", createHammaddelerDto.PersonelSicilNo);
            parameters.Add("@eklenmeGuncellenmeTarihi", createHammaddelerDto.EklenmeGuncellenmeTarihi);
            parameters.Add("@kullanımDurumu", createHammaddelerDto.KullanımDurumu);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
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
            string query = "Update Hammaddeler Set HammaddeGrupID=@hammaddeGrupId,MalzemeKodu=@malzemeKodu,MalzemeAciklamasi=@malzemeAciklamasi,PersonelSicilNo=@personelSicilNo,EklenmeGuncellenmeTarihi=@eklenmeGüncellenmeTarihi,KullanımDurumu=@kullanımDurumu where HammaddeId=@hammaddeId";
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
