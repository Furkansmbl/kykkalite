using Dapper;
using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.HammaddelerDtos;
using KykKaliteApi.Dtos.HMnumuneDtos;
using KykKaliteApi.Dtos.HMPNvalueDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.HMPNvalueRepository
{
    public class HMPNvalueRepository : IHMPNvalueRepository
    {
        private readonly Context _context;
        public HMPNvalueRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<ResultHMPNvalueDto>> GetAllHMPatamaAsync()
        {

            string query = "Select * From HMPNvalue";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultHMPNvalueDto>(query);
                return values.ToList();
            }
        }
        public async void CreateHMPNvalue(CreateHMPNvalueDto createHMPNvalueDto)
        {
            string query = "insert into HMnumune (HmpatamaKodu,Value,EklenmeTarihi,PersonelSicilNo) values (@hmpatamaKodu,@value,@eklenmeTarihi,@personelSicilNo)";
            var parameters = new DynamicParameters();
            parameters.Add("@hmpatamaKodu", createHMPNvalueDto.HmpatamaKodu);
            parameters.Add("@value", createHMPNvalueDto.Value);
            parameters.Add("@eklenmeTarihi", createHMPNvalueDto.EklenmeTarihi);
            parameters.Add("@personelSicilNo", createHMPNvalueDto.PersonelSicilNo);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void UpdateHMPNvalue(UpdateHMPNvalueDto updateHMPNvalueDto)
        {
            string query = "Update HMPNvalue Set HMPAtamaKodu=@HMPAtamaKodu,Value=@value,EklenmeTarihi=@eklenmeTarihi,PersonelSicilNo=@personelSicilNo where HMPNValueId=@hMPNValueId";
            var parameters = new DynamicParameters();
            parameters.Add("@hMPNValueId", updateHMPNvalueDto.HmpnvalueId);
            parameters.Add("@malzemeKodu", updateHMPNvalueDto.HmpatamaKodu);
            parameters.Add("@malzemeAciklamasi", updateHMPNvalueDto.Value);
            parameters.Add("@personelSicilNo", updateHMPNvalueDto.EklenmeTarihi);
            parameters.Add("@eklenmeGüncellenmeTarihi", updateHMPNvalueDto.PersonelSicilNo);

            using (var connectiont = _context.CreateConnection())
            {
                await connectiont.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteHMPNvalue(int id)
        {
            string query = "Delete From HMPNvalue Where HMPNValueID=@hMPNValueID";
            var parameters = new DynamicParameters();
            parameters.Add("@hMPNValueID", id);
            using (var connection = _context.CreateConnection())
            {
                int v = await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
