using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TestGetProUI {
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        public static SynchronizationContext SyncContxt = SynchronizationContext.Current;
        

        public MainWindow() {
            InitializeComponent();
        }

        void ChangeText_OnClick(object sender, RoutedEventArgs e) {
            Task.Run(() => {
                         SetText("123");
                     });
            
        }

        public void SetText(string strText) {
            if (!App.IsRunInMainThread) {
                SyncContxt.Post(oo => {
                                    SetText(strText);
                                }, state: null); //可以使用Post也可以使用Send
                return;
            }
            tbx_test.Text = strText;
        }
        public string GetText()
        {
            if (!App.IsRunInMainThread)
            {
                string str = null;
                SyncContxt.Send(oo => {
                                    str = GetText();  
                                },null);　　 //必须要使用Send
                return str;
            }
            return tbx_test.Text;
        }
    }
}
