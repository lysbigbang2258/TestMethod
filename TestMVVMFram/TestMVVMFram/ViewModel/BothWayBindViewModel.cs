// 201903072:16 PM

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using TestMVVMFram.Model;
using TestMVVMFram.UserControl;

namespace TestMVVMFram.ViewModel {
  public class BothWayBindViewModel : ViewModelBase {
    public BothWayBindViewModel() {
      if (IsInDesignMode)
      {
        UserInfo = new UserInfoModel();
        InitCombbox();
        InitSingleRadio();
        InitCompRadio();
        InitTreeInfo();
        InitCompCheck();
        InitListBoxList();
        InitUCList();
      }
      else
      {

      }
     
    }

    #region 辅助函数

    void InitCombbox() {
      CombboxList = new List<ComplexInfoModel> {
        new ComplexInfoModel {Key = "1", Text = "苹果"},
        new ComplexInfoModel {Key = "2", Text = "香蕉"},
        new ComplexInfoModel {Key = "3", Text = "梨"},
        new ComplexInfoModel {Key = "4", Text = "樱桃"}
      };
    }

    void InitSingleRadio() {
      SingleRadio = "喜欢吃苹果？";
      IsSingleRadioCheck = false;
    }

    void InitCompRadio() {
      RadioButtons = new List<CompBottonModel> {
        new CompBottonModel {Content = "苹果", IsCheck = false},
        new CompBottonModel {Content = "香蕉", IsCheck = false},
        new CompBottonModel {Content = "梨", IsCheck = false},
        new CompBottonModel {Content = "樱桃", IsCheck = false}
      };
    }

    void InitTreeInfo() {
      TreeInfo = new List<TreeNodeModel> {
        new TreeNodeModel {
          NodeID = "1",
          NodeName = "苹果",
          Children =
            new List<TreeNodeModel> {
              new TreeNodeModel {NodeID = "1.1", NodeName = "苹果A"},
              new TreeNodeModel {NodeID = "1.2", NodeName = "苹果B"},
              new TreeNodeModel {
                NodeID = "1.3",
                NodeName = "苹果C",
                Children =
                  new List<TreeNodeModel> {
                    new TreeNodeModel {NodeID = "1.3.1", NodeName = "苹果C1"},
                    new TreeNodeModel {NodeID = "1.3.2", NodeName = "苹果C2"}
                  }
              }
            }
        },
        new TreeNodeModel {
          NodeID = "2",
          NodeName = "香蕉",
          Children =
            new List<TreeNodeModel> {
              new TreeNodeModel {NodeID = "2.1", NodeName = "香蕉A"},
              new TreeNodeModel {NodeID = "2.2", NodeName = "香蕉B"},
              new TreeNodeModel {NodeID = "2.3", NodeName = "香蕉C"}
            }
        }
      };
    }

    void InitCompCheck() {
      CheckButtons = new List<CompBottonModel> {
        new CompBottonModel {Content = "苹果", IsCheck = false},
        new CompBottonModel {Content = "香蕉", IsCheck = false},
        new CompBottonModel {Content = "梨", IsCheck = false},
        new CompBottonModel {Content = "樱桃", IsCheck = false}
      };
    }

    void InitListBoxList() {
      ListBoxData = new ObservableCollection<dynamic> {
        new {Img = "/TestMVVMFram;component/Images/1.jpg", Info = "樱桃"},
        new {Img = "/TestMVVMFram;component/Images/2.jpg", Info = "葡萄"},
        new {Img = "/TestMVVMFram;component/Images/3.jpg", Info = "苹果"},
        new {Img = "/TestMVVMFram;component/Images/4.jpg", Info = "猕猴桃"},
        new {Img = "/TestMVVMFram;component/Images/5.jpg", Info = "柠檬"}
      };
    }

    void InitUCList() {
      UCList = new List<FruitInfoView> {
        new FruitInfoView("/TestMVVMFram;component/Images/1.jpg", "樱桃"),
        new FruitInfoView("/TestMVVMFram;component/Images/2.jpg", "葡萄"),
        new FruitInfoView("/TestMVVMFram;component/Images/3.jpg", "苹果"),
        new FruitInfoView("/TestMVVMFram;component/Images/4.jpg", "猕猴桃"),
        new FruitInfoView("/TestMVVMFram;component/Images/5.jpg", "柠檬")
      };
    }

    #endregion

    #region 命令

    /// <summary>
    ///   单选框命令
    /// </summary>
    RelayCommand radioCheckCommand;

