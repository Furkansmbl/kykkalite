using KykKaliteApi.Dtos.HMnumuneDtos;
using KykKaliteApi.Dtos.HMPatamaDtos;

namespace KykKaliteApi.Repositories.HMPatamaRepository
{
    public interface IHMPatamaRepository
    {
        Task<List<ResultHMPatamaDto>> GetAllHMPatamaAsync();
        void CreateHMPatama(CreateHMPatamaDto createHMPatamaDto);
        void UpdateHMPatama(UpdateHMPatamaDto updateHMPatamaDto);
    }
}
