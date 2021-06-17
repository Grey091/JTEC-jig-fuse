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
    }    
}
