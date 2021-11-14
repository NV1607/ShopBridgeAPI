using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Utility
{
    /// <summary>
    /// FileLoggerExtension
    /// </summary>
    public static class FileLoggerExtension
    {
        /// <summary>
        /// Add FileLogger Extension Method to configure or specfy to use
        /// FileLoggerProvider whenever used ILoggerProvider
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, Action<FileLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>()
                .Configure(configure);
            return builder;
        }
    }
}
