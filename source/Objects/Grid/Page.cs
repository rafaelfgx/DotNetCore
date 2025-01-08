namespace DotNetCore.Objects;

public record Page
{
    public int Index { get; set; } = 1;

    public int Size { get; set; }
}
