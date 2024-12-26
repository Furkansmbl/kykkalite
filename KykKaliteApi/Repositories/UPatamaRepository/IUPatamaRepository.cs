
using KykKaliteApi.Dtos.UnumuneDtos;
using KykKaliteApi.Dtos.UPatamaDtos;

namespace KykKaliteApi.Repositories.UPatamaRepository
{
    public interface IUPatamaRepository
    {
        Task<List<ResultUPatamaDto>> GetAllUPatamaAsync();
        void DeleteUPatama(int id);
        void CreateUPatama(CreateUPatamaDto createUPatamaDto);
        void UpdateUPatama(UpdateUPatamaDto updateUPatamaDto);
    }
}
