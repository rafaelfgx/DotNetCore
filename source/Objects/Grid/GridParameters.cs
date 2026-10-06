namespace DotNetCore.Objects;

public record GridParameters
{
    public Filters Filters { get; set; }

    public Order Order { get; set; }

    public Page Page { get; set; }
}
