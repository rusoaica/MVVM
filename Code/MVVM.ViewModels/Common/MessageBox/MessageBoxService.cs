/// Written by: Yulia Danilova
/// Creation Date: 19th of November, 2019
/// Purpose: Explicit implementation of abstract custom message box service
#region ========================================================================= USING =====================================================================================
using System;
using MVVM.ViewModels.Common.Enums;
using MVVM.ViewModels.Common.Interfaces;
#endregion

namespace MVVM.ViewModels.Common.MessageBox
{
    /// <summary>
    /// A service that shows message boxes.
    /// </summary>
    internal class MessageBoxService : IMessageBoxService
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        private readonly IDispatcher dispatcher;
        #endregion

        #region ================================================================== CTOR =====================================================================================
        /// <summary>
        /// Overload C-tor
        /// </summary>
        /// <param name="_dispatcher">The dispatcher to use</param>
        public MessageBoxService(IDispatcher _dispatcher)
        {
            dispatcher = _dispatcher;
        }
        #endregion

        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Changes the dialog result of the custom MessageBox
        /// </summary>
        /// <param name="_newDialogResult">The new dialog result value</param>
        public void ChangeInjectedDialogResult(bool? _newDialogResult)
        {
            // MessageBoxService dialog results are set from UI interraction!
            throw new NotSupportedException("Dialog results are set from UI only!");
        }

        /// <summary>
        /// Shows a new custom MessageBox
        /// </summary>
        /// <param name="_message">The text to display in the custom MessageBox</param>
        /// <returns>A <see cref="MessageBoxResult"/> value representing the result of the custom MessageBox dialog</returns>
        public MessageBoxResult Show(string _message)
        {
            return (MessageBoxResult)(dispatcher?.Dispatch(new Func<MessageBoxResult>(() =>
            {
                return new MsgBoxVM().Show(_message);
            })));
        }

        /// <summary>
        /// Shows a new custom MessageBox
        /// </summary>
        /// <param name="_message">The text to display in the custom MessageBox</param>
        /// <param name="_title">The Title of the custom MessageBox dialog</param>
        /// <returns>A <see cref="MessageBoxResult"/> value representing the result of the custom MessageBox dialog</returns>
        public MessageBoxResult Show(string _message, string _title)
        {
            return (MessageBoxResult)(dispatcher?.Dispatch(new Func<MessageBoxResult>(() =>
            {
                return new MsgBoxVM().Show(_message, _title);
            })));
        }

        /// <summary>
        /// Shows a new custom MessageBox
        /// </summary>
        /// <param name="_message">The text to display in the custom MessageBox</param>
        /// <param name="_title">The Title of the custom MessageBox dialog</param>
        /// <param name="_buttons">The buttons displayed inside the custom MessageBox dialog</param>
        /// <returns>A <see cref="MessageBoxResult"/> value representing the result of the custom MessageBox dialog</returns>
        public MessageBoxResult Show(string _message, string _title, MessageBoxButton _buttons)
        {
            return (MessageBoxResult)(dispatcher?.Dispatch(new Func<MessageBoxResult>(() =>
            {
                return new MsgBoxVM().Show(_message, _title, _buttons);
            })));
        }

        /// <summary>
        /// Shows a new custom MessageBox
        /// </summary>
        /// <param name="_message">The text to display in the custom MessageBox</param>
        /// <param name="_title">The Title of the custom MessageBox dialog</param>
        /// <param name="_buttons">The buttons displayed inside the custom MessageBox dialog</param>
        /// <param name="_image">The icon of the custom MessageBox dialog</param>
        /// <returns>A <see cref="MessageBoxResult"/> value representing the result of the custom MessageBox dialog</returns>
        public MessageBoxResult Show(string _message, string _title, MessageBoxButton _buttons, MessageBoxImage _image)
        {
            return (MessageBoxResult)(dispatcher?.Dispatch(new Func<MessageBoxResult>(() =>
            {
                return new MsgBoxVM().Show(_message, _title, _buttons, _image);
            })));
        }
        #endregion
    }
}
