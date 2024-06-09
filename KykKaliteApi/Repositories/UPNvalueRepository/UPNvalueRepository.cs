using Dapper;
using KykKaliteApi.Dtos.HMPNvalueDtos;
using KykKaliteApi.Dtos.ParametrelerDtos;
using KykKaliteApi.Dtos.UPatamaDtos;
using KykKaliteApi.Dtos.UPNvalueDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.UPNvalueRepository
{
    public class UPNvalueRepository : IUPNvalueRepository
    {
        private readonly Context _context;
        public UPNvalueRepository(Context context)
        {
            _context = context;
        }

        public async void CreateUPNvalue(CreateUPNvalueDto createUPNvalueDto)
        {
            string query = "insert into UPNvalue (UpatamaKodu,NumuneId,Value,EklenmeTarihi,PersonelSicilNo) values (@upatamaKodu,@numuneId,@value,@eklenmeTarihi,@personelSicilNo)";
            var parameters = new DynamicParameters();
            parameters.Add("@upatamaKodu", createUPNvalueDto.UpatamaKodu);
            parameters.Add("@numuneId", createUPNvalueDto.NumuneId);
            parameters.Add("@value", createUPNvalueDto.Value);
            parameters.Add("@eklenmeTarihi", createUPNvalueDto.EklenmeTarihi);
            parameters.Add("@personelSicilNo", createUPNvalueDto.PersonelSicilNo);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultUPNvalueDto>> GetAllUPNvalueAsync()
        {
            string query = "Select * From UPNvalue";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultUPNvalueDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateUPNvalue(UpdateUPNvalueDto updateUPNvalueDto)
        {
            string query = "UPDATE UPNvalue SET UpatamaKodu = @upatamaKodu,NumuneId = @numuneId, Value = @value, EklenmeTarihi = @eklenmeTarihi, PersonelSicilNo = @personelSicilNo WHERE UpnvalueId = @upnvalueId";
            var parameters = new DynamicParameters();
            parameters.Add("@upnvalueId", updateUPNvalueDto.UpnvalueId);
            parameters.Add("@upatamaKodu", updateUPNvalueDto.UpatamaKodu);
            parameters.Add("@numuneId", updateUPNvalueDto.NumuneId);
            parameters.Add("@value", updateUPNvalueDto.Value);
            parameters.Add("@eklenmeTarihi", updateUPNvalueDto.EklenmeTarihi);
            parameters.Add("@personelSicilNo", updateUPNvalueDto.PersonelSicilNo);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
