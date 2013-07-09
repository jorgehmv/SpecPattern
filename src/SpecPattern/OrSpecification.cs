#region [ using ]

using System;
using System.Linq.Expressions;
using SpecPattern.Helpers;

#endregion

namespace SpecPattern
{
    public class OrSpecification<T> : Specification<T>, IEquatable<OrSpecification<T>>
    {
        private readonly Specification<T> leftSide;
        private readonly Specification<T> rightSide;

        public OrSpecification(Specification<T> leftSide, Specification<T> rightSide)
        {
            Assert.ArgumentIsNotNull(leftSide, "Left side specification must not be null.");
            Assert.ArgumentIsNotNull(rightSide, "Right side specification must not be null.");

            this.leftSide = leftSide;
            this.rightSide = rightSide;
        }

        #region IEquatable<OrSpecification<T>> Members

        public bool Equals(OrSpecification<T> other)
        {
            if (other == null)
                return false;

            return leftSide == other.leftSide &&
                   rightSide == other.rightSide;
        }

        #endregion

        protected override Expression<Func<T, bool>> DefinePredicate()
        {
            var invokedExpr = Expression.Invoke(rightSide.Predicate, leftSide.Predicate.Parameters);
            return Expression.Lambda<Func<T, bool>>
                (Expression.OrElse(leftSide.Predicate.Body, invokedExpr), leftSide.Predicate.Parameters);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as OrSpecification<T>);
        }

        public override int GetHashCode()
        {
            return HashCodeProvider.GetHashCode(leftSide, rightSide);
        }

        public static bool operator ==(OrSpecification<T> x, OrSpecification<T> y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return x.Equals(y);
        }

        public static bool operator !=(OrSpecification<T> x, OrSpecification<T> y)
        {
            return !(x == y);
        }
    }
}