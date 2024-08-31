using KykKaliteApi.Dtos.CihazlarDtos;
using KykKaliteApi.Dtos.UretimHatlariDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.CihazlarRepository
{
    public interface ICihazlarRepository
    {
        Task<List<ResultCihazlarDto>> GetAllCihazlarAsync();
        void CreateCihazlar(CreateCihazlarDto cihazlarDto);
        void DeleteCihazlar(int id);
        Task<GetByIDCihazlarDto> GetCihazlar(int id);
        void UpdateCihazlar(UpdateCihazlarDto updateCihazlarDto);

    }
}
