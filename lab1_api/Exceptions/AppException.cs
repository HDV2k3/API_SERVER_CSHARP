using System;
using System.Net;

namespace lab1_api.Exceptions
{
    public class AppException : Exception
    {
        public ErrorCode ErrorCode { get; }
        public HttpStatusCode StatusCode { get; }

        public AppException(ErrorCode errorCode) : base(errorCode.GetMessage())
        {
            ErrorCode = errorCode;
            StatusCode = errorCode.GetHttpStatusCode();
        }

        public AppException(ErrorCode errorCode, string message, Exception ex) : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = errorCode.GetHttpStatusCode();
        }
    }
}
