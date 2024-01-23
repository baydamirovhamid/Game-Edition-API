using game.edition.api.DTO.RequestModels;
using game.edition.api.DTO.ResponseModels.Main;
using game.edition.api.Models;

namespace game.edition.api.Services.Interface
{
    public interface IBasketService
    {
        Task<ResponseSimple> CreateAsync(ResponseSimple response, BasketDto model);
        Task<ResponseSimple> UpdateAsync(ResponseSimple response, BasketDto model, int id);
        Task<ResponseSimple> DeleteAsync(ResponseSimple response, int id);
        Task<BasketVM> GetByIdAsync(int id);
        Task<ResponseListTotal<BasketVM>> GetAll(ResponseListTotal<BasketVM> response, int page, int pageSize);
    }
}
