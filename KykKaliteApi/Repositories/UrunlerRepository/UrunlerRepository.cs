using Dapper;
using KykKaliteApi.Dtos.GetUpatamaKodlariByUrunIDDtos;
using KykKaliteApi.Dtos.UrunGruplariDtos;
using KykKaliteApi.Dtos.UrunlerDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.UrunlerRepository
{
    public class UrunlerRepository : IUrunlerRepository
    {
        private readonly Context _context;
        public UrunlerRepository(Context context)
        {
            _context = context;
        }
        //Yeni Ürün EKleme//
        public async void CreateUrunler(CreateUrunlerDto createUrunlerDto)
        {

            string query = "insert into Urunler (UrunGrupId,MalzemeKodu,MalzemeAciklamasi,PersonelSicilNo,EklenmeGuncellenmeTarihi,KullanimDurumu) values (@urunGrupId,@malzemeKodu,@malzemeAciklamasi,@personelSicilNo,@eklenmeGuncellenmeTarihi,@kullanimDurumu)";
            var parameters = new DynamicParameters();
            parameters.Add("@urunGrupId", createUrunlerDto.UrunGrupId);
            parameters.Add("@malzemeKodu", createUrunlerDto.MalzemeKodu);
            parameters.Add("@malzemeAciklamasi", createUrunlerDto.MalzemeAciklamasi);
            parameters.Add("@personelSicilNo", createUrunlerDto.PersonelSicilNo);
            parameters.Add("@eklenmeGuncellenmeTarihi", createUrunlerDto.EklenmeGuncellenmeTarihi);
            parameters.Add("@kullanimDurumu", createUrunlerDto.KullanimDurumu);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        //Ürünlerdeki verileri çağırma//
        public async Task<List<ResultUrunlerDto>> GetAllUrunlerAsync()
        {
            string query = "Select * From Urunler";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultUrunlerDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultUrunlerDto>> GetUrunlerByMalzemeAciklamasi(int urunId, string malzemeAciklamasi)
        {
            string query = "SELECT * FROM Urunler WHERE MalzemeAciklamasi = @malzemeAciklamasi";
            var parameters = new DynamicParameters();
            parameters.Add("@urunId", urunId);
            parameters.Add("@malzemeAciklamasi", malzemeAciklamasi);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultUrunlerDto>(query, parameters);
                return values.ToList();
            }
        }

        public async void UpdateUrunler(UpdateUrunlerDto updateUrunlerDto)
        {
            string query = "UPDATE Urunler SET MalzemeKodu = @malzemeKodu,MalzemeAciklamasi = @malzemeAciklamasi,PersonelSicilNo = @personelSicilNo,EklenmeGuncellenmeTarihi = @eklenmeGuncellenmeTarihi,KullanimDurumu = @kullanimDurumu WHERE UrunId = @urunId";
            var parameters = new DynamicParameters();
            parameters.Add("@urunId", updateUrunlerDto.UrunId);
            parameters.Add("@urunGrupId", updateUrunlerDto.UrunGrupId);
            parameters.Add("@malzemeKodu", updateUrunlerDto.MalzemeKodu);
            parameters.Add("@malzemeAciklamasi", updateUrunlerDto.MalzemeAciklamasi);
            parameters.Add("@personelSicilNo", updateUrunlerDto.PersonelSicilNo);
            parameters.Add("@eklenmeGuncellenmeTarihi", updateUrunlerDto.EklenmeGuncellenmeTarihi);
            parameters.Add("@kullanimDurumu", updateUrunlerDto.KullanimDurumu);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
