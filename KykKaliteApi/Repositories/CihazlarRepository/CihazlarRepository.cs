using Dapper;
using KykKaliteApi.Dtos.CihazlarDtos;
using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.KullaniciDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.CihazlarRepository
{
    public class CihazlarRepository : ICihazlarRepository
    {
        private readonly Context _context;
        public CihazlarRepository(Context context)
        {
            _context = context;
        }

        public async void CreateCihazlar(CreateCihazlarDto cihazlarDto)
        {

            string query = "insert into Cihazlar (CihazKodu,KullanılanCihazEkipman,FabrikaId,PersonelSicilNo,EklenmeGuncellenmeTarihi,KullanımDurumu,CihazBilgi) values (@cihazKodu,@kullanılanCihazEkipman,@fabrikaId,@personelSicilNo,@eklenmeGuncellenmeTarihi,@kullanımDurumu,@cihazBilgi)";
            var parameters = new DynamicParameters();
            parameters.Add("@cihazKodu",cihazlarDto.CihazKodu);
            parameters.Add("@kullanılanCihazEkipman", cihazlarDto.KullanılanCihazEkipman);
            parameters.Add("@fabrikaID", cihazlarDto.FabrikaID);
            parameters.Add("@personelSicilNo", cihazlarDto.PersonelSicilNo);
            parameters.Add("@eklenmeGuncellenmeTarihi", cihazlarDto.EklenmeGuncellenmeTarihi);
            parameters.Add("@cihazBilgi", cihazlarDto.CihazBilgi);
            parameters.Add("@kullanımdurumu", true);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultCihazlarDto>> GetAllCihazlarAsync()
        {
            string query = "Select * From Cihazlar";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCihazlarDto>(query);
                return values.ToList();
            }
        }
        public async void DeleteCihazlar(int id)
        {
            string query = "Delete From Cihazlar Where CihazId=@cihazID";
            var parameters = new DynamicParameters();
            parameters.Add("@cihazID", id);
            using (var connection = _context.CreateConnection())
            {
                int v = await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<GetByIDCihazlarDto> GetCihazlar(int id)
        {
            string query = "Select * From Cihazlar Where CihazID=@cihazID";
            var parameters = new DynamicParameters();
            parameters.Add("@cihazID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIDCihazlarDto>(query, parameters);
                return values;
            }
        }

        public async void UpdateCihazlar(UpdateCihazlarDto updateCihazlarDto)
        {
                string query = "UPDATE Cihazlar SET CihazKodu = @cihazKodu,KullanılanCihazEkipman = @kullanılanCihazEkipman, CihazBilgi = @cihazBilgi, FabrikaID = @fabrikaID, PersonelSicilNo = @personelSicilNo,EklenmeGuncellenmeTarihi = @eklenmeGuncellenmeTarihi ,KullanımDurumu = @kullanımDurumu WHERE CihazID = @cihazID";
                var parameters = new DynamicParameters();
                parameters.Add("@cihazKodu", updateCihazlarDto.CihazKodu);
                parameters.Add("@kullanılanCihazEkipman", updateCihazlarDto.KullanılanCihazEkipman);
                parameters.Add("@cihazBilgi", updateCihazlarDto.CihazBilgi);
                parameters.Add("@fabrikaID", updateCihazlarDto.FabrikaID);
                parameters.Add("@personelSicilNo", updateCihazlarDto.PersonelSicilNo);
                parameters.Add("@eklenmeGuncellenmeTarihi", updateCihazlarDto.EklenmeGuncellenmeTarihi);
                parameters.Add("@kullanımDurumu", updateCihazlarDto.KullanımDurumu);
                parameters.Add("@cihazID", updateCihazlarDto.CihazID);

            using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, parameters);
                }
            
        }
    }
}
