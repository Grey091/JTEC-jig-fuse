using Dashboard1.Commands;
using Dashboard1.Models;
using Dashboard1.Services;
using Dashboard1.Stored;
using Dashboard1.View;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dashboard1.ViewModel
{
    class ScreenWorkVM : BaseVM
    {
        private double _DismissButtonProgress;
        public double DismissButtonProgress
        {
            get => _DismissButtonProgress;
            set { _DismissButtonProgress = value; OnPropertyChanged("DismissButtonProgress"); }
        }
       
        private string _QRcode;
        public string QRcode
        {
            get => _QRcode;
            set { _QRcode = value; OnPropertyChanged("QRcode"); }
        }
        private string _Mess1;
        public string Mess1
        {
            get => _Mess1;
            set { _Mess1 = value; OnPropertyChanged("Mess1"); }
        }
        private string _Mess2;
        public string Mess2
        {
            get => _Mess2;
            set { _Mess2 = value; OnPropertyChanged("Mess2"); }
        }
        private string _Mess3;
        public string Mess3
        {
            get => _Mess3;
            set { _Mess3 = value; OnPropertyChanged("Mess3"); }
        }
        public ICommand LoadDataToJig { get; set; }        

        public static SerialPort COM_Adruino;
        byte[] dataToAdruino = new byte[10];
        public ScreenWorkVM(NavigationStore navigationStore)
        {
           
        }
        public ScreenWorkVM()
        {
            COM_Adruino = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            COM_Adruino.ReadTimeout = 2000;
            COM_Adruino.WriteTimeout = 2000;
            //COM_Adruino.Open();
            
            #region Command
            LoadDataToJig = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if(COM_Adruino.IsOpen == true)
                {
                    COM_Adruino.Write(dataToAdruino, 0, 20);
                    
                }
                Maint_mode maint_ModeWD = new Maint_mode();
               
                if (maint_ModeWD.DataContext == null)
                {
                    return;
                }
                else
                {
                    var maint_ModeVM = maint_ModeWD.DataContext as MaintModeVM;
                    if (maint_ModeVM.ListData != null)
                    {
                        var List = maint_ModeVM.ListData.ToList<Jig>();                      
                        
                        foreach(var item in List)
                        {
                            var a = item.JigCode;
                            MessageBox.Show(a.ToString());
                        }
                    }
                       
                }

                //DismissButtonProgress = 20;
                //Thread.Sleep(2000);
                //DismissButtonProgress = 40;
                //Thread.Sleep(2000);
                //DismissButtonProgress = 80;
                //Thread.Sleep(2000);
                //DismissButtonProgress = 100;
                //Thread.Sleep(2000);
                //DismissButtonProgress = 00;
                QRcode = "";
            });           
            #endregion            
        }      
    }
}
