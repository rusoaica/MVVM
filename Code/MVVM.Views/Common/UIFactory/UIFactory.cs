/// Written by: Yulia Danilova
/// Creation Date: 30th of October, 2019
/// Purpose: Discrete implementation of IDeterioratedUI interface. 
#region ========================================================================= USING =====================================================================================
using System;
using System.Windows;
using MVVM.ViewModels.Common.Interfaces;
#endregion

namespace MVVM.Views.Common.UIFactory
{
    public class UIFactory : IUserInterface
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        private readonly string instance;
        private Window _wnd;
        #endregion

        #region ================================================================== CTOR =====================================================================================
        /// <summary>
        /// Overload C-tor
        /// </summary>
        /// <param name="_wnd">The name of the window to be instantiated</param>
        public UIFactory(string _wnd)
        {
            instance = _wnd;
        }
        #endregion

        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Closes the Window
        /// </summary>
        public void CloseUI()
        {
            if (_wnd != null)
                _wnd.Close();
        }

        /// <summary>
        /// Shows the window as modal dialog
        /// </summary>
        public void ShowModalUI()
        {
            _wnd = Activator.CreateInstance(Type.GetType(instance, true)) as Window;
            _wnd.ShowDialog();
        }

        /// <summary>
        /// Shows the window as modal dialog
        /// </summary>
        /// <param name="_viewmodel_type">The type of ViewModel to use as window's datacontext</param>
        /// <param name="arguments">Additional arguments to pass to the <paramref name="_viewmodel_type"/></param>
        public void ShowModalUI(string _viewmodel_type, params object[] arguments)
        {
            _wnd = Activator.CreateInstance(Type.GetType(instance, true), arguments) as Window;
            _ = Convert.ChangeType(_wnd.DataContext, Type.GetType(_viewmodel_type));
            _wnd.ShowDialog();
        }

        /// <summary>
        /// Shows the window, modal
        /// </summary>
        /// <param name="id">Parameter to pass into the window ViewModel</param>
        /// <param name="_viewmodel_type">The type of ViewModel to use as window's datacontext</param>
        public void ShowModalUI(string id, string _viewmodel_type)
        {
            _wnd = Activator.CreateInstance(Type.GetType(instance, true)) as Window;
            dynamic _vm = Convert.ChangeType(_wnd.DataContext, Type.GetType(_viewmodel_type));
            _vm.Id = id;
            _wnd.ShowDialog();
        }

        /// <summary>
        /// Shows the window
        /// </summary>
        public void ShowUI()
        {
            _wnd = Activator.CreateInstance(Type.GetType(instance, true)) as Window;
            _wnd.Show();
        }

        /// <summary>
        /// Shows the window
        /// </summary>
        /// <param name="id">Parameter to pass into the window ViewModel</param>
        /// <param name="_viewmodel_type">The type of ViewModel to use as window's datacontext</param>
        public void ShowUI(string id, string _viewmodel_type)
        {
            _wnd = Activator.CreateInstance(Type.GetType(instance, true)) as Window;
            dynamic _vm = Convert.ChangeType(_wnd.DataContext, Type.GetType(_viewmodel_type));
            _vm.Id = id;
            _wnd.Show();
        }

        /// <summary>
        /// Shows the window with specified arguments
        /// </summary>
        /// <param name="_viewmodel_type">The type of ViewModel to use as window's datacontext</param>
        /// <param name="arguments">Additional arguments to pass to the <paramref name="_viewmodel_type"/></param>
        public void ShowUI(string _viewmodel_type, params object[] arguments)
        {
            _wnd = Activator.CreateInstance(Type.GetType(instance, true), arguments) as Window;
            _ = Convert.ChangeType(_wnd.DataContext, Type.GetType(_viewmodel_type));
            _wnd.Show();
        }
        #endregion
    }
}
