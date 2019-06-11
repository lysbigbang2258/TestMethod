using System.Windows;
using System.Windows.Media;

namespace SimpleMVVM
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void DisplayUi(CustomerViewModel o)
        {
            lblName.Content = o.TxtCustomerName;
            lblAmount.Content = o.TxtAmount;
            BrushConverter brushconv = new BrushConverter();
            lblBuyingHabits.Background = brushconv.ConvertFromString(o.LblAmountColor) as SolidColorBrush;
            chkMarried.IsChecked = o.IsMarried;
        }
    }
}
