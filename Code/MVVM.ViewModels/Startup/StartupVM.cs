/// Written by: Yulia Danilova
/// Creation Date: 09th of December, 2019
/// Purpose: View Model for the ProductsV Window. Adds new Products.
#region ========================================================================= USING =====================================================================================
using NLog;
using System;
using NLog.Config;
using NLog.Targets;
using System.Threading;
using MVVM.Models.Startup;
using System.Threading.Tasks;
using System.Collections.Generic;
using MVVM.ViewModels.Common.IoC;
using MVVM.ViewModels.Common.MVVM;
using MVVM.ViewModels.Common.Enums;
using System.Collections.ObjectModel;
using MVVM.ViewModels.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace MVVM.ViewModels.Startup
{
    public class StartupVM : BaseModel
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        public static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public static Dictionary<string, IUserInterface> UIDispatcher = new Dictionary<string, IUserInterface>(); // string - the viewmodel type, IUserInterface - the view

        private IDispatcher dispatcher;
        #endregion

        #region ============================================================= BINDING COMMANDS ==============================================================================
        public SyncCommand Window_ContentRendered_Command { get; private set; }
        public IAsyncCommand Whatever_Command { get; private set; }
        public ISyncCommand<ProductM> ProductDelete_Command { get; private set; }
        public ISyncCommand<ProductM> ProductQuantity_KeyUp_Command { get; private set; }
        #endregion

        #region ============================================================ BINDING PROPERTIES =============================================================================
        private ObservableCollection<ProductM> sourceProducts = new ObservableCollection<ProductM>();
        public ObservableCollection<ProductM> SourceProducts
        {
            get { return sourceProducts; }
            set { sourceProducts = value; }
        }

        private ProductM productsSelectedItem = new ProductM();
        public ProductM ProductsSelectedItem
        {
            get { return productsSelectedItem; }
            set { productsSelectedItem = value; }
        }
        #endregion

        #region ================================================================== CTOR =====================================================================================
        /// <summary>
        /// Default C-tor
        /// </summary>
        public StartupVM()
        {
            Whatever_Command = new AsyncCommand(Whatever);
            ProductDelete_Command = new SyncCommand<ProductM>(RemoveProduct);
            Window_ContentRendered_Command = new SyncCommand(Window_ContentRendered);
            ProductQuantity_KeyUp_Command = new SyncCommand<ProductM>(ProductQuantity_KeyUp);

            SourceProducts = new ObservableCollection<ProductM>()
            {
                new ProductM() { ArticleNumber = "ABC001", Name = "Product A", Quantity = 10, Price = 9.99M, TotalPrice = 99.9M },
                new ProductM() { ArticleNumber = "ABC002", Name = "Product B", Quantity = 75, Price = 16.9M, TotalPrice = 1267.5M },
                new ProductM() { ArticleNumber = "ABC003", Name = "Product C", Quantity = 20, Price = 5.49M, TotalPrice = 89.8M },
            };
        }

        #endregion

        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Initializes NLog
        /// </summary>
        private void SetLoggingEnvironment()
        {
            // create NLog configuration  
            LoggingConfiguration config = new LoggingConfiguration();
            FileTarget fileTarget = new FileTarget("target2")
            {
                FileName = (AppDomain.CurrentDomain.BaseDirectory + @"Logs\" + DateTime.Now.ToString("yyMMdd") + ".log").Replace("\\", "/"),
                Layout = "${longdate} ${level} ${message}  ${exception}"
            };
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));
            config.AddTarget(fileTarget);
            LogManager.Configuration = config;
            // enable when debugging NLog itself:
            // LogManager.ThrowExceptions = true;
            logger.Info(Environment.NewLine + Environment.NewLine + Environment.NewLine);
            logger.Info("\tApplication started");
        }

        /// <summary>
        /// Some async method
        /// </summary>
        private async Task Whatever()
        {
            await Task.Delay(1000);
            MsgBox.Show("Apparently, it works!", "Whoa!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            // close the subscribed view
            dispatcher.Dispatch((SendOrPostCallback)delegate 
            { 
                CloseView(); 
            }, true);
        }

        /// <summary>
        /// Calculates the prices of the product identified by <paramref name="_product"/>
        /// </summary>
        /// <param name="_product">The product whose prices are recalculated</param>
        private void CalculatePrices(ProductM _product)
        {
            _product.TotalPrice = _product.Quantity * _product.Price;
        }

        /// <summary>
        /// Deletes a product from the list of products
        /// </summary>
        private void RemoveProduct(ProductM _product)
        {       
            SourceProducts.Remove(_product);
        }
        #endregion

        #region ============================================================= EVENT HANDLERS ================================================================================
        /// <summary>
        /// Handles the KeyUp event of quantity textbox of products
        /// </summary>
        private void ProductQuantity_KeyUp(ProductM _product)
        {
            CalculatePrices(_product);
        }

        /// <summary>
        /// Handles the ContentRendered event of the Window
        /// </summary>
        private void Window_ContentRendered()
        {
            SetLoggingEnvironment();
            dispatcher = DependencyInjection.ServiceProvider.GetService<IDispatcher>();
            // assign the custom message box service
            MsgBox = DependencyInjection.ServiceProvider.GetService<IMessageBoxService>();
            WindowTitle = "MVVM pattern";
        }
        #endregion
    }
}
