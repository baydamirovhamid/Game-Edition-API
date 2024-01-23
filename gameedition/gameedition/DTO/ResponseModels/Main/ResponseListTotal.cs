using game.edition.api.DTO.HelperModels;

namespace game.edition.api.DTO.ResponseModels.Main
{
    public class ResponseListTotal<T>
    {
        public StatusModel Status { get; set; }
        public ResponseTotal<T> Response { get; set; }
        public string TraceID { get; set; }
    }
}
