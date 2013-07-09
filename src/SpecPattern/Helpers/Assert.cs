#region [ using ]

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace SpecPattern.Helpers
{
    internal static class Assert
    {
        public static void ArgumentIsNotNull(object argument,
                                             string message)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(message, null as Exception);
            }
        }

        public static void ArgumentHasText(string argument,
                                           string message)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullException(message, null as Exception);
            }
        }

        public static void ArgumentHasLength(ICollection argument, string message)
        {
            ArgumentIsNotNull(argument, message);

            if (argument.Count == 0)
            {
                throw new ArgumentNullException(message, null as Exception);
            }
        }

        public static void ArgumentHasLength<T>(ICollection<T> argument, string message)
        {
            ArgumentIsNotNull(argument, message);

            if (argument.Count == 0)
            {
                throw new ArgumentNullException(message, null as Exception);
            }
        }

        public static void ArgumentIsGreaterThanOrEqualToLowerBound<T>(IComparable<T> value, T lowerBound,
                                                                       string message)
        {
            if (value.CompareTo(lowerBound) < 0)
            {
                throw new ArgumentOutOfRangeException(message, null as Exception);
            }
        }

        public static void ArgumentIsLowerThanOrEqualToUpperBound<T>(IComparable<T> value, T lowerBound, string message)
        {
            if (value.CompareTo(lowerBound) > 0)
            {
                throw new ArgumentOutOfRangeException(message, null as Exception);
            }
        }

        public static void ArgumentIsGreaterThanLowerBound<T>(IComparable<T> value, T lowerBound, string message)
        {
            if (value.CompareTo(lowerBound) <= 0)
            {
                throw new ArgumentOutOfRangeException(message, null as Exception);
            }
        }

        public static void ArgumentIsLowerThanUpperBound<T>(IComparable<T> value, T lowerBound, string message)
        {
            if (value.CompareTo(lowerBound) >= 0)
            {
                throw new ArgumentOutOfRangeException(message, null as Exception);
            }
        }

        public static void ArgumentSatisfies(bool condition,
                                             string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message);
            }
        }

        public static void State(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void StateIsNotNull(object state,
                                          string message)
        {
            if (state == null)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void StateHasText(string state,
                                        string message)
        {
            if (string.IsNullOrWhiteSpace(state))
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}