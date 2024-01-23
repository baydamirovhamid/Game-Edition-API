using Microsoft.AspNetCore.JsonPatch;
using game.edition.api.DTO.RequestModels;
using game.edition.api.DTO.ResponseModels.Inner;
using game.edition.api.DTO.ResponseModels.Main;

namespace game.edition.api.Services.Interface
{
    public interface IPlatformService
    {
        Task<ResponseSimple> CreateAsync(ResponseSimple response, PlatformDto model);
        Task<ResponseSimple> UpdateAsync(ResponseSimple response, PlatformDto model, int id);
        Task<ResponseSimple> DeleteAsync(ResponseSimple response, int id);
        Task<PlatformVM> GetByIdAsync(int id);
        Task<ResponseListTotal<PlatformVM>> GetAll(ResponseListTotal<PlatformVM> response, int page, int pageSize);
        Task<ResponseSimple> UpdatePlatformAsync(ResponseSimple response, PlatformDto model, int id);
        Task<ResponseSimple> PartiallyUpdatePlatformAsync(ResponseSimple response, int id, JsonPatchDocument<PlatformDto> model);

    }
}
