using game.edition.api.DTO.RequestModels;
using game.edition.api.DTO.ResponseModels.Inner;
using game.edition.api.DTO.ResponseModels.Main;
using game.edition.api.Services.Implementation;

namespace game.edition.api.Services.Interface
{
    public interface IGameService
    {
        Task<ResponseSimple> CreateAsync(ResponseSimple response, GameDto model);
        Task<ResponseSimple> UpdateAsync(ResponseSimple response, GameDto model, int id);
        Task<ResponseSimple> DeleteAsync(ResponseSimple response, int id);
        Task<GameVM> GetByIdAsync(int id);
        Task<ResponseListTotal<GameVM>> GetAll(ResponseListTotal<GameVM> response, int page, int pageSize);
    }
}
