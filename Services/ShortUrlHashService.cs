using UrlShortener.Interfaces.Services;

namespace UrlShortener.Services
{
    public class ShortUrlHashService : IShortUrlHashService
    {
        private const string CHARS = "abcdefghijklmnopqrstuvwxyz0123456789";
        private readonly Random _random;

        public ShortUrlHashService()
        {
            _random = new();
        }

        public Task<string> CreateAsync(string longUrl)
        {
            var chars = Enumerable.Repeat(string.Empty, 7)
                .Select(_ => CHARS[_random.Next(CHARS.Length)])
                .ToArray();
            return Task.FromResult(new string(chars));
        }
    }
}
