# DotNetCore.Domain

## Entity

```cs
public abstract class Entity<TId> : IEquatable<Entity<TId>>
{
    protected Entity() { }

    protected Entity(TId id) { }

    public TId Id { get; }

    public static bool operator !=(Entity<TId> a, Entity<TId> b) { }

    public static bool operator ==(Entity<TId> a, Entity<TId> b) { }

    public sealed override bool Equals(object obj) { }

    public bool Equals(Entity<TId> other) { }

    public sealed override int GetHashCode() { }
}
```

## Event

```cs
public abstract class Event
{
    public Guid Id { get; } = Guid.NewGuid();

    public DateTime DateTime { get; } = DateTime.UtcNow;
}
```

## ValueObject

```cs
public abstract class ValueObject
{
    public static bool operator !=(ValueObject a, ValueObject b) { }

    public static bool operator ==(ValueObject a, ValueObject b) { }

    public sealed override bool Equals(object obj) { }

    public sealed override int GetHashCode() { }

    protected abstract IEnumerable<object> GetEquals();
}
```

```cs
public abstract class ValueObject<T> : ValueObject
{
    protected ValueObject(T value) { }

    public T Value { get; }

    protected sealed override IEnumerable<object> GetEquals() { }
}
```
