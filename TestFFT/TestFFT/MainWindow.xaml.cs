using System;
using System.Linq;
using System.Windows;
using TestFFT.ArrayDisplay.DataFile;
using TestFFT.ArrayDisplay.net;

namespace TestFFT
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isworkSaveFlag;
        DataFile dataFile;

        public MainWindow()
        {
            InitializeComponent();
            Dataproc.PreGraphEventHandler += OnWorkGraph; // Work时域波形事件处理方法连接到事件
            Dataproc.FrapPointGraphEventHandler += OnFrapPointGraph; //使用新FFT频域事件处理
            Dataproc.FrapPointGraphEventHandler += OnMaxFrapPoint; //使用新FFT频域事件处理
            dataFile = new DataFile();
        }

        

        void OnWorkGraph(object sender, float[] e)
        {
            if (e == null)
            {
                return;
            }
            graph_normalTime.Dispatcher.Invoke(() => {
                                                   graph_normalTime.Refresh();
                                                   graph_normalTime.DataSource = e;
                                               });
        }
        void OnFrapPointGraph(object sender, Point[] e) {
            
            graph_normalFrequency.Dispatcher.Invoke(() => {
                                                        graph_normalFrequency.Refresh();
                                                        graph_normalFrequency.DataSource = e;
                                                    });
        }
        void OnMaxFrapPoint(object sender, Point[] e)
        {
            Point maxPoint = new Point(0, 0);
            foreach (Point dPoint in e)
            {
                if (maxPoint.Y < dPoint.Y)
                {
                    maxPoint = dPoint;
                }
            }
            tb_fre.Dispatcher.Invoke(() => {
                                         tb_fre.Text = maxPoint.X.ToString();
                                     });
        }
         void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UdpCommand udpCommand = new UdpCommand();
                udpCommand.SwitchWindow(ConstUdpArg.SwicthToNormalWindow);
                if (NormWaveData == null)
                {
                    NormWaveData = new UdpWaveData();
                    NormWaveData.StartReceiveData(ConstUdpArg.Src_NormWaveIp);
                    btn_normalstart.Content = "停止";
                    NormWaveData.StartRcvEvent.Set();

                }
                else if (NormWaveData != null || NormWaveData.IsBuilded)
                {
                    NormWaveData.Dispose();
                    NormWaveData = null;
                    graph_normalTime.Dispatcher.Invoke(() =>
                                                       {
                                                           graph_normalTime.DataSource = 0;
                                                           graph_normalTime.Refresh();
                                                       });
                    graph_normalFrequency.Dispatcher.Invoke(() =>
                                                            {
                                                                graph_normalFrequency.DataSource = 0;
                                                                graph_normalFrequency.Refresh();
                                                            });
                    
                  
                    btn_normalstart.Content = "启动";
                }
            }
            catch
            {
                Console.WriteLine(@"正常工作波形采集失败...");
                MessageBox.Show(@"正常工作波形采集失败...");
            }
        }

        public UdpWaveData NormWaveData {
            get;
            set;
        }

        void Button_ClickegTwo(object sender, RoutedEventArgs e) {
            DemoMath dmath = new DemoMath();
            dmath.Test();
        }

        void Button_ClickThree(object sender, RoutedEventArgs e) {
            isworkSaveFlag = !isworkSaveFlag;
            if (isworkSaveFlag)
            {
                btnSave.Content = "开始保存";
                dataFile.EnableWorkSaveFile();
            }

            else
            {
                btnSave.Content = "保存数据";
                dataFile.DisableSaveFile();
            }
        }
    }
}
