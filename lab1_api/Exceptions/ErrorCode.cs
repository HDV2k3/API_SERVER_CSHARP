using System.Net;

namespace lab1_api.Exceptions
{
    public enum ErrorCode
    {
        UNCATEGORIZED_EXCEPTION = 9999,
        INVALID_KEY = 1001,
        USER_EXISTED = 1002,
        USERNAME_INVALID = 1003,
        INVALID_PASSWORD = 1004,
        USER_NOT_EXISTED = 1005,
        UNAUTHENTICATED = 1006,
        UNAUTHORIZED = 1007,
        INVALID_DOB = 1008,
        SEND_EMAIL_ERROR = 1009,
        USER_ALREADY_EXISTED = 1010,
        INVALID_EMAIL = 1011,
    }

    public static class ErrorCodeExtensions
    {
        public static string GetMessage(this ErrorCode errorCode)
        {
            return errorCode switch
            {
                ErrorCode.UNCATEGORIZED_EXCEPTION => "Uncategorized error",
                ErrorCode.INVALID_KEY => "Invalid key",
                ErrorCode.USER_EXISTED => "User existed",
                ErrorCode.USERNAME_INVALID => "Username must be at least {min} characters",
                ErrorCode.INVALID_PASSWORD => "Password must be at least {min} characters",
                ErrorCode.USER_NOT_EXISTED => "User not existed",
                ErrorCode.UNAUTHENTICATED => "Unauthenticated",
                ErrorCode.UNAUTHORIZED => "You do not have permission",
                ErrorCode.INVALID_DOB => "Your age must be at least {min}",
                ErrorCode.SEND_EMAIL_ERROR => "Failed to send email",
                ErrorCode.USER_ALREADY_EXISTED => "User already existed",
                ErrorCode.INVALID_EMAIL => "Email Not Found",
                _ => "Unknown error"
            };
        }

        public static HttpStatusCode GetHttpStatusCode(this ErrorCode errorCode)
        {
            return errorCode switch
            {
                ErrorCode.INVALID_KEY => HttpStatusCode.BadRequest,
                ErrorCode.USER_EXISTED => HttpStatusCode.BadRequest,
                ErrorCode.USERNAME_INVALID => HttpStatusCode.BadRequest,
                ErrorCode.INVALID_PASSWORD => HttpStatusCode.BadRequest,
                ErrorCode.USER_NOT_EXISTED => HttpStatusCode.NotFound,
                ErrorCode.UNAUTHENTICATED => HttpStatusCode.Unauthorized,
                ErrorCode.UNAUTHORIZED => HttpStatusCode.Forbidden,
                ErrorCode.INVALID_DOB => HttpStatusCode.BadRequest,
                ErrorCode.SEND_EMAIL_ERROR => HttpStatusCode.InternalServerError,
                ErrorCode.USER_ALREADY_EXISTED => HttpStatusCode.BadRequest,
                ErrorCode.INVALID_EMAIL => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }
}
