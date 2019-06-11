// 2019031111:26 AM

using System;
using GalaSoft.MvvmLight;

namespace TestMVVMFram.ViewModel {
  public class FruitInfoViewModel : ViewModelBase
  {
    public FruitInfoViewModel(String img, String info)
    {
      this.Img = img;
      this.Info = info;
    }

    #region 属性

    private String img;
    /// <summary>
    /// 图片
    /// </summary>
    public String Img
    {
      get { return img; }
      set { img = value; }
    }


    private String info;
    /// <summary>
    /// 信息
    /// </summary>
    public String Info
    {
      get { return info; }
      set { info = value; }
    }


    #endregion
  }
}
