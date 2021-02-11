/// Written by: Yulia Danilova
/// Creation Date: 24th of October, 2019
/// Purpose: Implementation of ICommand and generic ISyncCommand interfaces
#region ========================================================================= USING =====================================================================================
using System;
using System.Windows.Input;
using MVVM.ViewModels.Common.Interfaces;
#endregion

namespace MVVM.ViewModels.Common.MVVM
{
    public class SyncCommand : ICommand
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        public event EventHandler CanExecuteChanged;

        private bool isExecuting;
        private readonly Action executeSync;
        private readonly Func<bool> canExecute;
        private readonly IErrorHandler errorHandler;
        #endregion

        #region ================================================================== CTOR =====================================================================================  
        /// <summary>
        /// Overload C-tor
        /// </summary>
        /// <param name="_execute">The async Task method to be executed</param>
        /// <param name="_canExecute">The method indicating whether the <paramref name="_execute"/>can be executed</param>
        /// <param name="_errorHandler">Optional error handling reference</param>
        public SyncCommand(Action _execute, Func<bool> _canExecute = null, IErrorHandler _errorHandler = null)
        {
            executeSync = _execute;
            canExecute = _canExecute;
            errorHandler = _errorHandler;
        }
        #endregion

        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Indicates whether the command can be executed
        /// </summary>
        /// <returns>True if the command can be executed; False otherwise.</returns>
        public bool CanExecute()
        {
            return !isExecuting && (canExecute?.Invoke() ?? true);
        }

        /// <summary>
        /// Executes a Task
        /// </summary>
        public void ExecuteSync()
        {
            if (CanExecute())
            {
                try
                {
                    isExecuting = true;
                    executeSync();
                }
                finally
                {
                    isExecuting = false;
                }
            }
            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Executes the delegate that signals changes in permissions of execution of the command
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit implementations
        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state
        /// </summary>
        /// <param name="_parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// Also, see the parameter version <seealso cref="AsyncCommand{T}.CanExecute(T)">AsyncCommand{T}.CanExecute(T)</seealso></param>
        /// <returns>True if this command can be executed; otherwise, False.</returns>
        bool ICommand.CanExecute(object _parameter)
        {
            return CanExecute();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="_parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// Also, see the parameter version <seealso cref="AsyncCommand{T}.CanExecute(T)">AsyncCommand{T}.CanExecute(T)</seealso></param>
        void ICommand.Execute(object _parameter)
        {
            try
            {
                ExecuteSync();
            }
            catch (Exception ex)
            {
                errorHandler?.HandleError(ex);
            }
        }
        #endregion
        #endregion
    }

    public class SyncCommand<T> : ISyncCommand<T>
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        public event EventHandler CanExecuteChanged;

        private bool isExecuting;
        private readonly Func<T, bool> canExecute;
        private readonly Action<T> executeSync;
        private readonly IErrorHandler errorHandler;
        #endregion

        #region ================================================================== CTOR =====================================================================================
        /// <summary>
        /// Overload C-tor
        /// </summary>
        /// <param name="_execute">The Action to be executed</param>
        /// <param name="_canExecute">The method indicating whether the <paramref name="_execute"/>can be executed</param>
        /// <param name="_errorHandler">Optional error handling reference</param>
        public SyncCommand(Action<T> _execute, Func<T, bool> _canExecute = null, IErrorHandler _errorHandler = null)
        {
            executeSync = _execute;
            canExecute = _canExecute;
            errorHandler = _errorHandler;
        }
        #endregion

        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state
        /// </summary>
        /// <param name="_parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// <returns>True if this command can be executed; otherwise, False.</returns>
        bool ICommand.CanExecute(object _parameter)
        {
            return _parameter == null || CanExecute((T)_parameter);
        }

        /// <summary>
        /// Executes a Task
        /// </summary>
        /// <param name="_parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.
        public void ExecuteSync(object _parameter)
        {
            if (CanExecute((T)_parameter))
            {
                try
                {
                    isExecuting = true;
                    executeSync((T)_parameter);
                }
                finally
                {
                    isExecuting = false;
                }
            }
            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Executes the delegate that signals changes in permissions of execution of the command
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit implementations
        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="_parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.
        void ICommand.Execute(object _parameter)
        {
            try
            {
                ExecuteSync((T)_parameter);
            }
            catch (Exception ex)
            {
                errorHandler?.HandleError(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public void ExecuteSync(T _parameter)
        {
            if (CanExecute(_parameter))
            {
                try
                {
                    isExecuting = true;
                    executeSync(_parameter);
                }
                finally
                {
                    isExecuting = false;
                }
            }
            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Indicates whether the command can be executed
        /// </summary>
        /// <param name="_parameter">Generic parameter passed to the command to be executed</param>
        /// <returns>True if the command can be executed; False otherwise.</returns>
        public bool CanExecute(T _parameter)
        {
            return !isExecuting && (canExecute?.Invoke(_parameter) ?? true);
        }
        #endregion
        #endregion
    }
}