    public RelayCommand RadioCheckCommand {
      get {
        if (radioCheckCommand == null) { radioCheckCommand = new RelayCommand(ExcuteRadioCommand); }
        return radioCheckCommand;
      }
      set {
        radioCheckCommand = value;
      }
    }

    void ExcuteRadioCommand() {
      RadioButton = RadioButtons.First(p => p.IsCheck);
    }

    /// <summary>
    ///   复选框命令
    /// </summary>
    RelayCommand checkCommand;

    public RelayCommand CheckCommand {
      get {
        if (checkCommand == null) { checkCommand = new RelayCommand(ExcuteCheckCommand); }
        return checkCommand;
      }
      set {
        checkCommand = value;
      }
    }

    void ExcuteCheckCommand() {
      CheckInfo = "";
      if (CheckButtons == null || CheckButtons.Count <= 0) { return; }
      var list = CheckButtons.Where(p => p.IsCheck);
      if (!list.Any()) { return; }
      foreach(CompBottonModel l in list) {
        CheckInfo += l.Content + ",";
      }
    }

    #endregion

    #region 属性

    UserInfoModel userInfo;

    /// <summary>
    ///   用户信息
    /// </summary>
    public UserInfoModel UserInfo {
      get {
        return userInfo;
      }
      set {
        userInfo = value;
        RaisePropertyChanged(() => UserInfo);
      }
    }

    #region 下拉框相关

    ComplexInfoModel combboxItem;

    /// <summary>
    ///   下拉框选中信息
    /// </summary>
    public ComplexInfoModel CombboxItem {
      get {
        return combboxItem;
      }
      set {
        combboxItem = value;
        RaisePropertyChanged(() => CombboxItem);
      }
    }

    List<ComplexInfoModel> combboxList;

    /// <summary>
    ///   下拉框列表
    /// </summary>
    public List<ComplexInfoModel> CombboxList {
      get {
        return combboxList;
      }
      set {
        combboxList = value;
        RaisePropertyChanged(() => CombboxList);
      }
    }

    #endregion

    #region 单选框相关

    string singleRadio;
    bool isSingleRadioCheck;

    public string SingleRadio {
      get {
        return singleRadio;
      }
      set {
        singleRadio = value;
        RaisePropertyChanged(() => SingleRadio);
      }
    }

    public bool IsSingleRadioCheck {
      get {
        return isSingleRadioCheck;
      }
      set {
        isSingleRadioCheck = value;
        RaisePropertyChanged(() => IsSingleRadioCheck);
      }
    }

    #endregion

    #region 多选框相关

    /// <summary>
    ///   组合单选框列表
    /// </summary>
    public List<CompBottonModel> RadioButtons {
      get;
      set;
    }

    CompBottonModel radioButton;

    /// <summary>
    ///   组合单选框 选中值
    /// </summary>
    public CompBottonModel RadioButton {
      get {
        return radioButton;
      }
      set {
        radioButton = value;
        RaisePropertyChanged(() => RadioButton);
      }
    }

    #endregion

    #region 复选框

    List<CompBottonModel> checkButtons;

    /// <summary>
    ///   组合复选框
    /// </summary>
    public List<CompBottonModel> CheckButtons {
      get {
        return checkButtons;
      }
      set {
        checkButtons = value;
        RaisePropertyChanged(() => CheckButtons);
      }
    }

    string checkInfo;

    /// <summary>
    ///   确认框选中信息
    /// </summary>
    public string CheckInfo {
      get {
        return checkInfo;
      }
      set {
        checkInfo = value;
        RaisePropertyChanged(() => CheckInfo);
      }
    }

    #endregion

    #region 树控件

    List<TreeNodeModel> treeInfo;

    /// <summary>
    ///   树控件数据信息
    /// </summary>
    public List<TreeNodeModel> TreeInfo {
      get {
        return treeInfo;
      }
      set {
        treeInfo = value;
        RaisePropertyChanged(() => TreeInfo);
      }
    }

    #endregion

    #region ListBox 模板

    IEnumerable listBoxData;

    /// <summary>
    ///   LisBox数据模板
    /// </summary>
    public IEnumerable ListBoxData {
      get {
        return listBoxData;
      }
      set {
        listBoxData = value;
        RaisePropertyChanged(() => ListBoxData);
      }
    }

    #endregion

    #region 用户控件模板列表

    List<FruitInfoView> uCList;

    /// <summary>
    ///   用户控件模板列表
    /// </summary>
    public List<FruitInfoView> UCList {
      get {
        return uCList;
      }
      set {
        uCList = value;
        RaisePropertyChanged(() => UCList);
      }
    }

    #endregion

    #endregion
  }
}
