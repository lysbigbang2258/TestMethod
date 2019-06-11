// 2019030710:32 AM

using GalaSoft.MvvmLight;
using TestMVVMFram.Model;

namespace TestMVVMFram.ViewModel{
  public class WelcomeViewModel:ViewModelBase  {
    /// <summary>
       /// 构造函数
         /// </summary>
         public WelcomeViewModel()
         {
             Welcome = new WelcomeModel() { Introduction = "Hello World！" };
         }
     #region 属性
 
         private WelcomeModel welcome;
         /// <summary>
         /// 欢迎词属性
         /// </summary>
         public WelcomeModel Welcome
         {
             get { return welcome; }
             set { welcome = value; RaisePropertyChanged(()=>Welcome); }
         }
         #endregion
  }
}
