using KykKaliteApi.Dtos.UPatamaDtos;
using KykKaliteApi.Dtos.UPNvalueDtos;
using KykKaliteApi.Dtos.UretimHatlariDtos;

namespace KykKaliteApi.Repositories.UretimHatlariRepository
{
    public interface IUretimHatlariRepository
    {
        Task<List<ResultUretimHatlariDto>> GetAllUretimHatlariAsync();
        void DeleteUretimHatlari(int id);
        void CreateUretimHatlari(CreateUretimHatlariDto createUretimHatlariDto);
        void UpdateUretimHatlari(UpdateUretimHatlariDto updateUretimHatlariDto);
    }
}