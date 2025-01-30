namespace GreenSphere.Api.Extensions.Localization;

public static class LocalizationExtensions
{
    public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
    {
        var supportedCultures = new[] { "en-US", "ar-EG" };
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);

        return app;
    }
}
