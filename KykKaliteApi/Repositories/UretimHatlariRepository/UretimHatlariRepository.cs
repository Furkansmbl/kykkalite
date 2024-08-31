using Dapper;
using KykKaliteApi.Dtos.HMPNvalueDtos;
using KykKaliteApi.Dtos.UPNvalueDtos;
using KykKaliteApi.Dtos.UretimHatlariDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.UretimHatlariRepository
{
    public class UretimHatlariRepository : IUretimHatlariRepository
    {
        private readonly Context _context;
        public UretimHatlariRepository(Context context)
        {
            _context = context;
        }

        public async void CreateUretimHatlari(CreateUretimHatlariDto createUretimHatlariDto)
        {
            string query = "insert into UretimHatlari (FabrikaId,HatAdiAciklamasi,EklenmeGuncellenmeTarihi) values (@fabrikaId,@hatAdiAciklamasi,@eklenmeGuncellenmeTarihi)";
            var parameters = new DynamicParameters();
            parameters.Add("@fabrikaId",createUretimHatlariDto.FabrikaId);
            parameters.Add("@hatAdiAciklamasi", createUretimHatlariDto.HatAdiAciklamasi);
            parameters.Add("@eklenmeGuncellenmeTarihi", createUretimHatlariDto.EklenmeGuncellenmeTarihi);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteUretimHatlari(int id)
        {
            string query = "Delete From UretimHatlari Where UretimHattiId=@uretimHattiId";
            var parameters = new DynamicParameters();
            parameters.Add("uretimHattiId", id);
            using (var connection = _context.CreateConnection())
            {
                int v = await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultUretimHatlariDto>> GetAllUretimHatlariAsync()
        {
            string query = "Select * From UretimHatlari";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultUretimHatlariDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateUretimHatlari(UpdateUretimHatlariDto updateUretimHatlariDto)
        {
            string query = "UPDATE UretimHatlari SET FabrikaId = @fabrikaId, HatAdiAciklamasi = @hatAdiAciklamasi, EklenmeGuncellenmeTarihi = @eklenmeGuncellenmeTarihi WHERE UretimHattiId = @uretimHattiId";
            var parameters = new DynamicParameters();
            parameters.Add("@UretimHattiId", updateUretimHatlariDto.UretimHattiId); 
            parameters.Add("@fabrikaId", updateUretimHatlariDto.FabrikaId);
            parameters.Add("@hatAdiAciklamasi", updateUretimHatlariDto.HatAdiAciklamasi);
            parameters.Add("@eklenmeGuncellenmeTarihi", updateUretimHatlariDto.EklenmeGuncellenmeTarihi);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
