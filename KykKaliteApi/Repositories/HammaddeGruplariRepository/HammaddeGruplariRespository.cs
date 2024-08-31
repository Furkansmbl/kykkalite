using Dapper;
using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.HammaddeGruplariDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.HammaddeGruplariRepository
{
    public class HammaddeGruplariRespository(Context context) : IHammaddeGruplariRespository
    {
        private readonly Context _context = context;

        public async void CreateHammaddeGruplari(CreateHammaddeGruplariDto createHammaddeGruplariDto)
        {
            string query = "insert into HammaddeGruplari (HMGrupAdi,EklenmeGuncellenmeTarihi) values (@hMGrupAdi,@eklenmeGuncellenmeTarihi)";
            var parameters = new DynamicParameters();
            parameters.Add("@hMGrupAdi", createHammaddeGruplariDto.HMGrupAdi);
            parameters.Add("@eklenmeGuncellenmeTarihi", createHammaddeGruplariDto.EklenmeGuncellenmeTarihi);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultHammaddeGruplariDto>> GetAllHammaddeGruplari()
        {
            string query = "Select * From HammaddeGruplari";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultHammaddeGruplariDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateHammaddeGruplari(UpdateHammaddeGruplariDto updateHammaddeGruplariDto)
        {
            string query = "UPDATE HammaddeGruplari Set HMGrupAdi = @hMGrupAdi,EklenmeGuncellenmeTarihi = @eklenmeGuncellenmeTarihi WHERE HammaddeGrupID = @hammaddeGrupID";
            var parameters = new DynamicParameters();
            parameters.Add("@hMGrupAdi", updateHammaddeGruplariDto.HMGrupAdi);
            parameters.Add("@hammaddeGrupID", updateHammaddeGruplariDto.HammaddeGrupID); 
            parameters.Add("@eklenmeGuncellenmeTarihi", updateHammaddeGruplariDto.EklenmeGuncellenmeTarihi);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
