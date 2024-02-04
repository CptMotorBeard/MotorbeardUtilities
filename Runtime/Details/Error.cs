using System.Collections.Generic;

namespace BeardKit
{
    public interface IError
    {
        string ToLoggableString();
        string ToErrorCodeString();
    }

    public readonly struct Error<TCode> : IError
        where TCode : struct, System.Enum
    {
        public Error(in TCode code, in string message="")
        {
            Code = code;
            Message = message;
        }

        public TCode Code { get; }

        /// <summary>The user set message.</summary>
        public string Message { get; }

        public override string ToString()
        {
            return ToLoggableString();
        }

        public string ToLoggableString()
        {
            return string.IsNullOrEmpty(Message) ? $"Code: {Code}" : $"Code: {Code}, Message: {Message}";
        }

        public string ToErrorCodeString()
        {
            return $"{System.Convert.ToInt32(Code)}";
        }

        public static implicit operator Error<TCode>(in TCode code)
        {
            return new Error<TCode>(code);
        }

        public static bool operator ==(in Error<TCode> error, in TCode code)
        {
            return Comparer<TCode>.Default.Compare(error.Code, code) == 0;
        }

        public static bool operator !=(in Error<TCode> error, in TCode code)
        {
            return !(error == code);
        }

        public static bool operator ==(in Error<TCode> lhs, in Error<TCode> rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(in Error<TCode> lhs, in Error<TCode> rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(in Error<TCode> other)
        {
            return Comparer<TCode>.Default.Compare(Code, other.Code) == 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is Error<TCode> other)
            {
                return Equals(other);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }

    public readonly struct Error<TCode, TError> : IError
        where TCode : struct, System.Enum
        where TError : struct, IError
    {
        private readonly IError m_innerError;

        public Error(in TCode code, in string message = "")
        {
            m_innerError = default;
            Code = code;
            Message = message;
        }

        public Error(in TCode code, in TError innerError, in string message = "") : this(code, message)
        {
            m_innerError = innerError;
        }

        public TCode Code { get; }

        /// <summary>The user set message.</summary>
        public string Message { get; }

        public override string ToString()
        {
            string outerError = string.IsNullOrEmpty(Message) ? $"Code: {Code}" : $"Code: {Code}, Message: {Message}";
            string innerError = m_innerError.ToString();
            // Inverse
            string str = innerError;
            if (!string.IsNullOrEmpty(outerError))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    str += "\n";
                }
                str += outerError;
            }

            return str;
        }

        public string ToLoggableString()
        {
            string innerError = m_innerError.ToLoggableString();
            if (string.IsNullOrEmpty(innerError))
            {
                return string.IsNullOrEmpty(Message) ? $"Code: {Code}" : $"Code: {Code}, Message: {Message}";
            }
            else
            {
                return string.IsNullOrEmpty(Message) ? $"Code: {Code}, Inner: {{ {innerError} }}" : $"Code: {Code}, Message: {Message}, , Inner: {{ {innerError} }}";
            }
        }

        public string ToErrorCodeString()
        {
            string innerCode = m_innerError.ToErrorCodeString();
            if (string.IsNullOrEmpty(innerCode))
            {
                return $"{System.Convert.ToInt32(Code)}";
            }
            else
            {
                return $"{System.Convert.ToInt32(Code)}-{innerCode}";
            }
        }

        public bool HasInner() => m_innerError != null;
        public TError GetInner() => (TError)m_innerError;

        public bool TryGetInner(out TError error)
        {
            if (m_innerError == null)
            {
                error = default;
                return false;
            }

            error = (TError)m_innerError;
            return true;
        }

        public static implicit operator Error<TCode, TError>(in TCode code)
        {
            return new Error<TCode, TError>(code);
        }

        public static bool operator ==(in Error<TCode, TError> error, in TCode code)
        {
            return Comparer<TCode>.Default.Compare(error.Code, code) == 0;
        }

        public static bool operator !=(in Error<TCode, TError> error, in TCode code)
        {
            return !(error == code);
        }

        public static bool operator ==(in Error<TCode, TError> lhs, in Error<TCode, TError> rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(in Error<TCode, TError> lhs, in Error<TCode, TError> rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(in Error<TCode, TError> other)
        {
            return Comparer<TCode>.Default.Compare(Code, other.Code) == 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is Error<TCode, TError> other)
            {
                return Equals(other);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}