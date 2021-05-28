using Dashboard1.Stored;
using Dashboard1.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Dashboard1
{
    /// <summary>
    /// Interação lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            NavigationStore navigationStore = new NavigationStore();

            navigationStore.CurrentViewModel = new MaintModeVM(navigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowVM(navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
