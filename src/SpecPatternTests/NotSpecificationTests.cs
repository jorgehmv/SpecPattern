#region [ using ]

using System;
using SpecPattern;
using NUnit.Framework;

#endregion

namespace SpecPatternTests
{
    [TestFixture]
    public class NotSpecificationTests : ValueObjectTests<NotSpecification<object>>
    {
        protected object someEntity = new object();


        protected override NotSpecification<object> CreateValueObjectX()
        {
            return new NotSpecification<object>(SpecificationUtils.CreateSatisfiedSpecification());
        }

        protected override NotSpecification<object> CreateValueObjectY()
        {
            return new NotSpecification<object>(SpecificationUtils.CreateNotSatisfiedSpecification());
        }

        [Test]
        public void ConstructorWithSpecificationNullShouldThrowNullArgumentException()
        {
            AssertUtils.ThrowsWithMessage<ArgumentNullException>(() => new NotSpecification<object>(null),
                                                                 "Specification must not be null.");
        }

        [Test]
        public void IsSatisfiedByWithNotSatisfiedShouldReturnTrue()
        {
            var spec =
                new NotSpecification<object>(SpecificationUtils.CreateNotSatisfiedSpecification());

            bool result = spec.IsSatisfiedBy(someEntity);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsSatisfiedByWithSatisfiedShouldReturnFalse()
        {
            var spec =
                new NotSpecification<object>(SpecificationUtils.CreateSatisfiedSpecification());

            bool result = spec.IsSatisfiedBy(someEntity);

            Assert.IsFalse(result);
        }
    }
}