#region [ using ]

using System;
using System.Linq.Expressions;
using SpecPattern;

#endregion

namespace SpecPatternTests
{
    public class StubSpecification : Specification<object>, IEquatable<StubSpecification>
    {
        private readonly bool isSatisfied;

        public StubSpecification(bool isSatisfied)
        {
            this.isSatisfied = isSatisfied;
        }

        #region IEquatable<StubSpecification> Members

        public bool Equals(StubSpecification other)
        {
            if (other == null)
                return false;

            return isSatisfied == other.isSatisfied;
        }

        #endregion

        protected override Expression<Func<object, bool>> DefinePredicate()
        {
            return p => isSatisfied;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as StubSpecification);
        }

        public override int GetHashCode()
        {
            return isSatisfied.GetHashCode();
        }

        public static bool operator ==(StubSpecification x, StubSpecification y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return x.Equals(y);
        }

        public static bool operator !=(StubSpecification x, StubSpecification y)
        {
            return !(x == y);
        }
    }
}