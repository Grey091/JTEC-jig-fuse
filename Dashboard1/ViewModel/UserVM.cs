using Dashboard1.Commands;
using Dashboard1.Services;
using Dashboard1.Stored;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dashboard1.ViewModel
{
    public class UserVM : BaseVM
    {        
        private string _Mess1;
        public string Mess1
        {
            get => _Mess1;
            set { _Mess1 = value; OnPropertyChanged("Mess1"); }
        }
        public UserVM(NavigationStore navigationStore)
        {           
        }
        public UserVM()
        {
            
        }
       
    }
}
