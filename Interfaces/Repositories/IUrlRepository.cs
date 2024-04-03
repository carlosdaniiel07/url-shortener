using UrlShortener.Entities;

namespace UrlShortener.Interfaces.Repositories
{
    public interface IUrlRepository
    {
        Task<Url?> GetByShortCodeAsync(string shortCode);
        Task<Url?> GetByLongUrlAsync(string longUrl);
        Task SaveAsync(Url url);
    }
}
