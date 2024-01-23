using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using game.edition.api.DTO.HelperModels.Const;
using game.edition.api.DTO.RequestModels;
using game.edition.api.DTO.ResponseModels.Inner;
using game.edition.api.DTO.ResponseModels.Main;
using game.edition.api.Infrastructure.Repository;
using game.edition.api.Models;
using game.edition.api.Services.Interface;

namespace game.edition.api.Services.Implementation
{
    public class PlatformService : IPlatformService
    {
        private readonly IRepository<PLATFORM> _platforms;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private readonly ISqlService _sqlService;
        private readonly IMapper _mapper;

        public PlatformService(
            IRepository<PLATFORM> platforms,
            ILoggerManager logger,
            IConfiguration configuration,
            ISqlService sqlService,
            IMapper mapper)
        {
            _platforms = platforms;
            _logger = logger;
            _configuration = configuration;
            _sqlService = sqlService;
            _mapper = mapper;
        }

        public async Task<ResponseSimple> CreateAsync(ResponseSimple response, PlatformDto model)
        {
            try
            {
                var platform = _mapper.Map<PLATFORM>(model);
                platform.CreatedAt = DateTime.Now;
                _platforms.Insert(platform);
                await _platforms.SaveAsync();
                response.Status.Message = "Uğurla əlavə olundu!";
            }
            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(CreateAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.DB;
                response.Status.Message = "Problem baş verdi!";
            }
            return response;
        }

        public async Task<ResponseSimple> UpdateAsync(ResponseSimple response, PlatformDto model, int id)
        {
            try
            {
                var platform = _mapper.Map<PLATFORM>(model);

                var platformDb = await _platforms.AllQuery
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                platform.Id = id;
                platform.UpdatedAt = DateTime.Now;
                platform.CreatedAt = platformDb.CreatedAt;

                _platforms.Update(platform);
                await _platforms.SaveAsync();
                response.Status.Message = "Uğurla yeniləndi!";
            }
            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(UpdateAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.DB;
                response.Status.Message = "Problem baş verdi!";
            }
            return response;
        }

        public async Task<ResponseSimple> DeleteAsync(ResponseSimple response, int id)
        {
            try
            {
                var platform = await _platforms.AllQuery.FirstOrDefaultAsync(x => x.Id == id);
                _platforms.Remove(platform);
                await _platforms.SaveAsync();
                response.Status.Message = "Uğurla silindi!";
            }
            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(DeleteAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.DB;
                response.Status.Message = "Problem baş verdi!";
            }
            return response;
        }

        public async Task<PlatformVM> GetByIdAsync(int id)
        {
            var db_model = await _platforms.AllQuery.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<PlatformVM>(db_model);
        }

        public async Task<ResponseListTotal<PlatformVM>> GetAll(ResponseListTotal<PlatformVM> response, int page, int pageSize)
        {

            var db_data = await _platforms.AllQuery.OrderByDescending(x => x.CreatedAt).ToListAsync();
            response.Response.Total = db_data.Count;
            db_data = db_data.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            response.Response.Data = _mapper.Map<List<PlatformVM>>(db_data);
            return response;
        }

        public async Task<ResponseSimple> UpdatePlatformAsync(ResponseSimple response, PlatformDto model, int id)
        {
            try
            {
                var platform = _mapper.Map<PLATFORM>(model);
                var existingPlatform = await _platforms.AllQuery
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
                platform.Id = id;
                _platforms.Update(platform);
                await _platforms.SaveAsync();
                response.Status.ErrorCode = 0;
                response.Status.Message = "Uğurla yeniləndi!";
            }

            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(UpdatePlatformAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.DB;
                response.Status.Message = "Problem baş verdi!";
            }

            return response;
        }


        public async Task<ResponseSimple> PartiallyUpdatePlatformAsync(ResponseSimple response, int id, JsonPatchDocument<PlatformDto> model)
        {
            try
            {
                var existingPlatform = await _platforms.AllQuery
                .FirstOrDefaultAsync(x => x.Id == id);

                if (existingPlatform == null)
                {
                    response.Status.ErrorCode = ErrorCodes.NOT_FOUND;
                    response.Status.Message = "Platform not found.";
                    return response;
                }

                var platformDto = _mapper.Map<PlatformDto>(existingPlatform);

                model.ApplyTo(platformDto);

                _mapper.Map(platformDto, existingPlatform);
                _platforms.Update(existingPlatform);
                await _platforms.SaveAsync();
                response.Status.ErrorCode = 0;
                response.Status.Message = "Uğurla yeniləndi!";
            }

            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(UpdatePlatformAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.DB;
                response.Status.Message = "Problem baş verdi!";
            }
            return response;
        }
    }
}
