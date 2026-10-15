namespace DotNetCore.Objects;

public record Order
{
    public bool Ascending { get; set; } = true;

    public string Property { get; set; }
}
