namespace GreenSphere.Domain.Utils;

public sealed class CategorySpecParams
{
    private string? _search;

    public string? Search
    {
        get => _search;
        set => _search = value?.ToLower();
    }
}