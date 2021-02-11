/// Written by: Yulia Danilova
/// Creation Date: 27th of November, 2019
/// Purpose: Interface for interacting with Dispatcher objects
#region ========================================================================= USING =====================================================================================
using System;
#endregion

namespace MVVM.ViewModels.Common.Interfaces
{
    public interface IDispatcher
    {
        #region ================================================================= METHODS ===================================================================================
        TResult Dispatch<TResult>(Func<TResult> callback);
        void Dispatch(Delegate method, params object[] args);
        #endregion
    }
}
