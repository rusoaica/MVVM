/// Written by: Yulia Danilova
/// Creation Date: 19th of November, 2019
/// Purpose: Interface for custom message box Show() method overloads
#region ========================================================================= USING =====================================================================================
using MVVM.ViewModels.Common.Enums;
#endregion

namespace MVVM.ViewModels.Common.Interfaces
{
    public interface IMessageBoxService
    {
        #region ================================================================= METHODS ===================================================================================
        MessageBoxResult Show(string _message);
        MessageBoxResult Show(string _message, string _title);
        MessageBoxResult Show(string _message, string _title, MessageBoxButton _buttons);
        MessageBoxResult Show(string _message, string _title, MessageBoxButton _buttons, MessageBoxImage _image);
        void ChangeInjectedDialogResult(bool? _dialogResult);
        #endregion
    }
}
