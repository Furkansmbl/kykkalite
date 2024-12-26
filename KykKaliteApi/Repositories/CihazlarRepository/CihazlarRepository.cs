using Dapper;
using KykKaliteApi.Dtos.CihazlarDtos;
using KykKaliteApi.Dtos.FabrikalarDtos;
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

            string query = "insert into Cihazlar values (@cihazKodu,@kullanılanCihazEkipman,@fabrikaId,@personelSicilNo,@eklenmeGuncellenmeTarihi,@kullanımDurumu)";
            var parameters = new DynamicParameters();
            parameters.Add("@cihazKodu",cihazlarDto.CihazKodu);
            parameters.Add("@kullanılanCihazEkipman", cihazlarDto.KullanılanCihazEkipman);
            parameters.Add("@fabrikaID", cihazlarDto.FabrikaID);
            parameters.Add("@personelSicilNo", cihazlarDto.PersonelSicilNo);
            parameters.Add("@eklenmeGuncellenmeTarihi", cihazlarDto.EklenmeGuncellenmeTarihi);
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
    }
}
