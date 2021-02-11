/// Written by: Yulia Danilova
/// Creation Date: 24th of October, 2019
/// Purpose: Implementation of IAsyncCommand and generic IAsyncCommand interfaces
/// Remarks: The two versions are pretty similar and it is tempting to only keep the latter. We could use a AsyncCommand<object> with null parameter to replace the first one. 
///          While it technically works, it is better to keep the two of them both, in the sense that having no parameter is not semantically similar to taking a null parameter.
#region ========================================================================= USING =====================================================================================
using System;
using System.Windows.Input;
using System.Threading.Tasks;
using MVVM.ViewModels.Common.Interfaces;
using MVVM.ViewModels.Common.Extensions;
#endregion

namespace MVVM.ViewModels.Common.MVVM
{
    public class AsyncCommand : IAsyncCommand
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        public event EventHandler CanExecuteChanged;

        private bool isExecuting;
        private readonly Func<Task> execute;
        private readonly Func<bool> canExecute;
        private readonly IErrorHandler errorHandler;
        #endregion

        #region ================================================================== CTOR =====================================================================================
        /// <summary>
        /// Default C-tor
        /// </summary>
        /// <param name="_execute">The async Task method to be executed</param>
        /// <param name="_canExecute">The method indicating whether the <paramref name="_execute"/>can be executed</param>
        /// <param name="_errorHandler">Optional error handling reference</param>
        public AsyncCommand(Func<Task> _execute, Func<bool> _canExecute = null, IErrorHandler _errorHandler = null)
        {
            execute = _execute;
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
        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    isExecuting = true;
                    await execute();
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
            ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
        }
        #endregion
        #endregion
    }

    public class AsyncCommand<T> : IAsyncCommand<T>
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        public event EventHandler CanExecuteChanged;

        private bool isExecuting;
        private readonly Func<T, Task> execute;
        private readonly Func<T, bool> canExecute;
        private readonly IErrorHandler errorHandler;
        #endregion

        #region ================================================================== CTOR =====================================================================================
        /// <summary>
        /// Default C-tor
        /// </summary>
        /// <param name="_execute">The async Task method to be executed</param>
        /// <param name="_canExecute">The method indicating whether the <paramref name="_execute"/>can be executed</param>
        /// <param name="_errorHandler">Optional error handling reference</param>
        public AsyncCommand(Func<T, Task> _execute, Func<T, bool> _canExecute = null, IErrorHandler _errorHandler = null)
        {
            execute = _execute;
            canExecute = _canExecute;
            errorHandler = _errorHandler;
        }
        #endregion

        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Indicates whether the command can be executed
        /// </summary>
        /// <param name="_parameter">Generic parameter passed to the command to be executed</param>
        /// <returns>True if the command can be executed; False otherwise.</returns>
        public bool CanExecute(T _parameter)
        {
            return !isExecuting && (canExecute?.Invoke(_parameter) ?? true);
        }

        /// <summary>
        /// Executes a Task
        /// </summary>
        /// <param name="_parameter">Generic parameter passed to the command to be executed</param>
        public async Task ExecuteAsync(T _parameter)
        {
            if (CanExecute(_parameter))
            {
                try
                {
                    isExecuting = true;
                    await execute(_parameter);
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
        /// Also, see the parameterless version <seealso cref="AsyncCommand.CanExecute">AsyncCommand.CanExecute</seealso></param>
        /// <returns>True if this command can be executed; otherwise, False.</returns>
        bool ICommand.CanExecute(object _parameter)
        {
            // WPF bug - due to virtualization, sometimes _parameter can be automatically set to "DisconnectedItem", throwing exception
            return _parameter == null || _parameter.ToString() == "{DisconnectedItem}" || CanExecute((T)_parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="_parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// Also, see the parameterless version <seealso cref="AsyncCommand.CanExecute">AsyncCommand.CanExecute</seealso></param>
        void ICommand.Execute(object _parameter)
        {
            ExecuteAsync((T)_parameter).FireAndForgetSafeAsync(errorHandler);
        }
        #endregion
        #endregion
    }
}
