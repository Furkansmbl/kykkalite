using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.HammaddelerDtos;
using KykKaliteApi.Dtos.HMPNvalueDtos;

namespace KykKaliteApi.Repositories.HMPNvalueRepository
{
    public interface IHMPNvalueRepository
    {
        Task<List<ResultHMPNvalueDto>> GetAllHMPatamaAsync();
        void CreateHMPNvalue(CreateHMPNvalueDto createHMPNvalueDto );
        void UpdateHMPNvalue(UpdateHMPNvalueDto updateHMPNvalueDto);
        void DeleteHMPNvalue(int id);
    }
}
