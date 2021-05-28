using Dashboard1.Commands;
using Dashboard1.Services;
using Dashboard1.Stored;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dashboard1.ViewModel
{
    class MainWindowVM : BaseVM
    {
        private readonly NavigationStore _navigationStore;
        public BaseVM CurrentViewModel => _navigationStore.CurrentViewModel;       
        public ICommand NavigateMaintModeCommand { get; }
        public ICommand NavigateScreenWorkCommand { get; }
        public ICommand NavigateUserCommand { get; }        
        public MainWindowVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
           
            NavigateMaintModeCommand = new NavigateCommand<MaintModeVM>(new NavigationService<MaintModeVM>(
               navigationStore, () => new MaintModeVM(navigationStore)));
            NavigateScreenWorkCommand = new NavigateCommand<ScreenWorkVM>(new NavigationService<ScreenWorkVM>(
               navigationStore, () => new ScreenWorkVM(navigationStore)));
            NavigateUserCommand = new NavigateCommand<UserVM>(new NavigationService<UserVM>(
               navigationStore, () => new UserVM(navigationStore)));           
        }
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        public MainWindowVM()
        {

        }
    }
}
