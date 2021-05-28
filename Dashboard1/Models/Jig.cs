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
        private string _ProductCode;
        public string ProductCode
        {
            get => _ProductCode;
            set { _ProductCode = value; OnPropertyChanged("ProductCode"); }
        }
        private string _sequenceLed;
        public string sequenceLed
        {
            get => _sequenceLed;
            set { _sequenceLed = value; OnPropertyChanged("sequenceLed"); }
        }
    }    
}
