using System.Windows;

namespace TestFFT.MVVM
{
    /// <summary>
    /// NWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NWindow : Window
    {
        public NWindow()
        {
            InitializeComponent();
            DataContext = new NWindowModel();
        }
    }
}
