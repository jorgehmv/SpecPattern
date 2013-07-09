#region [ using ]

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace SpecPatternTests
{
    [TestFixture]
    public abstract class ValueObjectTests<T> where T : class, IEquatable<T>
    {
        /// <summary>
        /// 	Creates an instance of T distinct to CreateValueObjectY() one
        /// </summary>
        /// <returns>instance of T</returns>
        protected abstract T CreateValueObjectX();

        /// <summary>
        /// 	Creates an instance of T distinct to CreateValueObjectX() one
        /// </summary>
        /// <returns>instance of T</returns>
        protected abstract T CreateValueObjectY();

        protected bool EqualityOperator(T x, T y)
        {
            return (bool) typeof (T).GetMethod("op_Equality", new[] {typeof (T), typeof (T)})
                              .Invoke(null, new object[] {x, y});
        }

        protected bool InequalityOperator(T x, T y)
        {
            return (bool) typeof (T).GetMethod("op_Inequality", new[] {typeof (T), typeof (T)})
                              .Invoke(null, new object[] {x, y});
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// 	Althoug it is possible for two distinct objects to have the same hashcode it is pretty unlikely, so if it happens it is probably a bug
        /// </remarks>
        [Test]
        public void DistinctObjectsHaveDistinctHashCode()
        {
            T x = CreateValueObjectX();
            T y = CreateValueObjectY();

            Assert.AreNotEqual(x.GetHashCode(), y.GetHashCode());
        }

        [Test]
        public void EqualObjectsHaveSameHashCode()
        {
            T x = CreateValueObjectX();
            T y = CreateValueObjectX();

            Assert.AreEqual(x.GetHashCode(), y.GetHashCode());
        }

        [Test]
        public void EqualsIsCommutativeMethodTest()
        {
            T x = CreateValueObjectX();
            T y = CreateValueObjectX();

            Assert.That(EqualityComparer<T>.Default.Equals(x, y) && EqualityComparer<T>.Default.Equals(y, x));
        }

        [Test]
        public void EqualsIsCommutativeOperatorTest()
        {
            T x = CreateValueObjectX();
            T y = CreateValueObjectX();

            Assert.That(EqualityOperator(x, y) && EqualityOperator(y, x));
        }

        [Test]
        public void EqualsIsTransitiveMethodTest()
        {
            T x = CreateValueObjectX();
            T y = CreateValueObjectX();
            T z = CreateValueObjectX();

            Assert.That(EqualityComparer<T>.Default.Equals(x, y) && EqualityComparer<T>.Default.Equals(y, z) &&
                        EqualityComparer<T>.Default.Equals(x, z));
        }

        [Test]
        public void EqualsIsTransitiveOperatorTest()
        {
            T x = CreateValueObjectX();
            T y = CreateValueObjectX();
            T z = CreateValueObjectX();

            Assert.That(EqualityOperator(x, y) && EqualityOperator(y, z) && EqualityOperator(x, z));
        }

        [Test]
        public void InequalityIsTheOppositeToEqualityOperatorTest()
        {
            T x = CreateValueObjectX();
            T y = CreateValueObjectY();

            Assert.AreNotEqual(EqualityOperator(x, y), InequalityOperator(x, y));
            Assert.AreNotEqual(EqualityOperator(y, x), InequalityOperator(y, x));
            Assert.AreNotEqual(EqualityOperator(x, x), InequalityOperator(x, x));
            Assert.AreNotEqual(EqualityOperator(y, y), InequalityOperator(y, y));
        }

        [Test]
        public void NullEqualsNullMethodTest()
        {
            T x = null;
            T y = null;

            Assert.That(EqualityComparer<T>.Default.Equals(x, y));
        }

        [Test]
        public void NullEqualsNullOperatorTest()
        {
            T x = null;
            T y = null;

            Assert.That(EqualityOperator(x, y));
        }

        [Test]
        public void ObjectEqualsItselfMethodTest()
        {
            T x = CreateValueObjectX();

            Assert.That(EqualityComparer<T>.Default.Equals(x, x));
        }

        [Test]
        public void ObjectEqualsItselfOperatorTest()
        {
            T x = CreateValueObjectX();
            Assert.That(EqualityOperator(x, x));
        }

        [Test]
        public void ObjectNotEqualsNullMethodTest()
        {
            T x = CreateValueObjectX();
            T y = null;

            Assert.That(!EqualityComparer<T>.Default.Equals(x, y));
        }

        [Test]
        public void ObjectNotEqualsNullOperatorTest()
        {
            T x = CreateValueObjectX();
            T y = null;

            Assert.That(InequalityOperator(x, y));
        }
    }
}