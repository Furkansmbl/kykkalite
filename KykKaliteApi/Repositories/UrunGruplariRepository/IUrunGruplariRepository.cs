using KykKaliteApi.Dtos.UretimHatlariDtos;
using KykKaliteApi.Dtos.UrunGruplariDtos;

namespace KykKaliteApi.Repositories.UrunGruplariRepository
{
    public interface IUrunGruplariRepository
    {
        Task<List<ResultUrunGruplariDto>> GetAllUrunGruplariAsync();
        void DeleteUrunGruplari(int id);
        void CreateUrunGruplari(CreateUrunGruplariDto createUrunGruplariDto);
        void UpdateUrunGruplari(UpdateUrunGruplariDto updateUrunGruplariDto);
    }
}
