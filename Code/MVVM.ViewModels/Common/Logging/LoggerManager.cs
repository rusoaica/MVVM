/// Written by: Yulia Danilova
/// Creation Date: 26th of January, 2021
/// Purpose: Implementation of ILoggerManager interface
#region ========================================================================= USING =====================================================================================
using NLog;
using MVVM.ViewModels.Common.Interfaces;
#endregion

namespace MVVM.ViewModels.Common.Logging
{
    public class LoggerManager : ILoggerManager
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Writes the diagnostic message at Debug level
        /// </summary>
        /// <param name="_message">The message to log</param>
        public void LogDebug(string _message)
        {
            logger.Debug(_message);
        }

        /// <summary>
        /// Writes the diagnostic message at Error level
        /// </summary>
        /// <param name="_message">The message to log</param>
        public void LogError(string _message)
        {
            logger.Error(_message);
        }

        /// <summary>
        /// Writes the diagnostic message at Info level
        /// </summary>
        /// <param name="_message">The message to log</param>
        public void LogInfo(string _message)
        {
            logger.Info(_message);
        }

        /// <summary>
        /// Writes the diagnostic message at Warn level
        /// </summary>
        /// <param name="_message">The message to log</param>
        public void LogWarn(string _message)
        {
            logger.Warn(_message);
        }
        #endregion
    }
}
