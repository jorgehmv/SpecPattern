#region [ using ]

using System;
using System.Linq.Expressions;
using SpecPattern.Helpers;

#endregion

namespace SpecPattern
{
    public class AndSpecification<T> : Specification<T>, IEquatable<AndSpecification<T>>
    {
        private readonly Specification<T> leftSide;
        private readonly Specification<T> rightSide;

        public AndSpecification(Specification<T> leftSide, Specification<T> rightSide)
        {
            Assert.ArgumentIsNotNull(leftSide, "Left side specification must not be null.");
            Assert.ArgumentIsNotNull(rightSide, "Right side specification must not be null.");

            this.leftSide = leftSide;
            this.rightSide = rightSide;
        }

        #region IEquatable<AndSpecification<T>> Members

        public bool Equals(AndSpecification<T> other)
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
                (Expression.AndAlso(leftSide.Predicate.Body, invokedExpr), leftSide.Predicate.Parameters);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as AndSpecification<T>);
        }

        public override int GetHashCode()
        {
            return HashCodeProvider.GetHashCode(leftSide, rightSide);
        }

        public static bool operator ==(AndSpecification<T> x, AndSpecification<T> y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return x.Equals(y);
        }

        public static bool operator !=(AndSpecification<T> x, AndSpecification<T> y)
        {
            return !(x == y);
        }
    }
}