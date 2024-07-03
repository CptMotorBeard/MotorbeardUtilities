using System;
using System.Collections.Generic;
using BeardKit.Detail;

namespace BeardKit
{
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public readonly struct Result<PAYLOAD_T, ERROR_CODE> where ERROR_CODE : struct
    {
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)

        public Result(in ERROR_CODE error)
        {
            m_payload = default;
            m_error = error;
        }

        public Result(in PAYLOAD_T payload)
        {
            m_payload = payload;
            m_error = null;
        }

        public PAYLOAD_T Payload
        {
            get
            {
                if (m_error != null)
                {
                    throw new InvalidOperationException("Result Payload being accessed but result has an error!");
                }

                return m_payload;
            }
        }

        public ERROR_CODE Error
        {
            get
            {
                if (m_error == null)
                {
                    throw new InvalidOperationException("Result Error being accessed but result is a success!");
                }

                return m_error.Value;
            }
        }

        #region Operators

        public static implicit operator Result<PAYLOAD_T, ERROR_CODE>(in ERROR_CODE errorCode)
        {
            return new Result<PAYLOAD_T, ERROR_CODE>(errorCode);
        }

        public static implicit operator Result<PAYLOAD_T, ERROR_CODE>(in PAYLOAD_T payload)
        {
            return new Result<PAYLOAD_T, ERROR_CODE>(payload);
        }

        public static bool operator ==(in Result<PAYLOAD_T, ERROR_CODE> result, in SuccessType success)
        {
            return result.m_error == null;
        }

        public static bool operator !=(in Result<PAYLOAD_T, ERROR_CODE> result, in SuccessType success)
        {
            return !(result == success);
        }

        public static bool operator ==(in Result<PAYLOAD_T, ERROR_CODE> result, in ERROR_CODE error_code)
        {
            if (!result.m_error.HasValue)
            {
                return false;
            }

            if (error_code is Enum)
            {
                return Comparer<ERROR_CODE>.Default.Compare(result.Error, error_code) == 0;
            }

            return EqualityComparer<ERROR_CODE>.Default.Equals(result.m_error.Value, error_code);
        }

        public static bool operator !=(in Result<PAYLOAD_T, ERROR_CODE> result, in ERROR_CODE error_code)
        {
            return !(result == error_code);
        }

        #endregion

        private readonly ERROR_CODE? m_error;
        private readonly PAYLOAD_T m_payload;
    }

#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public readonly struct Result<ERROR_CODE> where ERROR_CODE : struct
    {
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)

        public Result(in ERROR_CODE error)
        {
            m_error = error;
        }

        public ERROR_CODE Error
        {
            get
            {
                if (m_error == null)
                {
                    throw new InvalidOperationException("Result Error being accessed but result is a success!");
                }

                return m_error.Value;
            }
        }

        #region Operators

        public static implicit operator Result<ERROR_CODE>(in ERROR_CODE errorCode)
        {
            return new Result<ERROR_CODE>(errorCode);
        }

        public static bool operator ==(in Result<ERROR_CODE> result, in SuccessType success)
        {
            return result.m_error == null;
        }

        public static bool operator !=(in Result<ERROR_CODE> result, in SuccessType success)
        {
            return !(result == success);
        }

        public static bool operator ==(in Result<ERROR_CODE> result, in ERROR_CODE error_code)
        {
            if (!result.m_error.HasValue)
            {
                return false;
            }

            if (error_code is Enum)
            {
                return Comparer<ERROR_CODE>.Default.Compare(result.Error, error_code) == 0;
            }

            return EqualityComparer<ERROR_CODE>.Default.Equals(result.m_error.Value, error_code);
        }

        public static bool operator !=(in Result<ERROR_CODE> result, in ERROR_CODE error_code)
        {
            return !(result == error_code);
        }

        #endregion

        private readonly ERROR_CODE? m_error;
    }

    public static class Result
    {
        public static SuccessType Success = SuccessType.SuccessValue;
    }

    namespace Detail
    {
        public struct SuccessType
        {
            public static SuccessType SuccessValue;
        }
    }
}