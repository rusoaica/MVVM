/// Written by: Yulia Danilova
/// Creation Date: 21th of November, 2019
/// Purpose: Interface for interracting with a View

namespace MVVM.ViewModels.Common.Interfaces
{
    public interface IUserInterface
    {
        #region ================================================================= METHODS ===================================================================================
        void ShowUI();

        void ShowUI(string _id, string _type);

        void ShowUI(string _type, params object[] _arguments);

        void ShowModalUI(string _type, params object[] _arguments);

        void ShowModalUI(string _id, string _type);

        void ShowModalUI();

        void CloseUI();
        #endregion
    }
}
