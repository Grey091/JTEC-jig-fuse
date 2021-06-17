using Dashboard1.Commands;
using Dashboard1.Models;
using Dashboard1.Services;
using Dashboard1.Stored;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Dashboard1.ViewModel
{
    class MaintModeVM : BaseVM
    {      
        public MaintModeVM(NavigationStore navigationStore)
        {
           
        }       
        private ObservableCollection<Jig> _ListData; 
        public ObservableCollection<Jig> ListData
        {
            get => _ListData;
            set
            {
                _ListData = value;
                OnPropertyChanged();
            }
        }
        private string _path;
        public string Path { get => _path; set { _path = value; OnPropertyChanged("Path"); } }
        private double _DismissButtonProgress;
        public double DismissButtonProgress
        {
            get => _DismissButtonProgress;
            set { _DismissButtonProgress = value; OnPropertyChanged("DismissButtonProgress"); }
        }
        public ICommand UpdateDataFromExcel { get; set; }
        public ICommand BrowseCommand { get; set; }
        public MaintModeVM()
        {
            UpdateDataFromExcel = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ListData = ReadMOdelsCodeInExcel(Path);
            });
            BrowseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.Filter = "Excel Worksheets|*.xlsx";
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    string source = dlg.FileName;
                    Path = source;
                }
            });
        }
        public static int FindNumberRowsOfFileExcel(ExcelWorksheet worksheet)
        {
            int numRow = 0;
            int i = 2;
            while (worksheet.Cells[i, 1].Value != null)
            {
                if (worksheet.Cells[i, 1].Value != null)
                    numRow = int.Parse(worksheet.Cells[i, 1].Value.ToString());
                i++;
            }
            return numRow + 2;
        }
        public static ObservableCollection<Jig> ReadMOdelsCodeInExcel(string Path)
        {
            ObservableCollection<Jig> ListData = new ObservableCollection<Jig>();
            //"C:/Users/DELL/Desktop/JTEC-jig fuse/Dashboard1/MSexcelJtec/JtecBook1.xlsx"
            string path = Path;
            FileInfo fileInfo = new FileInfo(path);           
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                var numRow = FindNumberRowsOfFileExcel(worksheet);

                for (int row = 2; row < numRow; row++)
                {
                    var ID = worksheet.Cells[row, 1].Value;
                    var ProductCode = worksheet.Cells[row, 2].Value.ToString();
                    var JigCode = worksheet.Cells[row, 3].Value.ToString();

                    Jig MyJig = new Jig();
                    MyJig.ID = row - 1;
                    MyJig.JigCode = JigCode;
                    MyJig.ProductCode = ProductCode;

                    if (worksheet.Cells[row, 4].Value != null)
                    {
                        var sequenceLed1 = worksheet.Cells[row, 4].Value.ToString();
                        MyJig.sequenceLed1 = sequenceLed1;
                    }
                    else MyJig.sequenceLed1 = "";
                    if (worksheet.Cells[row, 5].Value != null)
                    {
                        var sequenceLed2 = worksheet.Cells[row, 5].Value.ToString();
                        MyJig.sequenceLed2 = sequenceLed2;
                    }
                    else MyJig.sequenceLed2 = "";
                    if (worksheet.Cells[row, 6].Value != null)
                    {
                        var sequenceLed3 = worksheet.Cells[row, 6].Value.ToString();
                        MyJig.sequenceLed3 = sequenceLed3;
                    }
                    else MyJig.sequenceLed3 = "";
                    if (worksheet.Cells[row, 7].Value != null)
                    {
                        var sequenceLed4 = worksheet.Cells[row, 7].Value.ToString();
                        MyJig.sequenceLed4 = sequenceLed4;
                    }
                    else MyJig.sequenceLed4 = "";
                    ListData.Add(MyJig);
                }
            }
            return ListData;
        }
    }
}
