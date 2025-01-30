using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace GreenSphere.Api.Localization;

public class JsonStringLocalizerFactory(IDistributedCache cache) : IStringLocalizerFactory
{
    public IStringLocalizer Create(Type resourceSource)
    {
        return new JsonStringLocalizer(cache, GetLogger());
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return new JsonStringLocalizer(cache, GetLogger());
    }

    private static ILogger<JsonStringLocalizer> GetLogger()
    {
        var loggerFactory = new LoggerFactory();
        return loggerFactory.CreateLogger<JsonStringLocalizer>();
    }
}