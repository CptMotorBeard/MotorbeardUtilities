using System;
using System.Diagnostics;

namespace MotorbeardUtilities
{
    public static class Assert
    {
        public static bool BreakOnFail = true;

        public static void Fail(in string userMessage)
        {
            Fail(null, userMessage);
        }

        public static void IsTrue(in bool condition)
        {
            IsTrue(condition, null);
        }

        public static void IsTrue(in bool condition, in string userMessage)
        {
            if (!condition)
            {
                Fail(AssertionMessageUtil.BooleanFailureMessage(expected: true), userMessage);
            }
        }

        public static void IsFalse(in bool condition)
        {
            IsFalse(condition, null);
        }

        public static void IsFalse(in bool condition, in string userMessage)
        {
            if (condition)
            {
                Fail(AssertionMessageUtil.BooleanFailureMessage(expected: false), userMessage);
            }
        }

        public static void IsNull<T>(in T value)
        {
            IsNull(value, null);
        }

        public static void IsNull<T>(in T value, in string userMessage)
        {
            if (typeof(UnityEngine.Object).IsAssignableFrom(typeof(T)))
            {
                IsNull(value as UnityEngine.Object, userMessage);
            }
            else if (value != null)
            {
                Fail(AssertionMessageUtil.NullFailureMessage(value, expectNull: true), userMessage);
            }
        }

        public static void IsNull(in UnityEngine.Object value, in string userMessage)
        {
            if (value != null)
            {
                Fail(AssertionMessageUtil.NullFailureMessage(value, expectNull: true), userMessage);
            }
        }

        public static void IsNotNull<T>(in T value)
        {
            IsNotNull(value, null);
        }

        public static void IsNotNull<T>(in T value, in string userMessage)
        {
            if (typeof(UnityEngine.Object).IsAssignableFrom(typeof(T)))
            {
                IsNotNull(value as UnityEngine.Object, userMessage);
            }
            else if (value == null)
            {
                Fail(AssertionMessageUtil.NullFailureMessage(value, expectNull: false), userMessage);
            }
        }

        public static void IsNotNull(in UnityEngine.Object value, in string userMessage)
        {
            if (value == null)
            {
                Fail(AssertionMessageUtil.NullFailureMessage(value, expectNull: false), userMessage);
            }
        }

        private static void Fail(in string message, in string userMessage)
        {
            if (BreakOnFail)
            {
                Debugger.Break();
            }

            string logMessage = string.Empty;
            if (string.IsNullOrEmpty(message))
            {
                logMessage = "Assertion has failed\n";
            }

            if (!string.IsNullOrEmpty(userMessage))
            {
                logMessage = $"{userMessage}\n{message}";
            }

            DebugLogger.LogAssertion(message);
        }

        private static class AssertionMessageUtil
        {
            public static string GetMessage(in string failureMessage)
            {
                return $"Assertion failure: {failureMessage}";
            }

            public static string GetMessage(in string failureMessage, in string expected)
            {
                return $"Assertion failure: {failureMessage}{Environment.NewLine}Expected: {expected}";
            }

            public static string GetEqualityMessage(in object actual, in object expected, in bool expectEqual)
            {
                return GetMessage($"Values are {(expectEqual ? "not " : "")}equal", $"{actual} {(expectEqual ? "==" : "!=")} {expected}");
            }

            public static string NullFailureMessage(in object value, in bool expectNull)
            {
                return GetMessage($"Value was {(expectNull ? "not " : "")}Null", $"{value} was {(expectNull ? "" : "not ")}Null");
            }

            public static string BooleanFailureMessage(in bool expected)
            {
                return GetMessage($"Value was {!expected}", expected.ToString());
            }
        }
    }
}