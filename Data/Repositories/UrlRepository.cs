using Microsoft.EntityFrameworkCore;
using UrlShortener.Entities;
using UrlShortener.Interfaces.Repositories;

namespace UrlShortener.Data.Repositories
{
    public class UrlRepository(ApplicationDbContext context) : IUrlRepository
    {
        public async Task<Url?> GetByShortCodeAsync(string shortCode)
        {
            return await context.Urls
                .SingleOrDefaultAsync(x => x.Short == shortCode);
        }

        public async Task<Url?> GetByLongUrlAsync(string longUrl)
        {
            return await context.Urls
                .SingleOrDefaultAsync(x => x.LongUrl == longUrl);
        }

        public async Task SaveAsync(Url url)
        {
            await context.Urls.AddAsync(url);
            await context.SaveChangesAsync();
        }
    }
}
