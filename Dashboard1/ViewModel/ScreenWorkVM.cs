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
        #region Property
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
        private string _sequenceLed1;
        public string sequenceLed1
        {
            get => _sequenceLed1;
            set { _sequenceLed1 = value; OnPropertyChanged("sequenceLed1"); }
        }
        private string _sequenceLed2;
        public string sequenceLed2
        {
            get => _sequenceLed2;
            set { _sequenceLed2 = value; OnPropertyChanged("sequenceLed2"); }
        }
        private string _sequenceLed3;
        public string sequenceLed3
        {
            get => _sequenceLed3;
            set { _sequenceLed3 = value; OnPropertyChanged("sequenceLed3"); }
        }
        private string _sequenceLed4;
        public string sequenceLed4
        {
            get => _sequenceLed4;
            set { _sequenceLed4 = value; OnPropertyChanged("sequenceLed4"); }
        }
        private string _QRcode;
        public string QRcode
        {
            get => _QRcode;
            set { _QRcode = value; OnPropertyChanged("QRcode"); }
        }
        #endregion

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

        List<string> ListJigCode = new List<string> { "PF200-15000", "PF200-10000", "PF210-20000", "PF213-06000", "PF230-10000", "36189-70533" ,
                                                      "63590273", "0300650(x2)0300700(x1)", "0300650(x1)0300700(x1)", "0301460(x2)0301480(x2)",
                                                      "0301564(x2)0301484(x1)", "9681-XC3"};

        public static SerialPort COM_Adruino;        
        List<byte> ListDataToWrite = new List<byte>() {255};        
        public ScreenWorkVM(NavigationStore navigationStore)
        {
           
        }
        public ScreenWorkVM()
        {
            DismissButtonProgress = true;

            string portName = "COM4";
            COM_Adruino = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
            COM_Adruino.ReadTimeout = 2000;
            COM_Adruino.WriteTimeout = 2000;             
            COM_Adruino.Open(); 
                     

            QRcode = "H01039563 00 0002,1,4210645111D3-04,,,,,";
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
                        var ListModels = maint_ModeVM.ListData.ToList<Jig>();

                        foreach (var model in ListModels)
                        {                           
                            if(String.Compare(codeProduct, model.ProductCode) == 0)
                            {
                                codeJig = model.JigCode;
                                var index = (byte)ListJigCode.IndexOf(codeJig);
                                ListDataToWrite.Add(index);

                                var a = codeProduct.ToCharArray().ToList();
                                foreach (var item in a)
                                {
                                    var b = (byte)item;
                                    ListDataToWrite.Add(b);
                                }

                                var s = (byte)('!');
                                ListDataToWrite.Add(s);

                                sequenceLed1 = model.sequenceLed1;
                                if(sequenceLed1 != "")
                                {
                                    AddValueToList(sequenceLed1);
                                }

                                sequenceLed2 = model.sequenceLed2;
                                if (sequenceLed2 != "")
                                {
                                    AddValueToList(sequenceLed2);
                                }

                                sequenceLed3 = model.sequenceLed3;
                                if (sequenceLed3 != "")
                                {
                                    AddValueToList(sequenceLed3);
                                }

                                sequenceLed4 = model.sequenceLed4;
                                if (sequenceLed4 != "")
                                {
                                    AddValueToList(sequenceLed4);
                                }

                                ListDataToWrite.Add(255);                               
                            }
                        }
                    }
                }
                var dataToAdruino = ListDataToWrite.ToArray();
                #endregion               
                if (COM_Adruino.IsOpen == true)
                {
                    COM_Adruino.Write(dataToAdruino, 0, dataToAdruino.Length);
                    //MessageBox.Show("Hi");
                }               
               
                QRcode = "";
                DismissButtonProgress = true;
            });           
            #endregion            
        } 
        public void AddValueToList(string input)
        {
            var seq = input.Split(',');
            foreach (var item in seq)
            {
                var result = StringToByte(item);
                ListDataToWrite.Add(result);
            }
        }
        public byte StringToByte(string input)
        {
            byte result = 0;
            int i = 1;
            var a = input.ToCharArray();
            foreach(var ch in a)
            {
                var num = (byte)ch;
                result += (byte)((num - 48) * Math.Pow(10, i));
                i--;
            }
            return result;
        }
    }
}
