using KykKaliteApi.Dtos.HMPNvalueDtos;
using KykKaliteApi.Dtos.KullaniciDtos;

namespace KykKaliteApi.Repositories.KullaniciRepository
{
    public interface IKullaniciRepository
    {
        Task<List<ResultKullaniciDto>> GetAllKullaniciAsync();
        void DeleteKullanici(int id);
        void CreateKullanici(CreateKullaniciDto createKullaniciDto);
    }
}
