#region [ using ]

using System;
using SpecPattern;
using NUnit.Framework;

#endregion

namespace SpecPatternTests
{
    [TestFixture]
    public class AndSpecificationTests : ValueObjectTests<AndSpecification<object>>
    {
        protected object someEntity = new object();

        protected override AndSpecification<object> CreateValueObjectX()
        {
            return new AndSpecification<object>(SpecificationUtils.CreateSatisfiedSpecification(),
                                                SpecificationUtils.CreateNotSatisfiedSpecification());
        }

        protected override AndSpecification<object> CreateValueObjectY()
        {
            return new AndSpecification<object>(SpecificationUtils.CreateNotSatisfiedSpecification(),
                                                SpecificationUtils.CreateSatisfiedSpecification());
        }

        [Test]
        public void ConstructorWithLeftSpecificationNullShouldThrowException()
        {
            AssertUtils.ThrowsWithMessage<ArgumentNullException>(() =>
                                                                 new AndSpecification<object>(null,
                                                                                              SpecificationUtils.
                                                                                                  CreateSatisfiedSpecification
                                                                                                  ()),
                                                                 "Left side specification must not be null.");
        }

        [Test]
        public void ConstructorWithRightSpecificationNullShouldThrowException()
        {
            AssertUtils.ThrowsWithMessage<ArgumentNullException>(
                () => new AndSpecification<object>(SpecificationUtils.CreateSatisfiedSpecification(), null),
                "Right side specification must not be null.");
        }

        [Test]
        public void IsSatisfiedByWithNotSatisfiedAndNotSatisfiedShouldReturnFalse()
        {
            var spec =
                new AndSpecification<object>(SpecificationUtils.CreateNotSatisfiedSpecification(),
                                             SpecificationUtils.CreateNotSatisfiedSpecification());

            bool result = spec.IsSatisfiedBy(someEntity);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsSatisfiedByWithNotSatisfiedAndSatisfiedShouldReturnFalse()
        {
            var spec =
                new AndSpecification<object>(SpecificationUtils.CreateNotSatisfiedSpecification(),
                                             SpecificationUtils.CreateSatisfiedSpecification());

            bool result = spec.IsSatisfiedBy(someEntity);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsSatisfiedByWithSatisfiedAndNotSatisfiedShouldReturnFalse()
        {
            var spec =
                new AndSpecification<object>(SpecificationUtils.CreateSatisfiedSpecification(),
                                             SpecificationUtils.CreateNotSatisfiedSpecification());

            bool result = spec.IsSatisfiedBy(someEntity);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsSatisfiedByWithSatisfiedAndSatisfiedShouldReturnTrue()
        {
            var spec =
                new AndSpecification<object>(SpecificationUtils.CreateSatisfiedSpecification(),
                                             SpecificationUtils.CreateSatisfiedSpecification());

            bool result = spec.IsSatisfiedBy(someEntity);

            Assert.IsTrue(result);
        }
    }
}