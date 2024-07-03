using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace BeardKit
{
    public static class DebugLogger
    {
        /// <summary>
        /// Logs a message to the Unity Console. Is stripped from compilation unless the 'DEBUG' symbol is defined.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("DEBUG")]
        public static void Log(object message)
        {
            Debug.Log(message);
        }

        /// <summary>
        /// Logs a message to the Unity Console. Is stripped from compilation unless the 'DEBUG' symbol is defined.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("DEBUG")]
        public static void Log(object message, Object context)
        {
            Debug.Log(message, context);
        }

        /// <summary>
        /// Logs a formatted message to the Unity Console. Is stripped from compilation unless the 'DEBUG' symbol is defined.
        /// </summary>
        /// <param name="logType">Type of message e.g. warn or error etc.</param>
        /// <param name="logOptions">Option flags to treat the log message special.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        [Conditional("DEBUG")]
        public static void LogFormat(string format, params object[] args)
        {
            Debug.LogFormat(format, args);
        }

        /// <summary>
        /// Logs a formatted message to the Unity Console. Is stripped from compilation unless the 'DEBUG' symbol is defined.
        /// </summary>
        /// <param name="logType">Type of message e.g. warn or error etc.</param>
        /// <param name="logOptions">Option flags to treat the log message special.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        [Conditional("DEBUG")]
        public static void LogFormat(Object context, string format, params object[] args)
        {
            Debug.LogFormat(context, format, args);
        }

        /// <summary>
        /// Logs a formatted message to the Unity Console. Is stripped from compilation unless the 'DEBUG' symbol is defined.
        /// </summary>
        /// <param name="logType">Type of message e.g. warn or error etc.</param>
        /// <param name="logOptions">Option flags to treat the log message special.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        [Conditional("DEBUG")]
        public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
        {
            Debug.LogFormat(logType, logOptions, context, format, args);
        }

        /// <summary>
        /// A variant of Debug.Log that logs an error message to the console. Is stripped from compilation unless the 'DEBUG'
        /// symbol is defined.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("DEBUG")]
        public static void LogError(object message)
        {
            Debug.LogError(message);
        }

        /// <summary>
        /// A variant of Debug.Log that logs an error message to the console. Is stripped from compilation unless the 'DEBUG'
        /// symbol is defined.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("DEBUG")]
        public static void LogError(object message, Object context)
        {
            Debug.LogError(message, context);
        }

        /// <summary>
        /// Logs a formatted error message to the Unity console. Is stripped from compilation unless the 'DEBUG' symbol is defined.
        /// </summary>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        [Conditional("DEBUG")]
        public static void LogErrorFormat(string format, params object[] args)
        {
            Debug.LogErrorFormat(format, args);
        }

        /// <summary>
        /// Logs a formatted error message to the Unity console. Is stripped from compilation unless the 'DEBUG' symbol is defined.
        /// </summary>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        [Conditional("DEBUG")]
        public static void LogErrorFormat(Object context, string format, params object[] args)
        {
            Debug.LogErrorFormat(context, format, args);
        }

        /// <summary>
        /// A variant of Debug.Log that logs an error message to the console. Is stripped from compilation unless the 'DEBUG'
        /// symbol is defined.
        /// </summary>
        /// <param name="exception">Runtime Exception.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("DEBUG")]
        public static void LogException(Exception exception)
        {
            Debug.LogException(exception);
        }

        /// <summary>
        /// A variant of Debug.Log that logs an error message to the console. Is stripped from compilation unless the 'DEBUG'
        /// symbol is defined.
        /// </summary>
        /// <param name="exception">Runtime Exception.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("DEBUG")]
        public static void LogException(Exception exception, Object context)
        {
            Debug.LogException(exception, context);
        }

        /// <summary>
        /// A variant of Debug.Log that logs a warning message to the console. Is stripped from compilation unless the 'DEBUG'
        /// symbol is defined.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("DEBUG")]
        public static void LogWarning(object message)
        {
            Debug.LogWarning(message);
        }

        /// <summary>
        /// A variant of Debug.Log that logs a warning message to the console. Is stripped from compilation unless the 'DEBUG'
        /// symbol is defined.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("DEBUG")]
        public static void LogWarning(object message, Object context)
        {
            Debug.LogWarning(message, context);
        }

        /// <summary>
        /// Logs a formatted warning message to the Unity Console. Is stripped from compilation unless the 'DEBUG' symbol is
        /// defined.
        /// </summary>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        [Conditional("DEBUG")]
        public static void LogWarningFormat(string format, params object[] args)
        {
            Debug.LogWarningFormat(format, args);
        }

        /// <summary>
        /// Logs a formatted warning message to the Unity Console. Is stripped from compilation unless the 'DEBUG' symbol is
        /// defined.
        /// </summary>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        [Conditional("DEBUG")]
        public static void LogWarningFormat(Object context, string format, params object[] args)
        {
            Debug.LogWarningFormat(context, format, args);
        }

        /// <summary>
        /// A variant of Debug.Log that logs an assertion message to the console. Is stripped from compilation unless the 'DEBUG'
        /// symbol is defined.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("DEBUG")]
        public static void LogAssertion(object message)
        {
            Debug.LogAssertion(message);
        }

        /// <summary>
        /// A variant of Debug.Log that logs an assertion message to the console. Is stripped from compilation unless the 'DEBUG'
        /// symbol is defined.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("DEBUG")]
        public static void LogAssertion(object message, Object context)
        {
            Debug.LogAssertion(message, context);
        }

        /// <summary>
        /// Logs a formatted assertion message to the Unity console. Is stripped from compilation unless the 'DEBUG' symbol is
        /// defined.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("DEBUG")]
        public static void LogAssertionFormat(string format, params object[] args)
        {
            Debug.LogAssertionFormat(format, args);
        }

        /// <summary>
        /// Logs a formatted assertion message to the Unity console. Is stripped from compilation unless the 'DEBUG' symbol is
        /// defined.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("DEBUG")]
        public static void LogAssertionFormat(Object context, string format, params object[] args)
        {
            Debug.LogAssertionFormat(context, format, args);
        }
    }
}