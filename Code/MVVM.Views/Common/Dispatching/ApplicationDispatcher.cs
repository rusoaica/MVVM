/// Written by: Yulia Danilova
/// Creation Date: 27th of November, 2019
/// Purpose: Explicit implementation of abstract Dispatcher interface, used in UI environments
#region ========================================================================= USING =====================================================================================
using System;
using System.Windows;
using System.Windows.Threading;
using MVVM.ViewModels.Common.Interfaces;
#endregion

namespace MVVM.Views.Common.Dispatching
{
    public class ApplicationDispatcher : IDispatcher
    {
        #region =============================================================== PROPERTIES ==================================================================================
        Dispatcher UnderlyingDispatcher
        {
            get
            {
                if (Application.Current == null)
                    throw new InvalidOperationException("You must call this method from within a running WPF application!");
                if (Application.Current.Dispatcher == null)
                    throw new InvalidOperationException("You must call this method from within a running WPF application with an active dispatcher!");
                return Application.Current.Dispatcher;
            }
        }
        #endregion

        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Executes the specified delegate at the specified priority with the specified argument synchronously
        /// </summary>
        /// <param name="_method">A delegate to a method that takes one argument, which is pushed onto the System.Windows.Threading.Dispatcher event queue.</param>
        /// <param name="_args">An object to pass as an argument to the given method.</param>
        public void Dispatch(Delegate _method, params object[] _args)
        {
            UnderlyingDispatcher.Invoke(DispatcherPriority.Background, _method, _args);
        }

        /// <summary>
        /// Executes the specified delegate synchronously
        /// </summary>
        /// <typeparam name="TResult">The type of result returned by <paramref name="callback"/></typeparam>
        /// <param name="callback">A func returning a result of type <typeparamref name="TResult"/></param>
        /// <returns>A Func callback of type <typeparamref name="TResult"/></returns>
        public TResult Dispatch<TResult>(Func<TResult> callback)
        {
            return UnderlyingDispatcher.Invoke(callback);
        }
        #endregion
    }
}
