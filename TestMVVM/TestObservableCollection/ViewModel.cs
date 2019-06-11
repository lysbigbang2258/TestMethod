// 2019030410:47 AM

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestObservableCollection.Annotations;

namespace TestObservableCollection {
  public class ViewModel:INotifyPropertyChanged {
    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
    private ObservableCollection<Students> studentList;
    public ObservableCollection<Students> StudentList
    {
      get
      {
        return studentList;
      }
      set
      {
        if (studentList != value)
        {
          studentList = value;
          OnPropertyChanged("StudentList");
        }
      }
    }
  }
}
