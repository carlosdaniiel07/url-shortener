namespace UrlShortener.Entities
{
    public class Url
    {
        public long Id { get; set; }
        public required string Short { get; set; }
        public required string LongUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
