using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TestUICancelThread {
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        CancellationTokenSource tokenSource;

        void Start_OnClick(object sender, RoutedEventArgs e) {
            tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;
            Task.Run(() => {
                         while(!ct.IsCancellationRequested) {
                             Console.WriteLine(DateTime.Now);
                             Thread.Sleep(1000);
                         }
                         if (ct.IsCancellationRequested)
                         {
                             // Clean up here, then...
                             ct.ThrowIfCancellationRequested();
                         }
                     }, ct);
        }

        void Cancel_Onclick(object sender, RoutedEventArgs e) {
            if (tokenSource!=null)
            {
                tokenSource.Cancel();
                Console.WriteLine("Cancel the Operation");
            }
            
            
        }
    }
}
