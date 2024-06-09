using KykKaliteApi.Dtos.UPatamaDtos;
using KykKaliteApi.Dtos.UPNvalueDtos;

namespace KykKaliteApi.Repositories.UPNvalueRepository
{
    public interface IUPNvalueRepository
    {
        Task<List<ResultUPNvalueDto>> GetAllUPNvalueAsync();
        void CreateUPNvalue(CreateUPNvalueDto createUPNvalueDto);
        void UpdateUPNvalue(UpdateUPNvalueDto updateUPNvalueDto);
    }
}
