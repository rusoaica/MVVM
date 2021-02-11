/// Written by: Yulia Danilova
/// Creation Date: 08th of November, 2019
/// Purpose: View Model for the custom MessageBox dialog
#region ========================================================================= USING =====================================================================================
using System.Reflection;
using MVVM.ViewModels.Startup;
using MVVM.ViewModels.Common.MVVM;
using MVVM.ViewModels.Common.Enums;
using MVVM.ViewModels.Common.Interfaces;
#endregion

namespace MVVM.ViewModels.Common.MessageBox
{
    public class MsgBoxVM : BaseModel
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        private MessageBoxImage messageBoxImage = MessageBoxImage.Information;
        #endregion

        #region ============================================================= BINDING COMMANDS ==============================================================================
        public SyncCommand Yes_Command { get; private set; }
        public SyncCommand No_Command { get; private set; }
        public SyncCommand Copy_Command { get; private set; }
        #endregion

        #region ============================================================ BINDING PROPERTIES ============================================================================= 
        private bool? dialogResult = null;
        public bool? DialogResult
        {
            get { return dialogResult; }
            set { dialogResult = value; Notify(nameof(DialogResult)); }
        }

        private string yesLabel = "";
        public string YesLabel
        {
            get { return yesLabel; }
            set { yesLabel = value; }
        }

        private string noLabel = "";
        public string NoLabel
        {
            get { return noLabel; }
            set { noLabel = value; }
        }

        private string cancelLabel = "";
        public string CancelLabel
        {
            get { return cancelLabel; }
            set { cancelLabel = value; }
        }

        private string prompt = "";
        public string Prompt
        {
            get { return prompt; }
            set { prompt = value; }
        }

        private string messageBoxIcon = "mb_iconasterisk.png";
        public string MessageBoxIcon
        {
            get { return messageBoxIcon; }
            set { messageBoxIcon = value; }
        }

        private bool isNoVisible;
        public bool IsNoVisible
        {
            get { return isNoVisible; }
            set { isNoVisible = value; }
        }

        private bool isCancelVisible;
        public bool IsCancelVisible
        {
            get { return isCancelVisible; }
            set { isCancelVisible = value; }
        }
        #endregion

        #region ================================================================ PROPERTIES =================================================================================
        public IClipboard Clipboard { get; set; }
        #endregion

        #region ================================================================== CTOR =====================================================================================
        /// <summary>
        /// Default c-tor
        /// </summary>
        public MsgBoxVM()
        {
            No_Command = new SyncCommand(No);
            Yes_Command = new SyncCommand(Yes);
            Copy_Command = new SyncCommand(Copy);
        }
        #endregion

        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Sets the DialogResult value of the MessageBox to True
        /// </summary>
        private void Yes()
        {
            DialogResult = true;
        }

        /// <summary>
        /// Sets the DialogResult value of the MessageBox to False
        /// </summary>
        private void No()
        {
            DialogResult = false;
        }

        /// <summary>
        /// Copies the prompt of the MessageBox dialog into the Cache memory
        /// </summary>
        private void Copy()
        {
            Clipboard?.SetText(Prompt);
        }

        /// <summary>
        /// Shows a new instance of the MessageBox dialog
        /// </summary>
        /// <param name="text">The text to be displayed inside the MessageBox</param>
        /// <returns>A <see cref="System.Nullable{System.Boolean}"/> representing the DialogResult of the MessageBox window</returns>
        public MessageBoxResult Show(string text)
        {
            Prompt = text;
            YesLabel = "OK";
            IsNoVisible = false;
            IsCancelVisible = false;
            if (StartupVM.UIDispatcher.ContainsKey(nameof(MsgBoxVM)) && testAutomationEnvironment == TestAutomationEnvironment.None)
                StartupVM.UIDispatcher[nameof(MsgBoxVM)].ShowModalUI(typeof(MsgBoxVM).Namespace + "." + nameof(MsgBoxVM) + ", " + Assembly.GetExecutingAssembly().GetName().Name, this);
            return DialogResult == true ? MessageBoxResult.OK : MessageBoxResult.None;
        }

        /// <summary>
        /// Shows a new instance of the MessageBox dialog
        /// </summary>
        /// <param name="text">The text to be displayed inside the MessageBox</param>
        /// <param name="caption">The text displayed on the title bar of the MessageBox</param>
        /// <returns>A <see cref="System.Nullable{System.Boolean}"/> representing the DialogResult of the MessageBox window</returns>
        public MessageBoxResult Show(string text, string caption)
        {
            Prompt = text;
            YesLabel = "OK";
            IsNoVisible = false;
            IsCancelVisible = false;
            WindowTitle = caption;
            if (StartupVM.UIDispatcher.ContainsKey(nameof(MsgBoxVM)) && testAutomationEnvironment == TestAutomationEnvironment.None)
                StartupVM.UIDispatcher[nameof(MsgBoxVM)].ShowModalUI(typeof(MsgBoxVM).Namespace + "." + nameof(MsgBoxVM) + ", " + Assembly.GetExecutingAssembly().GetName().Name, this);
            return DialogResult == true ? MessageBoxResult.OK : MessageBoxResult.None;
        }

