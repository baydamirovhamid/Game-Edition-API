using game.edition.api.DTO.HelperModels.Const;
using game.edition.api.Services.Interface;
using StatusCodeModel = game.edition.api.DTO.HelperModels.Const.StatusCodeModel;

namespace game.edition.api.Services.Implementation
{
    public class ValidationCommon: IValidationCommon
    {
        public int CheckErrorCode(int error)
        {
            switch (error)
            {
                case ErrorCodes.AUTH:
                    return StatusCodeModel.AUTH;

                case ErrorCodes.FORBIDDEN:
                    return StatusCodeModel.FORBIDDEN;

                case ErrorCodes.NOT_FOUND:
                    return StatusCodeModel.NOT_FOUND;

                case ErrorCodes.REQUIRED:
                case ErrorCodes.FORMAT:
                case ErrorCodes.ALREADY_PAID:
                    return StatusCodeModel.BAD_REQUEST;

                case ErrorCodes.SYSTEM:
                case ErrorCodes.DB:
                    return StatusCodeModel.INTERNEL_SERVER;
            }
            return StatusCodeModel.OK;
        }
    }
}
