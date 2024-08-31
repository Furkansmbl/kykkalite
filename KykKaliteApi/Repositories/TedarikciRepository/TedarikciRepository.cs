using Dapper;
using KykKaliteApi.Dtos.CihazlarDtos;
using KykKaliteApi.Dtos.KullaniciDtos;
using KykKaliteApi.Dtos.TedarikciDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.TedarikciRepository
{
    public class TedarikciRepository : ITedarikciRepository
    {
        private readonly Context _context;
        public TedarikciRepository(Context context)
        {
            _context = context;
        }

        public async void CreateTedarikci(ResultTedarikciDto resultTedarikciDto)
        {

            string query = "insert into TedarikciHammadde (THMID,UNVANI,ISYERIADI,MALZADI) values (@THMID,@UNVANI,@ISYERIADI,@MALZADI)";
            var parameters = new DynamicParameters();
            parameters.Add("@cihazKodu", resultTedarikciDto.THMID);
            parameters.Add("@kullanılanCihazEkipman", resultTedarikciDto.UNVANI);
            parameters.Add("@fabrikaID", resultTedarikciDto.ISYERIADI);
            parameters.Add("@personelSicilNo", resultTedarikciDto.MALZADI);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultTedarikciDto>> GetAllTedarikciAsync()
        {
            string query = "Select * From TedarikciHammadde";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultTedarikciDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateTedarikci(UpdateTedarikciDto updateTedarikciDto)
        {
            string query = "UPDATE TedarikciHammadde SET UNVANI = @UNVANI,ISYERIADI = @ISYERIADI, MALZADI = @MALZADI WHERE THMID = @THMID";
            var parameters = new DynamicParameters();
            parameters.Add("@UNVANI", updateTedarikciDto.UNVANI);
            parameters.Add("@ISYERIADI", updateTedarikciDto.ISYERIADI);
            parameters.Add("@MALZADI", updateTedarikciDto.MALZADI);
            parameters.Add("@THMID", updateTedarikciDto.THMID);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
