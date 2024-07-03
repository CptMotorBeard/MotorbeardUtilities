using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Object = UnityEngine.Object;

namespace BeardKit
{
    public static class Assert
    {
        /// <summary>If the debugger is attached, break on failure will cause a debug break.</summary>
        public static bool BreakOnFail { get; set; } = true;

        /// <summary>
        /// This assert always fails. Is stripped from compilation unless the 'DEBUG' symbol is defined.
        /// </summary>
        /// <param name="userMessage">String message to be logged to the console with this assert.</param>
        [Conditional("DEBUG")]
        public static void Fail(in string userMessage = null,
            [CallerMemberName] string membName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Fail(null, userMessage, membName, filePath, lineNumber);
        }

        /// <summary>
        /// Checks if a condition is true and asserts if it is not. Is stripped from compilation unless the 'DEBUG' symbol is
        /// defined.
        /// </summary>
        /// <param name="condition">Condition to check if it is true.</param>
        /// <param name="userMessage">String message to be logged to the console with this assert.</param>
        [Conditional("DEBUG")]
        public static void IsTrue(in bool condition, in string userMessage = null,
            [CallerMemberName] string membName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (!condition)
            {
                Fail(AssertionMessageUtil.BooleanFailureMessage(true), userMessage, membName, filePath, lineNumber);
            }
        }

        /// <summary>
        /// Checks if a condition is false and asserts if it is not. Is stripped from compilation unless the 'DEBUG' symbol is
        /// defined.
        /// </summary>
        /// <param name="condition">Condition to check if it is false.</param>
        /// <param name="userMessage">String message to be logged to the console with this assert.</param>
        [Conditional("DEBUG")]
        public static void IsFalse(in bool condition, in string userMessage = null,
            [CallerMemberName] string membName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (condition)
            {
                Fail(AssertionMessageUtil.BooleanFailureMessage(false), userMessage, membName, filePath, lineNumber);
            }
        }

        /// <summary>
        /// Checks if a value is null and asserts if it is not. Is stripped from compilation unless the 'DEBUG' symbol is defined.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="userMessage">String message to be logged to the console with this assert.</param>
        [Conditional("DEBUG")]
        public static void IsNull<T>(in T value, in string userMessage = null,
            [CallerMemberName] string membName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (typeof(Object).IsAssignableFrom(typeof(T)))
            {
                IsNull(value as Object, userMessage, membName, filePath, lineNumber);
            }
            else if (value != null)
            {
                Fail(AssertionMessageUtil.NullFailureMessage(true), userMessage, membName, filePath, lineNumber);
            }
        }

        [Conditional("DEBUG")]
        private static void IsNull(in Object value, in string userMessage = null, string membName = "", string filePath = "",
            int lineNumber = 0)
        {
            if (value != null)
            {
                Fail(AssertionMessageUtil.NullFailureMessage(true), userMessage, membName, filePath, lineNumber);
            }
        }

        /// <summary>
        /// Checks if a value is not null and asserts if it is. Is stripped from compilation unless the 'DEBUG' symbol is defined.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="userMessage">String message to be logged to the console with this assert.</param>
        [Conditional("DEBUG")]
        public static void IsNotNull<T>(in T value, in string userMessage = null,
            [CallerMemberName] string membName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (typeof(Object).IsAssignableFrom(typeof(T)))
            {
                IsNotNull(value as Object, userMessage, membName, filePath, lineNumber);
            }
            else if (value == null)
            {
                Fail(AssertionMessageUtil.NullFailureMessage(false), userMessage, membName, filePath, lineNumber);
            }
        }

        [Conditional("DEBUG")]
        private static void IsNotNull(in Object value, in string userMessage = null, string membName = "", string filePath = "",
            int lineNumber = 0)
        {
            if (value == null)
            {
                Fail(AssertionMessageUtil.NullFailureMessage(false), userMessage, membName, filePath, lineNumber);
            }
        }

        [Conditional("DEBUG")]
        private static void Fail(in string message, in string userMessage = null, string membName = "", string filePath = "",
            int lineNumber = 0)
        {
            if (BreakOnFail)
            {
                Debugger.Break();
            }

            string logMessage = message;
            if (string.IsNullOrEmpty(message))
            {
                logMessage = "Assertion has failed";
            }

            if (!string.IsNullOrEmpty(userMessage))
            {
                logMessage = $"{userMessage}{Environment.NewLine}{logMessage}";
            }

            logMessage = $"{logMessage}{Environment.NewLine}{filePath}::{membName}::{lineNumber}{Environment.NewLine}";
            DebugLogger.LogAssertion(logMessage);
        }

        private static class AssertionMessageUtil
        {
            internal static string GetMessage(in string failureMessage)
            {
                return $"Assertion failure: {failureMessage}";
            }

            internal static string GetMessage(in string failureMessage, in string expected)
            {
                return $"Assertion failure: {failureMessage}{Environment.NewLine}We expected: {expected}";
            }

            internal static string GetEqualityMessage(in object actual, in object expected, in bool expectEqual)
            {
                return GetMessage($"Values are {(expectEqual ? "not " : "")}equal", $"{actual} {(expectEqual ? "==" : "!=")} {expected}");
            }

            internal static string NullFailureMessage(in bool expectNull)
            {
                return GetMessage($"Value was {(expectNull ? "not " : "")}Null");
            }

            internal static string BooleanFailureMessage(in bool expected)
            {
                return GetMessage($"Value was {!expected}", expected.ToString());
            }
        }
    }
}