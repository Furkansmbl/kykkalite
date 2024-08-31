using KykKaliteApi.Dtos.CihazlarDtos;
using KykKaliteApi.Dtos.TedarikciDtos;

namespace KykKaliteApi.Repositories.TedarikciRepository
{
    public interface ITedarikciRepository
    {
        Task<List<ResultTedarikciDto>> GetAllTedarikciAsync();
        void CreateTedarikci(ResultTedarikciDto resultTedarikciDto);
        void UpdateTedarikci(UpdateTedarikciDto updateTedarikciDto);



    }
}
