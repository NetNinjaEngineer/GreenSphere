using GreenSphere.Domain.Entities;
using System.Text.Json;

namespace GreenSphere.Persistence;
public static class DatabaseSeedHelper
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static async Task SeedDatabaseAsync(this ApplicationDbContext context)
    {
        if (!context.Categories.Any())
        {
            var categoriesJson = await File.ReadAllTextAsync($"..//GreenSphere.Persistence//DataSeed//categories.json");
            var categories = JsonSerializer.Deserialize<IEnumerable<Category>>(categoriesJson, Options);
            if (categories != null)
            {
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }
        }

        if (!context.Products.Any())
        {
            var productsJson = await File.ReadAllTextAsync($"..//GreenSphere.Persistence//DataSeed//products.json");
            var products = JsonSerializer.Deserialize<IEnumerable<Product>>(productsJson, Options);
            if (products != null)
            {
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }

        if (!context.Ratings.Any())
        {
            var ratingsJson = await File.ReadAllTextAsync($"..//GreenSphere.Persistence//DataSeed//ratings.json");
            var ratings = JsonSerializer.Deserialize<IEnumerable<Rating>>(ratingsJson, Options);
            if (ratings != null)
            {
                await context.Ratings.AddRangeAsync(ratings);
                await context.SaveChangesAsync();
            }
        }

        if (!context.ProductTranslations.Any())
        {
            var productTranslationsJson = await File.ReadAllTextAsync($"..//GreenSphere.Persistence//DataSeed//productTranslations.json");
            var productTranslations = JsonSerializer.Deserialize<IEnumerable<ProductTranslation>>(productTranslationsJson, Options);
            if (productTranslations != null)
            {
                await context.ProductTranslations.AddRangeAsync(productTranslations);
                await context.SaveChangesAsync();
            }
        }
    }
}