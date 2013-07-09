using System;
using System.Linq.Expressions;

namespace SpecPattern
{
    public class NullSpecification<T> : Specification<T>
    {
        private readonly bool satisfies;

        protected override Expression<Func<T, bool>> DefinePredicate()
        {
            return c => satisfies;
        }

        public NullSpecification(bool satisfies)
        {
            this.satisfies = satisfies;
        }
    }
}
