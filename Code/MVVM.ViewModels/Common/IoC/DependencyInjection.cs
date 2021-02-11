/// Written by: Yulia Danilova
/// Creation Date: 23nd of Jamuary 2021
/// Purpose: Dependency injection service provider
#region ========================================================================= USING =====================================================================================
using System;
using MVVM.ViewModels.Common.MVVM;
using MVVM.ViewModels.Common.Enums;
using MVVM.ViewModels.Common.Logging;
using MVVM.ViewModels.Common.Interfaces;
using MVVM.ViewModels.Common.MessageBox;
using MVVM.ViewModels.Common.Dispatching;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
#endregion

namespace MVVM.ViewModels.Common.IoC
{
    public class DependencyInjection
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        private static IServiceProvider serviceProvider;
        private static readonly IServiceCollection services;
        private static DependencyInjection instance = new DependencyInjection();
        #endregion

        #region =============================================================== PROPERTIES ==================================================================================
        public static IServiceProvider ServiceProvider
        {
            get { return serviceProvider; }
        }

        public static DependencyInjection Instance
        {
            get { return instance; }
        }
        #endregion

        #region ================================================================== CTOR =====================================================================================
        /// <summary>
        /// Default C-tor
        /// </summary>
        static DependencyInjection()
        {
            services = new ServiceCollection();
            // add the custom services to the service collection
            Instance.RegisterCustomServices();
            // add the dispatcher service to the service collection
            Instance.RegisterDispatcherService();
            // add the dialog service to the service collection
            Instance.RegisterDialogServices();
        }
        #endregion

        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Registers custom services into the IServiceCollection
        /// </summary>
        /// <param name="_services">The IServiceCollection to add the services to</param>
        private void RegisterCustomServices()
        {
            // register a logging service
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        /// <summary>
        /// Loads service objects into the ServiceContainer
        /// </summary>
        private void RegisterDialogServices()
        {
            // depending on the specified automation test environment, register a specific dialog service
            switch (BaseModel.testAutomationEnvironment)
            {
                case TestAutomationEnvironment.None:
                    // register a service of type custom MessageBox dialog
                    services.AddTransient<IMessageBoxService, MessageBoxService>();
                    break;
                case TestAutomationEnvironment.Console:
                    // register a service of type console logging
                    services.AddTransient<IMessageBoxService, ConsoleService>();
                    break;
                case TestAutomationEnvironment.NLog:
                    // register a service of type NLog logging
                    services.AddTransient<IMessageBoxService, LoggingService>();
                    break;
            }
        }

        /// <summary>
        /// Registers a dispatcher service
        /// </summary>
        /// <param name="_dispatcher">A specific dispatcher to register</param>
        public void RegisterDispatcherService(Type _dispatcher = null)
        {
            // if no dispatcher is provided, such as in testing environments, add a mock up dispatcher
            if (_dispatcher == null)
                services.AddTransient<IDispatcher, MockDispatcher>();
            else
            {
                // if a specific dispatcher was provided, check if a dispatcher service was already registered
                ServiceDescriptor _descriptor = new ServiceDescriptor(typeof(IDispatcher), _dispatcher, ServiceLifetime.Transient);
                // if a dispatcher service was already registered, replace it with the specific dispatcher provided
                if (_descriptor != null)
                    services.Replace(_descriptor);
                else // a specific dispatcher was provided, and no previous dispatcher service was registered, register the provided one
                    services.AddTransient(typeof(IDispatcher), _dispatcher);
            }
            // rebuild the service provider with the registered services
            BuildServiceProvider();
        }

        /// <summary>
        /// Creates a service provider with the registered services
        /// </summary>
        private void BuildServiceProvider()
        {
            serviceProvider = services.BuildServiceProvider();
        }
        #endregion
    }
}