        /// <summary>
        /// Shows a new instance of the MessageBox dialog
        /// </summary>
        /// <param name="text">The text to be displayed inside the MessageBox</param>
        /// <param name="caption">The text displayed on the title bar of the MessageBox</param>
        /// <param name="messageType">The type of the MessageBox, which determines what buttons are visibile and their captions</param>
        /// <returns>A <see cref="System.Nullable{System.Boolean}"/> representing the DialogResult of the MessageBox window</returns>
        public MessageBoxResult Show(string text, string caption, MessageBoxButton messageType)
        {
            switch (messageType)
            {
                case MessageBoxButton.OK:
                    YesLabel = "OK";
                    IsNoVisible = false;
                    IsCancelVisible = false;
                    break;
                case MessageBoxButton.YesNo:
                    YesLabel = "Da";
                    NoLabel = "Nu";
                    IsNoVisible = true;
                    IsCancelVisible = false;
                    break;
                case MessageBoxButton.YesNoCancel:
                    YesLabel = "Da";
                    NoLabel = "Nu";
                    CancelLabel = "Anulare";
                    IsNoVisible = true;
                    IsCancelVisible = true;
                    break;
                case MessageBoxButton.OKCancel:
                    YesLabel = "OK";
                    NoLabel = "Anulare";
                    IsNoVisible = true;
                    IsCancelVisible = false;
                    break;
            }
            Prompt = text;
            WindowTitle = caption;
            if (StartupVM.UIDispatcher.ContainsKey(nameof(MsgBoxVM)) && testAutomationEnvironment == TestAutomationEnvironment.None)
                StartupVM.UIDispatcher[nameof(MsgBoxVM)].ShowModalUI(typeof(MsgBoxVM).Namespace + "." + nameof(MsgBoxVM) + ", " + Assembly.GetExecutingAssembly().GetName().Name, this);
            switch (messageType)
            {
                case MessageBoxButton.OK:
                    return DialogResult == true ? MessageBoxResult.OK : MessageBoxResult.None;
                case MessageBoxButton.YesNo:
                    return DialogResult == true ? MessageBoxResult.Yes : MessageBoxResult.No;
                case MessageBoxButton.YesNoCancel:
                    return DialogResult == true ? MessageBoxResult.Yes : (DialogResult == false ? MessageBoxResult.No : MessageBoxResult.Cancel);
                case MessageBoxButton.OKCancel:
                    return DialogResult == true ? MessageBoxResult.OK : MessageBoxResult.Cancel;
                default:
                    return MessageBoxResult.None;
            }
        }

        /// <summary>
        /// Shows a new instance of the MessageBox dialog
        /// </summary>
        /// <param name="text">The text to be displayed inside the MessageBox</param>
        /// <param name="caption">The text displayed on the title bar of the MessageBox</param>
        /// <param name="messageType">The type of the MessageBox, which determines what buttons are visibile and their captions</param>
        /// <param name="image">The icon image of the MessageBox</param>
        /// <returns>A <see cref="System.Nullable{System.Boolean}"/> representing the DialogResult of the MessageBox window</returns>
        public MessageBoxResult Show(string text, string caption, MessageBoxButton messageType, MessageBoxImage image)
        {
            switch (messageType)
            {
                case MessageBoxButton.OK:
                    YesLabel = "OK";
                    IsNoVisible = false;
                    IsCancelVisible = false;
                    break;
                case MessageBoxButton.YesNo:
                    YesLabel = "Da";
                    NoLabel = "Nu";
                    IsNoVisible = true;
                    IsCancelVisible = false;
                    break;
                case MessageBoxButton.YesNoCancel:
                    YesLabel = "Da";
                    NoLabel = "Nu";
                    CancelLabel = "Anulare";
                    IsNoVisible = true;
                    IsCancelVisible = true;
                    break;
                case MessageBoxButton.OKCancel:
                    YesLabel = "OK";
                    NoLabel = "Anulare";
                    IsNoVisible = true;
                    IsCancelVisible = false;
                    break;
            }
            switch (image)
            {
                case MessageBoxImage.Asterisk:
                    MessageBoxIcon = "mb_iconasterisk.png";
                    break;
                case MessageBoxImage.Exclamation:
                    MessageBoxIcon = "mb_iconexclamation.png";
                    break;
                case MessageBoxImage.Hand:
                    MessageBoxIcon = "mb_iconhand.png";
                    break;
                case MessageBoxImage.Question:
                    MessageBoxIcon = "mb_iconquestion.png";
                    break;
            }
            Prompt = text;
            WindowTitle = caption;
            messageBoxImage = image;
            if (StartupVM.UIDispatcher.ContainsKey(nameof(MsgBoxVM)) && testAutomationEnvironment == TestAutomationEnvironment.None)
                StartupVM.UIDispatcher[nameof(MsgBoxVM)].ShowModalUI(typeof(MsgBoxVM).Namespace + "." + nameof(MsgBoxVM) + ", " + Assembly.GetExecutingAssembly().GetName().Name, this);
            switch (messageType)
            {
                case MessageBoxButton.OK:
                    return DialogResult == true ? MessageBoxResult.OK : MessageBoxResult.None;
                case MessageBoxButton.YesNo:
                    return DialogResult == true ? MessageBoxResult.Yes : MessageBoxResult.No;
                case MessageBoxButton.YesNoCancel:
                    return DialogResult == true ? MessageBoxResult.Yes : (DialogResult == false ? MessageBoxResult.No : MessageBoxResult.Cancel);
                case MessageBoxButton.OKCancel:
                    return DialogResult == true ? MessageBoxResult.OK : MessageBoxResult.Cancel;
                default:
                    return MessageBoxResult.None;
            }
        }
        #endregion
    }
}
