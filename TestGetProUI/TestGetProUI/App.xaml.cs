using System.Threading;
using System.Windows;

namespace TestGetProUI {
    /// <summary>
    ///     App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        static volatile int s_nMainThreadID = Thread.CurrentThread.ManagedThreadId;

        public static bool IsRunInMainThread {
            get {
                return Thread.CurrentThread.ManagedThreadId == s_nMainThreadID;
            }
        }
    }
}
