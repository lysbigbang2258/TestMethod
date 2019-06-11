// 2019022610:26 AM

using System.ComponentModel;

namespace TestFFT.MVVM {
  public class ViewModelBase : INotifyPropertyChanged
  {
      public event PropertyChangedEventHandler PropertyChanged;

      protected virtual void OnPropertyChanged(string propName)
      {
          if (PropertyChanged != null)
          {
              PropertyChanged(this, new PropertyChangedEventArgs(propName));
          }
      }
  }
}
