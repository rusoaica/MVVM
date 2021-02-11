﻿/// Written by: Yulia Danilova
/// Creation Date: 24th of October, 2019
/// Purpose: Asynchronous non-generic and generic implementation of ICommand interface
#region ========================================================================= USING =====================================================================================
using System.Threading.Tasks;
using System.Windows.Input;
#endregion

namespace MVVM.ViewModels.Common.Interfaces
{
    public interface IAsyncCommand : ICommand
    {
        #region ================================================================ METHODS ====================================================================================
        /// <summary>
        /// Defines the asynchronous method to be called when the command is invoked.
        /// </summary>
        Task ExecuteAsync();

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state
        /// </summary>
        /// <returns>True if this command can be executed; otherwise, False.</returns>
        bool CanExecute();
        #endregion
    }

    public interface IAsyncCommand<T> : ICommand
    {
        #region ================================================================ METHODS ====================================================================================
        /// <summary>
        /// Defines the generic asynchronous method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Generic parameter passed to the command</param>
        Task ExecuteAsync(T parameter);

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state
        /// </summary>
        /// <param name="parameter">Generic parameter passed to the command</param>
        /// <returns>True if this command can be executed; otherwise, False.</returns>
        bool CanExecute(T parameter);
        #endregion
    }   
}
