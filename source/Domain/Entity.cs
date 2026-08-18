namespace DotNetCore.Domain;

public abstract class Entity
{
    public long Id { get; init; }

    public static bool operator !=(Entity left, Entity right) => !(left == right);

    public static bool operator ==(Entity left, Entity right) => left?.Equals(right) == true;

    public override bool Equals(object obj) => obj is Entity entity && (ReferenceEquals(this, entity) || Id == entity.Id);

    public override int GetHashCode() => Id.GetHashCode();
}
