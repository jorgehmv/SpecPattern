#region [ using ]

using System;
using SpecPattern;
using NUnit.Framework;

#endregion

namespace SpecPatternTests
{
    [TestFixture]
    public class SpecificationTests
    {
        [Test]
        public void AndMethodWithNullArgumentShouldThrowArgumentNullException()
        {
            Specification<object> spec1 = SpecificationUtils.CreateSatisfiedSpecification();
            Specification<object> spec2 = null;

            AssertUtils.ThrowsWithMessage<ArgumentNullException>(() => spec1.And(spec2), "Specification must not be null.");
        }

        [Test]
        public void AndMethodWithValidArgumentsShouldReturnAndSpecification()
        {
            Specification<object> spec1 = SpecificationUtils.CreateSatisfiedSpecification();
            Specification<object> spec2 = SpecificationUtils.CreateSatisfiedSpecification();
            Specification<object> expectedResult = new AndSpecification<object>(spec1, spec2);

            Specification<object> result = spec1.And(spec2);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void AndOperatorWithNullLeftArgumentShouldThrowArgumentNullException()
        {
            Specification<object> spec1 = null;
            Specification<object> spec2 = SpecificationUtils.CreateSatisfiedSpecification();
            Specification<object> result = null;

            AssertUtils.ThrowsWithMessage<ArgumentNullException>(() => result = spec1 & spec2,
                                                                 "Left side specification must not be null.");
        }

        [Test]
        public void AndOperatorWithNullRightArgumentShouldThrowArgumentNullException()
        {
            Specification<object> spec1 = SpecificationUtils.CreateSatisfiedSpecification();
            Specification<object> spec2 = null;
            Specification<object> result = null;

            AssertUtils.ThrowsWithMessage<ArgumentNullException>(() => result = spec1 & spec2,
                                                                 "Right side specification must not be null.");
        }

        [Test]
        public void NotMethodWithCalledShouldReturnNotSpecification()
        {
            Specification<object> spec = SpecificationUtils.CreateSatisfiedSpecification();
            Specification<object> expectedResult = new NotSpecification<object>(spec);

            Specification<object> result = spec.Not();

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void NotOperatorWithNullArgumentShouldThrowArgumentNullException()
        {
            Specification<object> spec = null;
            Specification<object> result = null;

            AssertUtils.ThrowsWithMessage<ArgumentNullException>(() => result = !spec, "Specification must not be null.");
        }

        [Test]
        public void OrMethodWithNullArgumentShouldThrowArgumentNullException()
        {
            Specification<object> spec1 = SpecificationUtils.CreateSatisfiedSpecification();
            Specification<object> spec2 = null;

            AssertUtils.ThrowsWithMessage<ArgumentNullException>(() => spec1.Or(spec2), "Specification must not be null.");
        }

        [Test]
        public void OrMethodWithValidArgumentsShouldReturnOrSpecification()
        {
            Specification<object> spec1 = SpecificationUtils.CreateSatisfiedSpecification();
            Specification<object> spec2 = SpecificationUtils.CreateSatisfiedSpecification();
            Specification<object> expectedResult = new OrSpecification<object>(spec1, spec2);

            Specification<object> result = spec1.Or(spec2);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void OrOperatorWithNullLeftArgumentShouldThrowArgumentNullException()
        {
            Specification<object> spec1 = null;
            Specification<object> spec2 = SpecificationUtils.CreateSatisfiedSpecification();
            Specification<object> result = null;

            AssertUtils.ThrowsWithMessage<ArgumentNullException>(() => result = spec1 | spec2,
                                                                 "Left side specification must not be null.");
        }

        [Test]
        public void OrOperatorWithNullRightArgumentShouldThrowArgumentNullException()
        {
            Specification<object> spec1 = SpecificationUtils.CreateSatisfiedSpecification();
            Specification<object> spec2 = null;
            Specification<object> result = null;

            AssertUtils.ThrowsWithMessage<ArgumentNullException>(() => result = spec1 | spec2,
                                                                 "Right side specification must not be null.");
        }
    }
}