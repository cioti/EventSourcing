using System;
using System.Linq.Expressions;

namespace EventSourcing
{
    /// <summary>
    /// Creates a new aggregate instance using the default constructor.
    /// </summary>
    public static class AggregateFactory<T>
    {
        private static readonly Func<T> _constructor = CreateTypeConstructor();

        public static T CreateAggregate()
        {
            if (_constructor == null)
                throw new AggregateDefaultConstructorMissingException(typeof(T));

            return _constructor();
        }

        private static Func<T> CreateTypeConstructor()
        {
            try
            {
                var expr = Expression.New(typeof(T));

                var func = Expression.Lambda<Func<T>>(expr);

                return func.Compile();
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}
