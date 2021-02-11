/// Written by: Yulia Danilova
/// Creation Date: 09th of November, 2020
/// Purpose: Code behind for the MsgBoxV window
#region ========================================================================= USING =====================================================================================
using System.Windows;
using MVVM.ViewModels.Common.Interfaces;
using MVVM.ViewModels.Common.MessageBox;
#endregion

namespace MVVM.Views.Common.UI
{
    /// <summary>
    /// Interaction logic for MsgBoxV.xaml
    /// </summary>
    public partial class MsgBoxV : Window
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        private readonly IClipboard clipboard = new WindowsClipboard();
        #endregion

        #region ================================================================== CTOR =====================================================================================
        /// <summary>
        /// Overload C-tor
        /// </summary>
        /// <param name="_param">The ViewModel to inject</param>
        public MsgBoxV(MsgBoxVM _param)
        {
            InitializeComponent();
            // inject the system Clipboard wrapper dependency
            _param.Clipboard = clipboard;
            DataContext = _param;
        }

        /// <summary>
        /// Default c-tor
        /// </summary>
        public MsgBoxV()
        {
            InitializeComponent();
            // inject the system Clipboard wrapper dependency
            (DataContext as MsgBoxVM).Clipboard = clipboard;
        }
        #endregion
    }
}
