using System.Collections.Generic;

namespace DotNetCore.Domain
{
    public abstract class ValueObject : Base<ValueObject> { }

    public abstract class ValueObject<T> : ValueObject
    {
        protected ValueObject(T value)
        {
            Value = value;
        }

        public T Value { get; }

        protected sealed override IEnumerable<object> Equals()
        {
            yield return Value;
        }
    }
}
