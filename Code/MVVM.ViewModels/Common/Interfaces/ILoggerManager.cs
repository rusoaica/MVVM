/// Written by: Yulia Danilova
/// Creation Date: 26th of January, 2021
/// Purpose: Interface for providing logging functionality

namespace MVVM.ViewModels.Common.Interfaces
{
    public interface ILoggerManager
    {
        #region ================================================================= METHODS ===================================================================================
        void LogInfo(string _message);
        void LogWarn(string _message);
        void LogDebug(string _message);
        void LogError(string _message);
        #endregion
    }
}
