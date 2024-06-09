using KykKaliteApi.Dtos.NewDtos;

namespace KykKaliteApi.Repositories.NewRepository
{
    public interface INewwRepository
    {
        Task<List<ResultNewwDtp>> GetAllNewlerAsync();
    }
}
