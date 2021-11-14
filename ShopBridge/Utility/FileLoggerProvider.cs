using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Utility
{
    /// <summary>
    /// FileLogger Provider
    /// </summary>
    public class FileLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly FileLoggerOptions Options;

        /// <summary>
        /// FileLoggerProvider
        /// </summary>
        /// <param name="_options"></param>
        public FileLoggerProvider(IOptions<FileLoggerOptions> _options)
        {
            Options = _options.Value;
            CreateFilePathIfNotExists();
        }

        /// <summary>
        /// CreateLogger
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private void CreateFilePathIfNotExists()
        {
            if (string.IsNullOrEmpty(Options.FilePath))
                Options.FilePath = Path.Combine(".", "logs") + Path.DirectorySeparatorChar;

            if (!Directory.Exists(Options.FilePath))
                Directory.CreateDirectory(Options.FilePath);
        }

        /// <summary>
        /// GetFilePath
        /// </summary>
        /// <returns></returns>
        public string GetFilePath()
        {
            string date = DateTimeOffset.Now.ToString("yyyyMMdd");
            string filePath = Options.FilePath + Options.FileName.Replace("{date}", date);

            var files = new DirectoryInfo(Options.FilePath)
                .GetFiles(Options.FileName.Replace("{date}.log", date) + "*.*")
                ?.OrderByDescending(x => x.CreationTime);


            return filePath;
        }
    }
}
