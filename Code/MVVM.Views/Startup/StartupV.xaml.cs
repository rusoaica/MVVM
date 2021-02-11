/// Written by: Yulia Danilova
/// Creation Date: 11th of November, 2020
/// Purpose: Code behind for the StartupV class
#region ========================================================================= USING =====================================================================================
using System.Windows;
using MVVM.Views.Common.UI;
using System.Windows.Controls;
using MVVM.ViewModels.Startup;
using MVVM.ViewModels.Common.IoC;
using MVVM.Views.Common.UIFactory;
using MVVM.Views.Common.Dispatching;
using MVVM.ViewModels.Common.MessageBox;
#endregion

namespace MVVM.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StartupV : Window
    {
        #region ================================================================== CTOR =====================================================================================
        /// <summary>
        /// Default C-tor
        /// </summary>
        public StartupV()
        {
            InitializeComponent();
            // register the dispatcher
            DependencyInjection.Instance.RegisterDispatcherService(typeof(ApplicationDispatcher));            
            // map abstract implementation of views to names of viewmodels
            StartupVM.UIDispatcher.Add(nameof(MsgBoxVM), new UIFactory(typeof(MsgBoxV).Namespace + "." + nameof(MsgBoxV)));
            // allow closing the View from the ViewModel, without breaking MVVM patterns
            (DataContext as StartupVM).ClosingView += (sender, e) => Close();
        }
        #endregion

        #region ============================================================= EVENT HANDLERS ================================================================================
        /// <summary>
        /// Handles SizeChanged event of the products listview
        /// </summary>
        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView _list_view = sender as ListView;
            GridView _grid_view = _list_view.View as GridView;
            double _working_width = _list_view.ActualWidth - 35;
            _grid_view.Columns[0].Width = _working_width * 0.30;
            _grid_view.Columns[1].Width = _working_width * 0.35;
            _grid_view.Columns[2].Width = _working_width * 0.10;
            _grid_view.Columns[3].Width = _working_width * 0.10;
            _grid_view.Columns[4].Width = _working_width * 0.10;
            _grid_view.Columns[5].Width = _working_width * 0.05;
        }
        #endregion
    }
}
