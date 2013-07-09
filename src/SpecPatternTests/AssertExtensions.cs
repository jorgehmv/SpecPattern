#region [ using ]

using System;
using NUnit.Framework;

#endregion

namespace SpecPatternTests
{
    public static class AssertUtils
    {
        public static void ThrowsWithMessage<T>(TestDelegate code, string exceptionMessage) where T : Exception
        {
            var exception = Assert.Throws<T>(code);
            Assert.AreEqual(exceptionMessage, exception.Message);
        }

        public static void AreDatesPracticallyEquals(DateTime x, DateTime y)
        {
            TimeSpan span = x - y;

            Assert.Less(span.Seconds, 1, "Dates are more than one second different.");
        }

        public static void AreEqualsWithEpsilon(double expected, double actual, double epsilon)
        {
            Assert.IsTrue(actual - epsilon < expected && expected < actual + epsilon);
        }
    }
}