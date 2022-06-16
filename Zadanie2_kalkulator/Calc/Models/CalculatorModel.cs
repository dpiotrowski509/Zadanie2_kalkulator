using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2_kalkulator.Calc.Models
{
    public class CalculatorModel : INotifyPropertyChanged
    {
        public string CurrentValue = "0";

        public string LastCalculation = "";
        public string CalculationArea
        {
            get { return CurrentValue; }
            set
            {
                CurrentValue = value;
                OnPropertyChanged();
            }
        }  
        public string LastCalculationEvent
        {
            get { return LastCalculation; }
            set
            {
                LastCalculation = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;   
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
