using KykKaliteApi.Dtos.HMPNvalueDtos;
using KykKaliteApi.Dtos.KullaniciDtos;
using KykKaliteApi.Dtos.UPNvalueDtos;

namespace KykKaliteApi.Repositories.KullaniciRepository
{
    public interface IKullaniciRepository
    {
        Task<List<ResultKullaniciDto>> GetAllKullaniciAsync();
        void DeleteKullanici(int id);
        void CreateKullanici(CreateKullaniciDto createKullaniciDto);
        void UpdateKullanici(UpdateKullaniciDto updateKullaniciDto);

    }
}
