using KykKaliteApi.Dtos.GetUpatamaKodlariByUrunIDDtos;
using KykKaliteApi.Dtos.UrunGruplariDtos;
using KykKaliteApi.Dtos.UrunlerDtos;

namespace KykKaliteApi.Repositories.UrunlerRepository
{
    public interface IUrunlerRepository
    {
        Task<List<ResultUrunlerDto>> GetAllUrunlerAsync();
        void CreateUrunler(CreateUrunlerDto createUrunlerDto );
        void UpdateUrunler(UpdateUrunlerDto updateUrunlerDto);
        Task<List<ResultUrunlerDto>> GetUrunlerByMalzemeAciklamasi(int urunId, string malzemeAciklamasi);

    }
}
