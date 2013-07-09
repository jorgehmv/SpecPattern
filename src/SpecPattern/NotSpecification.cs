#region [ using ]

using System;
using System.Linq.Expressions;
using SpecPattern.Helpers;

#endregion

namespace SpecPattern
{
    public class NotSpecification<T> : Specification<T>, IEquatable<NotSpecification<T>>
    {
        private readonly Specification<T> specification;

        public NotSpecification(Specification<T> specification)
        {
            Assert.ArgumentIsNotNull(specification, "Specification must not be null.");

            this.specification = specification;
        }

        #region IEquatable<NotSpecification<T>> Members

        public bool Equals(NotSpecification<T> other)
        {
            if (other == null)
                return false;

            return specification == other.specification;
        }

        #endregion

        protected override Expression<Func<T, bool>> DefinePredicate()
        {
            return Expression.Lambda<Func<T, bool>>
                (Expression.Not(specification.Predicate.Body), specification.Predicate.Parameters);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NotSpecification<T>);
        }

        public override int GetHashCode()
        {
            return specification.GetHashCode();
        }

        public static bool operator ==(NotSpecification<T> x, NotSpecification<T> y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return x.Equals(y);
        }

        public static bool operator !=(NotSpecification<T> x, NotSpecification<T> y)
        {
            return !(x == y);
        }
    }
}