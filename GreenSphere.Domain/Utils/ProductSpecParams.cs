namespace GreenSphere.Domain.Utils;

public sealed class ProductSpecParams
{
    private string? _search;

    public string? Search
    {
        get => _search;
        set => _search = value?.ToLower();
    }
}