using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MovieShopMVC.Middlewares;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MovieShopMVC.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MovieShopExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MovieShopExceptionMiddleware> _logger;

        public MovieShopExceptionMiddleware(RequestDelegate next, ILogger<MovieShopExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var exceptionDetails = new
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    ExceptionDateTime = DateTime.UtcNow,
                    ExceptionType = ex.GetType(),
                    Path = httpContext.Request.Path,
                    HttpMethod = httpContext.Request.Method,
                    User = httpContext.User.Identity.IsAuthenticated
                    ? httpContext.User.Identity.Name : null
                    // Email, UserId, QueryString, Headers, etc
                };

                // 📝 拼接日志文本
                var logText = $@"
======== Exception Caught ========
Time: {exceptionDetails.ExceptionDateTime}
Path: {exceptionDetails.Path}
Method: {exceptionDetails.HttpMethod}
User: {exceptionDetails.User}
Type: {exceptionDetails.ExceptionType}
Message: {exceptionDetails.Message}
StackTrace: {exceptionDetails.StackTrace}
==================================
";




                // 📁 日志文件路径（log/error.log）
                var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "log", "error.log");

                // ✅ 确保log目录存在
                var logDir = Path.GetDirectoryName(logFilePath);
                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }

                // 🖊️ 将日志写入文件（追加模式）
                await File.AppendAllTextAsync(logFilePath, logText);

                // 🚨 你也可以继续重定向错误页面
                httpContext.Response.Redirect("/home/error");

                return;
            }
        }

    }

}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseMovieShopExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MovieShopExceptionMiddleware>();
    }
}
