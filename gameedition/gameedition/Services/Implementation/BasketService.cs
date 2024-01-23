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
    public class BasketService: IBasketService
    {
        private readonly IRepository<BASKET> _baskets;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private readonly ISqlService _sqlService;
        private readonly IMapper _mapper;

        public BasketService(
            IRepository<BASKET> baskets,
            ILoggerManager logger,
            IConfiguration configuration,
            ISqlService sqlService,
            IMapper mapper)
        {
            _baskets = baskets;
            _logger = logger;
            _configuration = configuration;
            _sqlService = sqlService;
            _mapper = mapper;
        }

        public async Task<ResponseSimple> CreateAsync(ResponseSimple response, BasketDto model)
        {
            try
            {
                var basket = _mapper.Map<BASKET>(model);
                basket.CreatedAt = DateTime.Now;
                _baskets.Insert(basket);
                await _baskets.SaveAsync();
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

        public async Task<ResponseSimple> UpdateAsync(ResponseSimple response, BasketDto model, int id)
        {
            try
            {
                var basket = _mapper.Map<BASKET>(model);

                var basketDb = await _baskets.AllQuery
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                basket.Id = id;
                basket.UpdatedAt = DateTime.Now;
                basket.CreatedAt = basketDb.CreatedAt;

                _baskets.Update(basket);
                await _baskets.SaveAsync();
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
                var game = await _baskets.AllQuery.FirstOrDefaultAsync(x => x.Id == id);
                _baskets.Remove(game);
                await _baskets.SaveAsync();
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
        public async Task<BasketVM> GetByIdAsync(int id)
        {
            var db_model = await _baskets.AllQuery.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<BasketVM>(db_model);
        }

        public async Task<ResponseListTotal<BasketVM>> GetAll(ResponseListTotal<BasketVM> response, int page, int pageSize)
        {

            var db_data = await _baskets.AllQuery.OrderByDescending(x => x.CreatedAt).ToListAsync();
            response.Response.Total = db_data.Count;
            db_data = db_data.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            response.Response.Data = _mapper.Map<List<BasketVM>>(db_data);
            return response;
        }
    }
}
