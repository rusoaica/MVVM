/// Written by: Yulia Danilova
/// Creation Date: 21th of November, 2019
/// Purpose: Explicit implementation of abstract custom message box service. Uses console for logging, mocking up user interaction with a dialog box.
#region ========================================================================= USING =====================================================================================
using System;
using MVVM.ViewModels.Common.Enums;
using MVVM.ViewModels.Common.Interfaces;
#endregion

namespace MVVM.ViewModels.Common.MessageBox
{
    internal class ConsoleService : IMessageBoxService
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        private bool? injectedDialogResult = null;
        #endregion

        #region ================================================================== CTOR =====================================================================================
        /// <summary>
        /// Overloaded c-tor
        /// </summary>
        /// <param name="_injectedDialogResult">A boolean indicating mock up user interaction result (simulates MessageBoxResult)</param>
        public ConsoleService(bool? _injectedDialogResult)
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
            Console.WriteLine("Message was: " + _message);
            switch (injectedDialogResult)
            {
                case true:
                    Console.WriteLine("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                    Console.WriteLine("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
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
            Console.WriteLine("Title was: " + _title);
            Console.WriteLine("Message was: " + _message);
            switch (injectedDialogResult)
            {
                case true:
                    Console.WriteLine("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                    Console.WriteLine("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
            }
        }

        /// <summary>
        /// Mocks up a new MessageBox interaction and writes at the console the message and the response
        /// </summary>
        /// <param name="_message">The text to display in the custom MessageBox</param>
        /// <param name="title">The Title of the mockup MessageBox dialog</param>
        /// <param name="_buttons">The buttons of the mockup MessageBox dialog</param>
        /// <returns>A <see cref="MessageBoxResult"/> value representing the result of the mockup MessageBox dialog</returns>
        public MessageBoxResult Show(string _message, string title, MessageBoxButton _buttons)
        {
            Console.WriteLine("Title was: " + title);
            Console.WriteLine("Message was: " + _message);
            switch (injectedDialogResult)
            {
                case true:
                    Console.WriteLine("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                    Console.WriteLine("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
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
            Console.WriteLine("Title was: " + _title);
            Console.WriteLine("Message was: " + _message);
            switch (injectedDialogResult)
            {
                case true:
                    Console.WriteLine("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                    Console.WriteLine("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
            }
        }
        #endregion
    }
}
