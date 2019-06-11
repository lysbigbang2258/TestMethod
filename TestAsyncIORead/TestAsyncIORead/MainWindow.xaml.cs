using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace TestAsyncIORead {
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        FileTransfer fileTransfer;
        readonly LocalFile localFile;

        public MainWindow() {
            InitializeComponent();
            fileTransfer = new FileTransfer();
            localFile = new LocalFile();
        }

        async void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            OpenFileDialog open = new OpenFileDialog(); //定义打开文本框实体
            open.Title = "打开文件"; //对话框标题
            open.Filter = "波形文件（.txt）|*.txt|所有文件|*.*"; //文件扩展名

            open.InitialDirectory = Environment.CurrentDirectory;

            var fileList = new List<string>();
            var resultList = new List<string>();
            Encoding coding = Encoding.GetEncoding("gb2312");
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK) //打开
            {
                string filename = open.FileName;
                var tmp = new byte[4];
                var result = await localFile.ReadFileAsnc(filename);
                byte[] resultBytes = result;
                for(int i = 0; i < (resultBytes.Length / 4); i++) {
                    tmp[0] = resultBytes[i];
                    tmp[1] = resultBytes[i + 1];
                    tmp[2] = resultBytes[i + 2];
                    tmp[3] = resultBytes[i + 3];
                    string str = coding.GetString(tmp);
                    resultList.Add(str);
                }
                foreach(string s in resultList) {
                    Console.WriteLine(s);
                }
            }
        }
    }
}
