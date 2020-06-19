using System.Collections.Generic;
using System.Linq;

namespace DotNetCore.Domain
{
    public abstract class ValueObject
    {
        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }

        public static bool operator ==(ValueObject a, ValueObject b)
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
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return GetEquals().SequenceEqual(((ValueObject)obj).GetEquals());
        }

        public sealed override int GetHashCode()
        {
            return GetEquals().Aggregate(0, (a, b) => (a * 97) + b.GetHashCode());
        }

        protected abstract IEnumerable<object> GetEquals();
    }

    public abstract class ValueObject<T> : ValueObject
    {
        protected ValueObject(T value)
        {
            Value = value;
        }

        public T Value { get; }

        protected sealed override IEnumerable<object> GetEquals()
        {
            yield return Value;
        }
    }
}
