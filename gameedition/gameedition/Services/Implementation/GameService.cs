using AutoMapper;
using Microsoft.EntityFrameworkCore;
using game.edition.api.DTO.HelperModels.Const;
using game.edition.api.DTO.RequestModels;
using game.edition.api.DTO.ResponseModels.Main;
using game.edition.api.Infrastructure.Repository;
using game.edition.api.Models;
using game.edition.api.Services.Interface;

namespace game.edition.api.Services.Implementation
{
    public class GameService : IGameService
    {
        private readonly IRepository<GAME> _games;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private readonly ISqlService _sqlService;
        private readonly IMapper _mapper;

        public GameService(
            IRepository<GAME> games,
            ILoggerManager logger,
            IConfiguration configuration,
            ISqlService sqlService,
            IMapper mapper)
        {
            _games = games;
            _logger = logger;
            _configuration = configuration;
            _sqlService = sqlService;
            _mapper = mapper;
        }

        public async Task<ResponseSimple> CreateAsync(ResponseSimple response, GameDto model)
        {
            try
            {
                var game = _mapper.Map<GAME>(model);
                game.CreatedAt = DateTime.Now;
                _games.Insert(game);
                await _games.SaveAsync();
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

        public async Task<ResponseSimple> UpdateAsync(ResponseSimple response, GameDto model, int id)
        {
            try
            {
                var game = _mapper.Map<GAME>(model);

                var gameDb = await _games.AllQuery
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                game.Id = id;
                game.UpdatedAt = DateTime.Now;
                game.CreatedAt = gameDb.CreatedAt;

                _games.Update(game);
                await _games.SaveAsync();
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
                var game = await _games.AllQuery.FirstOrDefaultAsync(x => x.Id == id);
                _games.Remove(game);
                await _games.SaveAsync();
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

        public async Task<GameVM> GetByIdAsync(int id)
        {
            var db_model = await _games.AllQuery.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<GameVM>(db_model);
        }

        public async Task<ResponseListTotal<GameVM>> GetAll(ResponseListTotal<GameVM> response, int page, int pageSize)
        {
            
            var db_data = await _games.AllQuery.OrderByDescending(x=>x.CreatedAt).ToListAsync();
            response.Response.Total = db_data.Count;
            db_data = db_data.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            response.Response.Data = _mapper.Map<List<GameVM>>(db_data);
            return response;
        }      
    }
}
