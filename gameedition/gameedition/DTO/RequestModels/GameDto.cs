namespace game.edition.api.DTO.RequestModels
{
    public class GameDto
    {
        public string? Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public int? Price { get; set; }
        public int? CompanyId { get; set; }
        public int? PlatformId { get; set; }
    }
}
