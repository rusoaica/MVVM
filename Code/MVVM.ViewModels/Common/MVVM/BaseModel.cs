/// Written by: Yulia Danilova
/// Creation Date: 19th of November, 2019
/// Purpose: Base class for MVVM pattern models, contains functionality for interacting with the views
#region ========================================================================= USING =====================================================================================
using System;
using MVVM.Models.Common.MVVM;
using MVVM.ViewModels.Common.Enums;
using MVVM.ViewModels.Common.Interfaces;
#endregion

namespace MVVM.ViewModels.Common.MVVM
{
    public class BaseModel : LightBaseModel
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        public event EventHandler ClosingView;
        public static IMessageBoxService MsgBox;
        public static TestAutomationEnvironment testAutomationEnvironment = TestAutomationEnvironment.None;
        #endregion

        #region ============================================================ BINDING PROPERTIES =============================================================================
        private string windowTitle = "";
        public string WindowTitle
        {
            get { return windowTitle; }
            set { windowTitle = value; Notify(nameof(WindowTitle)); }
        }
        #endregion

        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Event handler for closing Views
        /// </summary>
        protected void CloseView()
        {
            ClosingView?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
