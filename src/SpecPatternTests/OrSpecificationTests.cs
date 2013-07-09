#region [ using ]

using System;
using SpecPattern;
using NUnit.Framework;

#endregion

namespace SpecPatternTests
{
    [TestFixture]
    public class OrSpecificationTests : ValueObjectTests<OrSpecification<object>>
    {
        protected object someEntity = new object();

        protected override OrSpecification<object> CreateValueObjectX()
        {
            return new OrSpecification<object>(SpecificationUtils.CreateSatisfiedSpecification(),
                                               SpecificationUtils.CreateNotSatisfiedSpecification());
        }

        protected override OrSpecification<object> CreateValueObjectY()
        {
            return new OrSpecification<object>(SpecificationUtils.CreateNotSatisfiedSpecification(),
                                               SpecificationUtils.CreateSatisfiedSpecification());
        }

        [Test]
        public void ConstructorWithLeftSpecificationNullShouldThrowNullArgumentException()
        {
            AssertUtils.ThrowsWithMessage<ArgumentNullException>(
                () => new OrSpecification<object>(null, SpecificationUtils.CreateSatisfiedSpecification()),
                "Left side specification must not be null.");
        }

        [Test]
        public void ConstructorWithRightSpecificationNullShouldThrowNullArgumentException()
        {
            AssertUtils.ThrowsWithMessage<ArgumentNullException>(
                () => new OrSpecification<object>(SpecificationUtils.CreateSatisfiedSpecification(), null),
                "Right side specification must not be null.");
        }

        [Test]
        public void IsSatisfiedByWithNotSatisfiedAndNotSatisfiedShouldReturnFalse()
        {
            var spec =
                new OrSpecification<object>(SpecificationUtils.CreateNotSatisfiedSpecification(),
                                            SpecificationUtils.CreateNotSatisfiedSpecification());

            bool result = spec.IsSatisfiedBy(someEntity);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsSatisfiedByWithNotSatisfiedAndSatisfiedShouldReturnTrue()
        {
            var spec =
                new OrSpecification<object>(SpecificationUtils.CreateNotSatisfiedSpecification(),
                                            SpecificationUtils.CreateSatisfiedSpecification());

            bool result = spec.IsSatisfiedBy(someEntity);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsSatisfiedByWithSatisfiedAndNotSatisfiedShouldReturnTrue()
        {
            var spec = new OrSpecification<object>(
                SpecificationUtils.CreateSatisfiedSpecification(), SpecificationUtils.CreateNotSatisfiedSpecification());

            bool result = spec.IsSatisfiedBy(someEntity);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsSatisfiedByWithSatisfiedAndSatisfiedShouldReturnTrue()
        {
            var spec = new OrSpecification<object>(
                SpecificationUtils.CreateSatisfiedSpecification(), SpecificationUtils.CreateSatisfiedSpecification());

            bool result = spec.IsSatisfiedBy(someEntity);

            Assert.IsTrue(result);
        }
    }
}