// 2019030710:24 AM

using System;
using GalaSoft.MvvmLight;

namespace TestMVVMFram.Model {
  public class WelcomeModel : ObservableObject
  {
    private String introduction;
          /// <summary>
          /// 欢迎词
          /// </summary>
          public String Introduction
        {
              get { return introduction; }
              set { introduction = value; RaisePropertyChanged(()=>Introduction); }
          }
  }
}
