namespace DotNetCore.Objects;

public record Filter
{
    public string Property { get; set; }

    public string Comparison { get; set; }

    public string Value { get; set; }
}
