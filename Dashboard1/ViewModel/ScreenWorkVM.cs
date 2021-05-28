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
        private bool _DismissButtonProgress;
        public bool DismissButtonProgress
        {
            get => _DismissButtonProgress;
            set { _DismissButtonProgress = value; OnPropertyChanged("DismissButtonProgress"); }
        }
        private string _codeProduct;
        public string codeProduct
        {
            get => _codeProduct;
            set { _codeProduct = value; OnPropertyChanged("codeProduct"); }
        }
        private string _codeJig;
        public string codeJig
        {
            get => _codeJig;
            set { _codeJig = value; OnPropertyChanged("codeJig"); }
        }
        private string _sequenceLed;
        public string sequenceLed
        {
            get => _sequenceLed;
            set { _sequenceLed = value; OnPropertyChanged("sequenceLed"); }
        }
        private string _QRcode;
        public string QRcode
        {
            get => _QRcode;
            set { _QRcode = value; OnPropertyChanged("QRcode"); }
        }
        #region mess
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
        #endregion
        public ICommand LoadDataToJig { get; set; }      

        public static SerialPort COM_Adruino;
        byte[] dataToAdruino = new byte[10];
        public ScreenWorkVM(NavigationStore navigationStore)
        {
           
        }
        public ScreenWorkVM()
        {
            DismissButtonProgress = true;

            COM_Adruino = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            COM_Adruino.ReadTimeout = 2000;
            COM_Adruino.WriteTimeout = 2000;
            //COM_Adruino.Open();

            QRcode = "H01039563 00 0002,1,4210645111D2-04,,,,,";
            //MessageBox.Show("Hi");
            #region Command
            LoadDataToJig = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                DismissButtonProgress = false;
                codeProduct = QRcode.Substring(20, 15);

                #region FindJIGcode
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
                        var ListJIG = maint_ModeVM.ListData.ToList<Jig>();

                        foreach (var item in ListJIG)
                        {                           
                            if(String.Compare(codeProduct, item.ProductCode) == 0)
                            {
                                codeJig = item.JigCode;
                                sequenceLed = item.sequenceLed;
                            }
                        }
                    }
                }
                #endregion

                if (COM_Adruino.IsOpen == true)
                {
                    dataToAdruino[0] = 255;
                    //DefineCodeProduct(QRcode);

                    //COM_Adruino.Write(dataToAdruino, 0, 20);
                    //MessageBox.Show("Hi");

                }               
               
                QRcode = "";
                DismissButtonProgress = true;
            });           
            #endregion            
        } 
        public string DefineCodeJIG(string codeProduct, List<Jig> jigs)
        {
            //H01039563 00 0002,1,4210645111D2-04,,,,,
            string codeJig = null;
            foreach(var item in jigs)
            {
                var check = String.Compare(codeProduct, item.ProductCode);
                if (check == 0)
                    codeJig = item.JigCode;
            }
            return codeJig;
        }
        public List<int> DefineCodeProduct(string codeProduct)
        {
            //H01039563 00 0002,1,4210645111D2-04,,,,,            
            List<int> ProductCodeArr = new List<int>();
            int i = 0;
            foreach(var ch in codeProduct)
            {
                int a = ch;
                ProductCodeArr.Add(a);
                i++;
            }
            return ProductCodeArr;
        }
        public string DefineColorOfLed(string codeProduct, List<Jig> jigs)
        {
            //H01039563 00 0002,1,4210645111D2-04,,,,,
            string sequenceLed = null;
            foreach (var item in jigs)
            {
                var check = String.Compare(codeProduct, item.ProductCode);
                if (check == 0)
                    sequenceLed = item.sequenceLed;
            }
            return sequenceLed;
        }
        public List<int> DefineColorOfLedd(string codeProduct)
        {
            //H01039563 00 0002,1,4210645111D2-04,,,,,
            List<int> LedArr = new List<int>();

            return LedArr;
        }
    }
}
