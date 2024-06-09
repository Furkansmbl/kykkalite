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
            string query = "insert into HammaddeGruplari (HMGrupAdi) values (@hMGrupAdi)";
            var parameters = new DynamicParameters();
            parameters.Add("@hMGrupAdi", createHammaddeGruplariDto.HMGrupAdi);
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
            string query = "Update HammaddeGruplari Set HMGrupAdi=@hMGrupAdi where HammaddeGrupID=@hammaddeGrupID";
            var parameters = new DynamicParameters();
            parameters.Add("@hMGrupAdi", updateHammaddeGruplariDto.HMGrupAdi);
            parameters.Add("@hammaddeGrupID", updateHammaddeGruplariDto.HammaddeGrupID); 
            using (var connectiont = _context.CreateConnection())
            {
                await connectiont.ExecuteAsync(query, parameters);
            }
        }
    }
}
