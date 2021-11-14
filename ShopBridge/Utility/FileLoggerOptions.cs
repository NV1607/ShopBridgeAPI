using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Utility
{
    /// <summary>
    /// FileLoggerOptions
    /// </summary>
    public class FileLoggerOptions
    {
        /// <summary>
        /// FilePath
        /// </summary>
        public virtual string FilePath { get; set; }

        /// <summary>
        /// FileName
        /// </summary>
        public virtual string FileName { get; set; } = "shopbridge-{date}.log";

    }
}
