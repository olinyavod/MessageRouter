/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Melomans.Windows"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using Autofac;
using Autofac.Extras.CommonServiceLocator;
using GalaSoft.MvvmLight.Messaging;
using MessageRouter.Simple.Network;
using MessageRouter.Simple.Service;
using Microsoft.Practices.ServiceLocation;

namespace MessageRouter.Simple.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private static IContainer _container;
        private static AutofacServiceLocator _serviceLocator;
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            if (_container == null)
            {
                ContainerBuilder builder = new ContainerBuilder();
                builder.RegisterModule<NetworkModule>();
                builder.RegisterType<MainViewModel>().SingleInstance();
                builder.RegisterType<Messenger>()
                    .As<IMessenger>()
                    .SingleInstance();
                builder.RegisterType<UsersService>()
                    .SingleInstance();
                _container = builder.Build();
                builder = new ContainerBuilder();
                builder.RegisterInstance(_container)
                    .ExternallyOwned();
                builder.Update(_container);
                _serviceLocator = new AutofacServiceLocator(_container);

            }

            ServiceLocator.SetLocatorProvider(() => _serviceLocator);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}