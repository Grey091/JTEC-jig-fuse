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
        private double _DismissButtonProgress;
        public double DismissButtonProgress
        {
            get => _DismissButtonProgress;
            set { _DismissButtonProgress = value; OnPropertyChanged("DismissButtonProgress"); }
        }
        public ICommand UpdateDataFromExcel { get; set; }
        public MaintModeVM()
        {
            UpdateDataFromExcel = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ListData = ReadMOdelsCodeInExcel();
            });
        }
        public static ObservableCollection<Jig> ReadMOdelsCodeInExcel()
        {
            ObservableCollection<Jig> ListData = new ObservableCollection<Jig>();
            string path = "C:/Users/DELL/Desktop/JTEC-jig fuse/Dashboard1/MSexcelJtec/JtecBook1.xlsx";
            FileInfo fileInfo = new FileInfo(path);

            //ExcelPackage package = new ExcelPackage(fileInfo);
            //ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
            //CultureInfo.CurrentCulture = new CultureInfo("zh-tw");
            //var filePath = FileInputUtil.GetFileInfo("‪MSexcelJtec", "JtecBook1.xlsx").FullName;
            //FileInfo existingFile = new FileInfo(filePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                for (int row = 2; row < 14; row++)
                {
                    var JigCode = worksheet.Cells[row, 2].Value.ToString();
                    var ID = worksheet.Cells[row, 1].Value;
                    var InputDate = worksheet.Cells[row, 3].Value.ToString();

                    Jig MyJig = new Jig();
                    MyJig.ID = row - 1;
                    MyJig.JigCode = JigCode;
                    MyJig.DateInput = InputDate;

                    ListData.Add(MyJig);
                }               
            }
            return ListData;
        }
    }
}
