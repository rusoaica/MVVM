/// Written by: Yulia Danilova
/// Creation Date: 24th of October, 2019
/// Purpose: Interface for implementing generic error handling in MVVM
#region ========================================================================= USING =====================================================================================
using System;
#endregion

namespace MVVM.ViewModels.Common.Interfaces
{
    public interface IErrorHandler
    {
        #region ================================================================ METHODS ====================================================================================
        /// <summary>
        /// Error handling method signature
        /// </summary>
        /// <param name="ex">The exception to be raised</param>
        void HandleError(Exception ex);
        #endregion
    }
}
