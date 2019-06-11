using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TestObservableCollection.Annotations;

namespace TestObservableCollection
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Students> infos = new ObservableCollection<Students>() { 
            new Students(){ Id=1, Age=11, Name="Tom"},
            new Students(){ Id=2, Age=12, Name="Darren"},
            new Students(){ Id=3, Age=13, Name="Jacky"},
            new Students(){ Id=4, Age=14, Name="Andy"}
        };

        ViewModel viewModel = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();

            lbStudent.DataContext = viewModel;
        }

        void Button1_OnClick(object sender, RoutedEventArgs e) {
            infos = new ObservableCollection<Students>() {
                new Students() {Id = 1, Age = 11, Name = "这是改变后的集合"},
                new Students() {Id = 2, Age = 12, Name = "这是改变后的集合"},
                new Students() {Id = 3, Age = 13, Name = "这是改变后的集合"},
                new Students() {Id = 4, Age = 14, Name = "这是改变后的集合"}
            };
            viewModel.StudentList = infos;
        }
       
    }
    public class Students : INotifyPropertyChanged
    {
        string _name;
        public int Id { get; set; }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public int Age { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }   
}
