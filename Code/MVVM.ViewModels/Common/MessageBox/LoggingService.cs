/// Written by: Yulia Danilova
/// Creation Date: 21th of November, 2019
/// Purpose: Explicit implementation of abstract custom message box service. Uses NLog for logging, mocking up user interaction with a dialog box.
#region ========================================================================= USING =====================================================================================
using MVVM.ViewModels.Common.Enums;
using MVVM.ViewModels.Common.Interfaces;
using MVVM.ViewModels.Startup;
#endregion

namespace MVVM.ViewModels.Common.MessageBox
{
    internal class LoggingService : IMessageBoxService
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        private bool? injectedDialogResult = null;
        #endregion

        #region ================================================================== CTOR =====================================================================================
        /// <summary>
        /// Overloaded c-tor
        /// </summary>
        /// <param name="_injectedDialogResult">A boolean indicating mock up user interaction result (simulates MessageBoxResult)</param>
        public LoggingService(bool? _injectedDialogResult)
        {
            this.injectedDialogResult = _injectedDialogResult;
        }
        #endregion

        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Changes the dialog result of the MessageBox mock up
        /// </summary>
        /// <param name="_newDialogResult">The new dialog result value</param>
        public void ChangeInjectedDialogResult(bool? _newDialogResult)
        {
            this.injectedDialogResult = _newDialogResult;
        }

        /// <summary>
        /// Mocks up a new MessageBox interaction and writes at the console the message and the response
        /// </summary>
        /// <param name="_message">The text to display in the custom MessageBox</param>
        /// <returns>A <see cref="MessageBoxResult"/> value representing the result of the mockup MessageBox dialog</returns>
        public MessageBoxResult Show(string _message)
        {
            StartupVM.logger.Info("Message was: " + _message);
            switch (injectedDialogResult)
            {
                case true:
                    StartupVM.logger.Info("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                    StartupVM.logger.Info("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                    StartupVM.logger.Info("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                    StartupVM.logger.Info("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
            }
        }

        /// <summary>
        /// Mocks up a new MessageBox interaction and writes at the console the message and the response
        /// </summary>
        /// <param name="_message">The text to display in the custom MessageBox</param>
        /// <param name="_title">The Title of the mockup MessageBox dialog</param>
        /// <returns>A <see cref="MessageBoxResult"/> value representing the result of the mockup MessageBox dialog</returns>
        public MessageBoxResult Show(string _message, string _title)
        {
            StartupVM.logger.Info("Title was: " + _title);
            StartupVM.logger.Info("Message was: " + _message);
            switch (injectedDialogResult)
            {
                case true:
                    StartupVM.logger.Info("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                    StartupVM.logger.Info("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                    StartupVM.logger.Info("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                    StartupVM.logger.Info("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
            }
        }

        /// <summary>
        /// Mocks up a new MessageBox interaction and writes at the console the message and the response
        /// </summary>
        /// <param name="_message">The text to display in the custom MessageBox</param>
        /// <param name="_title">The Title of the mockup MessageBox dialog</param>
        /// <param name="_buttons">The buttons of the mockup MessageBox dialog</param>
        /// <returns>A <see cref="MessageBoxResult"/> value representing the result of the mockup MessageBox dialog</returns>
        public MessageBoxResult Show(string _message, string _title, MessageBoxButton _buttons)
        {
            StartupVM.logger.Info("Title was: " + _title);
            StartupVM.logger.Info("Message was: " + _message);
            switch (injectedDialogResult)
            {
                case true:
                    StartupVM.logger.Info("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                    StartupVM.logger.Info("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                    StartupVM.logger.Info("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                    StartupVM.logger.Info("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
            }
        }

        /// <summary>
        /// Mocks up a new MessageBox interaction and writes at the console the message and the response
        /// </summary>
        /// <param name="_message">The text to display in the custom MessageBox</param>
        /// <param name="_title">The Title of the mockup MessageBox dialog</param>
        /// <param name="_buttons">The buttons of the mockup MessageBox dialog</param>
        /// <param name="_image">The icon of the mockup MessageBox dialog</param>
        /// <returns>A <see cref="MessageBoxResult"/> value representing the result of the mockup MessageBox dialog</returns>
        public MessageBoxResult Show(string _message, string _title, MessageBoxButton _buttons, MessageBoxImage _image)
        {
            StartupVM.logger.Info("Title was: " + _title);
            StartupVM.logger.Info("Message was: " + _message);
            switch (injectedDialogResult)
            {
                case true:
                    StartupVM.logger.Info("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                    StartupVM.logger.Info("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                    StartupVM.logger.Info("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                    StartupVM.logger.Info("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
            }
        }
        #endregion
    }
}
