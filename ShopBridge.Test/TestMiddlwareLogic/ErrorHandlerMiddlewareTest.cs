using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using ShopBridge.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopBridge.Test.TestMiddlwareLogic
{
    public class ErrorHandlerMiddlewareTest
    {
        private readonly Mock<ILogger<ErrorHandlerMiddleware>> mocklogger;

        public ErrorHandlerMiddlewareTest()
        {
            mocklogger = new Mock<ILogger<ErrorHandlerMiddleware>>();
        }

        [Fact]
        public void ErrorHandlerMiddleware_KeyNotFoundExceptionResult()
        {
            // Arrange
            var middleware = new ErrorHandlerMiddleware(
            next: (innerHttpContext) =>
            {
                innerHttpContext.Response.WriteAsync("Key Not Found Expection Occur");
                return Task.FromException(new KeyNotFoundException());
            });
            var context = new DefaultHttpContext();
            context.Request.Method = HttpMethods.Get;
            context.Request.Path = "/api/Inventory";


            // Act
            var result = middleware.Invoke(context, mocklogger.Object);

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, context.Response.StatusCode);
        }

        [Fact]
        public void ErrorHandlerMiddleware_UnhandledExceptionResult()
        {
            // Arrange
            var middleware = new ErrorHandlerMiddleware(
            next: (innerHttpContext) =>
            {
                innerHttpContext.Response.WriteAsync("Unhandled Expection Occur");
                return Task.FromException(new Exception());
            });
            var context = new DefaultHttpContext();
            context.Request.Method = HttpMethods.Get;
            context.Request.Path = "/api/Inventory";


            // Act
            var result = middleware.Invoke(context, mocklogger.Object);

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        }
    }
}
