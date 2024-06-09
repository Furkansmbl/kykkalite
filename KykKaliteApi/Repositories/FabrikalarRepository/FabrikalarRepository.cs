using Dapper;
using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.FabrikalarRepository
{
    public class FabrikalarRepository : IFabrikalarRepository
    {
        private readonly Context _context;
        public FabrikalarRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultFabrikaDto>> GetAllFabrikaAsync()
        {
            string query = "Select * From Fabrikalar";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultFabrikaDto>(query);
                return values.ToList();
            }
        }
       
        public async void CreateFabrika(CreateFabrikaDTO fabrikaDto)
        {
            string query = "insert into Fabrikalar (FabrikaAdi) values (@fabrikaAdi)";
            var parameters = new DynamicParameters();
            parameters.Add("@fabrikaAdi", fabrikaDto.FabrikaAdi);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

          public async void DeleteFabrika(int id)
        {
            string query = "Delete From Fabrikalar Where FabrikaID=@fabrikaID";
            var parameters = new DynamicParameters();
            parameters.Add("@fabrikaID", id);
            using (var connection = _context.CreateConnection())
            {
                int v = await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void UpdateFabrika(UpdateFabrikaDto updateFabrikaDto)
        {
            string query = "Update Fabrikalar Set FabrikaAdi=@fabrikaAdi where FabrikaID=@fabrikaID";
            var parameters = new DynamicParameters();
            parameters.Add("@fabrikaAdi",updateFabrikaDto.FabrikaAdi);
            parameters.Add("@fabrikaID", updateFabrikaDto.FabrikaID);
            using (var connectiont = _context.CreateConnection())
            {
                await connectiont.ExecuteAsync(query, parameters);
            }
        }
    }
}
