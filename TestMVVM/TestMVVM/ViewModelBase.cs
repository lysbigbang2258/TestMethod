// 2019022210:22 AM

using System.ComponentModel;

namespace TestMVVM {
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
