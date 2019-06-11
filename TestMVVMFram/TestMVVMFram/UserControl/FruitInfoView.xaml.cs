using System;
using TestMVVMFram.ViewModel;

namespace TestMVVMFram.UserControl
{
  /// <summary>
  /// UserControl1.xaml 的交互逻辑
  /// </summary>
  public partial class FruitInfoView : System.Windows.Controls.UserControl
  {
    public FruitInfoView(String img,String info)
    {
      InitializeComponent();
      this.DataContext = new FruitInfoViewModel(img,info);
    }
  }
}
