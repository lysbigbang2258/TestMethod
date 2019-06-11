// 2019022610:30 AM

using System.Collections.ObjectModel;
using System.Windows;

namespace TestFFT.MVVM {
    public class NWindowModel : ViewModelBase
    {
        ObservableCollection<Point> _normalFrequencyData;
        public ObservableCollection<Point> NormalFrequencyData
        {
            get
            {
                return _normalFrequencyData;
            }
            set
            {
                if (_normalFrequencyData == value) {
                    return;
                }
                _normalFrequencyData = value;
                OnPropertyChanged("NormalFrequency");
            }
        }
       
    }
}
