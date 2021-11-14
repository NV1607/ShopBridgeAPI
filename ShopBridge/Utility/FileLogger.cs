using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopBridge.Utility
{
    /// <summary>
    /// FileLogger
    /// </summary>
    public class FileLogger : ILogger
    {
        private readonly FileLoggerProvider FileProvider;

        private static readonly ReaderWriterLockSlim ReadWriteLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileProvider"></param>
        public FileLogger([NotNull] FileLoggerProvider fileProvider) => FileProvider = fileProvider;

        /// <summary>
        /// BeginScope
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state) => null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;
            ReadWriteLock.EnterWriteLock();
            try
            {
                using var streamWriter = new StreamWriter(FileProvider.GetFilePath(), true);
                streamWriter.WriteLine(string.Format("[{0}] [{1}] {2} {3} {4}",
                    DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss+00:00"),
                    logLevel.ToString(),
                    formatter(state, exception),
                    exception?.StackTrace?.ToString(),
                    exception?.InnerException?.ToString()));
            }
            finally
            {
                ReadWriteLock.ExitWriteLock();
            }
        }
    }
}
