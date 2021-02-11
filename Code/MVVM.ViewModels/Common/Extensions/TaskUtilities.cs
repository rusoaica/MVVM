/// Written by: Yulia Danilova
/// Creation Date: 24th of October, 2019
/// Purpose: Task extension method to handle exceptions (avoid littering whole project with try...catch'es)
#region ========================================================================= USING =====================================================================================
using System;
using System.Threading.Tasks;
using MVVM.ViewModels.Common.Interfaces;
#endregion

namespace MVVM.ViewModels.Common.Extensions
{
    public static class TaskUtilities
    {
        #region ================================================================ METHODS ====================================================================================
#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
#pragma warning restore RECS0165
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
        #endregion
    }
}
