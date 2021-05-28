using Dashboard1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard1.Models
{
    public class Jig : BaseVM
    {
        private int _ID;
        public int ID
        {
            get => _ID;
            set { _ID = value; OnPropertyChanged("ID"); }
        }
        private string _JigCode;
        public string JigCode
        {
            get => _JigCode;
            set { _JigCode = value; OnPropertyChanged("JigCode"); }
        }
        private string _DateInput;
        public string DateInput
        {
            get => _DateInput;
            set { _DateInput = value; OnPropertyChanged("DateInput"); }
        }
    }
}
