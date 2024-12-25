using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using lab1_api.Models;
using lab1_api.Exceptions;

namespace lab1_api.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Tiếp tục pipeline
                await _next(httpContext);
            }
            catch (AppException ex)
            {
                // Bắt AppException và trả về thông báo lỗi chi tiết
                _logger.LogError($"AppException: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                // Bắt các ngoại lệ khác và trả về lỗi chung
                _logger.LogError($"An unexpected error occurred: {ex.Message}");
                await HandleExceptionAsync(httpContext, new AppException(ErrorCode.UNCATEGORIZED_EXCEPTION));
            }
        }

        private Task HandleExceptionAsync(HttpContext context, AppException exception)
        {
            // Thiết lập mã trạng thái HTTP
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.StatusCode;

            // Tạo phản hồi lỗi
            var response = GenericApiResponse<object>.Error(exception.Message);
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
