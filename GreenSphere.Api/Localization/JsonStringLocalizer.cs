﻿using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Globalization;

namespace GreenSphere.Api.Localization;

public class JsonStringLocalizer(
    IDistributedCache cache,
    ILogger<JsonStringLocalizer> logger) : IStringLocalizer
{
    private readonly JsonSerializer _serializer = new();

    public LocalizedString this[string name]
    {
        get
        {
            var value = GetString(name);
            return new LocalizedString(name, value);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var actualValue = this[name];
            return !actualValue.ResourceNotFound
                ? new LocalizedString(name, string.Format(actualValue.Value, arguments))
                : actualValue;
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        var filePath = $"Resources/{CultureInfo.CurrentCulture.Name}.json";

        using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using StreamReader streamReader = new(stream);
        using JsonTextReader reader = new(streamReader);

        while (reader.Read())
        {
            if (reader.TokenType != JsonToken.PropertyName)
                continue;

            var key = reader.Value as string;
            reader.Read();
            var value = _serializer.Deserialize<string>(reader);
            yield return new LocalizedString(key, value);
        }
    }

    private string GetString(string key)
    {
        var filePath = $"Resources/{CultureInfo.CurrentCulture.Name}.json";
        var fullFilePath = Path.GetFullPath(filePath);

        logger.LogInformation($"Reading JSON file from {fullFilePath}");

        if (File.Exists(fullFilePath))
        {
            var cacheKey = $"locale_{Thread.CurrentThread.CurrentCulture.Name}_{key}";
            var cacheValue = cache.GetString(cacheKey);

            if (!string.IsNullOrEmpty(cacheValue))
                return cacheValue;

            var result = GetValueFromJSON(key, fullFilePath);

            if (!string.IsNullOrEmpty(result))
                cache.SetString(cacheKey, result);

            return result;
        }

        return string.Empty;
    }

    private string GetValueFromJSON(string propertyName, string filePath)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(filePath))
                return string.Empty;

            using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new(stream);
            using JsonTextReader reader = new(streamReader);

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName && reader.Value as string == propertyName)
                {
                    reader.Read();
                    return _serializer.Deserialize<string>(reader)!;
                }
            }

            return string.Empty;
        }
        catch (JsonReaderException ex)
        {
            logger.LogError($"Error reading JSON at line {ex.LineNumber}, position {ex.LinePosition}: {ex.Message}");
            return string.Empty;
        }
    }
}