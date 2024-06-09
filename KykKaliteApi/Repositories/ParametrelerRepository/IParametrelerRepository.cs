using KykKaliteApi.Dtos.KullaniciDtos;
using KykKaliteApi.Dtos.ParametrelerDtos;
using KykKaliteApi.Dtos.UrunlerDtos;

namespace KykKaliteApi.Repositories.ParametrelerRepository
{
    public interface IParametrelerRepository
    {
        Task<List<ResultParametrelerDto>> GetAllParametrelerAsync();
        void CreateParametreler(CreateParametrelerDto createParametrelerDto);
        void DeleteParametreler(int id);
        void UpdateParametreler(UpdateParametrelerDto updateParametrelerDto);

    }
}
