#region [ using ]

using SpecPattern;

#endregion

namespace SpecPatternTests
{
    public static class SpecificationUtils
    {
        public static Specification<object> CreateSatisfiedSpecification()
        {
            return CreateSpecification(true);
        }

        public static Specification<object> CreateNotSatisfiedSpecification()
        {
            return CreateSpecification(false);
        }

        public static Specification<object> CreateSpecification(bool isSatisfied)
        {
            return new StubSpecification(isSatisfied);
        }
    }
}