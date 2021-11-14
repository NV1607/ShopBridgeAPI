using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShopBridge.Middleware
{
    /// <summary>
    /// ErrorHandlerMiddleware
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// ErrorHandlerMiddleware
        /// </summary>
        /// <param name="next"></param>
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, ILogger<ErrorHandlerMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {

                switch (error)
                {
                    case KeyNotFoundException:
                        // not found error
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                logger.LogError(context.Response.StatusCode, error, error.Message);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(error.ToString());
            }
        }
    }
}
