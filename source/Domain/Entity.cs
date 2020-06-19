using System;

namespace DotNetCore.Domain
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        protected Entity() { }

        protected Entity(TId id)
        {
            Id = id;
        }

        public TId Id { get; }

        public static bool operator !=(Entity<TId> a, Entity<TId> b)
        {
            return !(a == b);
        }

        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(obj as Entity<TId>);
        }

        public bool Equals(Entity<TId> other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public sealed override int GetHashCode()
        {
            unchecked
            {
                return (GetType().GetHashCode() * 97) ^ Id.GetHashCode();
            }
        }
    }
}
