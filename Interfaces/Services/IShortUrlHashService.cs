namespace UrlShortener.Interfaces.Services
{
    public interface IShortUrlHashService
    {
        Task<string> CreateAsync(string longUrl);
    }
}
