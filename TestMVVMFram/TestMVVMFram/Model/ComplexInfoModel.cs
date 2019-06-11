// 201903072:41 PM

using System;
using GalaSoft.MvvmLight;

namespace TestMVVMFram.ViewModel {
  public class ComplexInfoModel : ObservableObject
  {
    private String key;
    /// <summary>
    /// Key值
    /// </summary>
    public String Key
    {
      get { return key; }
      set { key = value; RaisePropertyChanged(() => Key); }
    }

    private String text;
    /// <summary>
    /// Text值
    /// </summary>
    public String Text
    {
      get { return text; }
      set { text = value; RaisePropertyChanged(() => Text); }
    }
  }
}
