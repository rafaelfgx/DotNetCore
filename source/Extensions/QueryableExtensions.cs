using System;
using System.Linq;
using System.Linq.Expressions;

namespace DotNetCore.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, string property, object value)
        {
            return queryable.Filter(property, string.Empty, value);
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, string property, string comparison, object value)
        {
            if (string.IsNullOrWhiteSpace(property) || value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return queryable;
            }

            var parameter = Expression.Parameter(typeof(T));

            var left = Create(property, parameter);

            try
            {
                value = Change(value, left.Type);
            }
            catch
            {
                return Enumerable.Empty<T>().AsQueryable();
            }

            var right = Expression.Constant(value, left.Type);

            var body = Create(left, comparison, right);

            var expression = Expression.Lambda<Func<T, bool>>(body, parameter);

            return queryable.Where(expression);
        }

        public static IQueryable<T> Order<T>(this IQueryable<T> queryable, string property, bool ascending)
        {
            if (queryable == null || string.IsNullOrWhiteSpace(property))
            {
                return queryable;
            }

            var parameter = Expression.Parameter(typeof(T));

            var body = Create(property, parameter);

            var expression = (dynamic)Expression.Lambda(body, parameter);

            return ascending ? Queryable.OrderBy(queryable, expression) : Queryable.OrderByDescending(queryable, expression);
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int index, int size)
        {
            if (queryable == null || index <= 0 || size <= 0)
            {
                return queryable;
            }

            return queryable.Skip((index - 1) * size).Take(size);
        }

        private static object Change(object value, Type type)
        {
            if (type.BaseType == typeof(Enum))
            {
                value = Enum.Parse(type, value.ToString());
            }

            return Convert.ChangeType(value, type);
        }

        private static Expression Create(string property, ParameterExpression parameter)
        {
            return property.Split('.').Aggregate((Expression)parameter, Expression.Property);
        }

        private static Expression Create(Expression left, string comparison, ConstantExpression right)
        {
            if (string.IsNullOrWhiteSpace(comparison) && left.Type == typeof(string))
            {
                return Expression.Call(left, nameof(String.Contains), Type.EmptyTypes, right);
            }

            var type = comparison switch
            {
                "<" => ExpressionType.LessThan,
                "<=" => ExpressionType.LessThanOrEqual,
                ">" => ExpressionType.GreaterThan,
                ">=" => ExpressionType.GreaterThanOrEqual,
                "!=" => ExpressionType.NotEqual,
                _ => ExpressionType.Equal
            };

            return Expression.MakeBinary(type, left, right);
        }
    }
}
