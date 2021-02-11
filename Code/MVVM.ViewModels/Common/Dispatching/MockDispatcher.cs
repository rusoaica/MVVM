/// Written by: Yulia Danilova
/// Creation Date: 27th of November, 2019
/// Purpose: Explicit implementation of abstract Dispatcher interface, used in testing
#region ========================================================================= USING =====================================================================================
using System;
using MVVM.ViewModels.Common.Interfaces;
#endregion

namespace MVVM.ViewModels.Common.Dispatching
{
    public class MockDispatcher : IDispatcher
    {
        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Executes the specified delegate
        /// </summary>
        /// <param name="method">A delegate to a method that takes one argument.</param>
        /// <param name="args">An object to pass as an argument to the given method.</param>
        public void Dispatch(Delegate _method, params object[] _args)
        {
            _method.DynamicInvoke((object)_args);
        }

        /// <summary>
        /// Executes the specified delegate
        /// </summary>
        /// <param name="callback">A func delegate to a method that takes one argument and returns a result.</param>
        /// <returns>The callback result of <paramref name="callback"/></returns>
        public TResult Dispatch<TResult>(Func<TResult> callback)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
