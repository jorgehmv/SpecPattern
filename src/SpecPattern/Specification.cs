#region [ using ]

using System;
using System.Linq.Expressions;
using SpecPattern.Helpers;

#endregion

namespace SpecPattern
{
    public abstract class Specification<T>
    {
        private Expression<Func<T, bool>> predicate;

        public Expression<Func<T, bool>> Predicate
        {
            get { return predicate ?? (predicate = DefinePredicate()); }
        }

        protected abstract Expression<Func<T, bool>> DefinePredicate();

        public bool IsSatisfiedBy(T entity)
        {
            return Predicate.Compile().Invoke(entity);
        }

        public Specification<T> And(Specification<T> specification)
        {
            Assert.ArgumentIsNotNull(specification, "Specification must not be null.");

            return new AndSpecification<T>(this, specification);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            Assert.ArgumentIsNotNull(specification, "Specification must not be null.");

            return new OrSpecification<T>(this, specification);
        }

        public Specification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        public static Specification<T> operator &(
            Specification<T> leftSpecification, Specification<T> rightSpecification)
        {
            Assert.ArgumentIsNotNull(leftSpecification, "Left side specification must not be null.");
            Assert.ArgumentIsNotNull(rightSpecification, "Right side specification must not be null.");

            return leftSpecification.And(rightSpecification);
        }

        public static Specification<T> operator |(
            Specification<T> leftSpecification, Specification<T> rightSpecification)
        {
            Assert.ArgumentIsNotNull(leftSpecification, "Left side specification must not be null.");
            Assert.ArgumentIsNotNull(rightSpecification, "Right side specification must not be null.");

            return leftSpecification.Or(rightSpecification);
        }

        public static Specification<T> operator !(Specification<T> specification)
        {
            Assert.ArgumentIsNotNull(specification, "Specification must not be null.");

            return specification.Not();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Specification<T> x, Specification<T> y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return x.Equals(y);
        }

        public static bool operator !=(Specification<T> x, Specification<T> y)
        {
            return !(x == y);
        }
    }
}