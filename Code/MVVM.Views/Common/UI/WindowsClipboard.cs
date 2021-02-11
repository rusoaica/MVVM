/// Written by: Yulia Danilova
/// Creation Date: 09th of November, 2020
/// Purpose: Explicit implementation of abstract IClipboard interface, used in UI environments
#region ========================================================================= USING =====================================================================================
using System.Windows;
using MVVM.ViewModels.Common.Interfaces;
#endregion

namespace MVVM.Views.Common.UI
{
    public class WindowsClipboard : IClipboard
    {
        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Sets the <paramref name="_text"/> string into Windows clipboard memory
        /// </summary>
        /// <param name="_text">The string to be set into Windows clipboard</param>
        public void SetText(string _text)
        {
            Clipboard.SetText(_text);
        }
        #endregion
    }
}
