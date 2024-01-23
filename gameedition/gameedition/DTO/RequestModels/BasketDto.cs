namespace game.edition.api.DTO.RequestModels
{
    public class BasketDto
    {
        public int? CustomerId { get; set; }
        public int? GameId { get; set; }
        public DateTime? Date { get; set; }
        public int? TotalAmount { get; set; }
    }
}
