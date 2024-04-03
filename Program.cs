using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Data.Repositories;
using UrlShortener.Dto;
using UrlShortener.Entities;
using UrlShortener.Interfaces.Repositories;
using UrlShortener.Interfaces.Services;
using UrlShortener.Services;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql"));
});

builder.Services.AddScoped<IUrlRepository, UrlRepository>();
builder.Services.AddSingleton<IShortUrlHashService, ShortUrlHashService>();

var app = builder.Build();

app.MapGet("{shortCode}", async (string shortCode, IUrlRepository urlRepository) =>
{
    if (string.IsNullOrWhiteSpace(shortCode))
        return Results.BadRequest();

    var urlFromDatabase = await urlRepository.GetByShortCodeAsync(shortCode);

    if (urlFromDatabase is null)
        return Results.NotFound();

    return Results.Redirect(urlFromDatabase.LongUrl, permanent: true);
});

app.MapPost("/v1/short", async (CreateShortUrlDto createShortUrlDto, IUrlRepository urlRepository, IShortUrlHashService shortUrlHashService) =>
{
    if (string.IsNullOrWhiteSpace(createShortUrlDto.Url))
        return Results.BadRequest();

    var urlFromDatabase = await urlRepository.GetByLongUrlAsync(createShortUrlDto.Url);

    if (urlFromDatabase is not null)
        return Results.Ok(urlFromDatabase);

    var url = new Url
    {
        Short = await shortUrlHashService.CreateAsync(createShortUrlDto.Url),
        LongUrl = createShortUrlDto.Url,
        CreatedAt = DateTime.UtcNow,
    };

    await urlRepository.SaveAsync(url);

    return Results.Ok(url);
});

app.Run();

