using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;


namespace TestAsyncIORead {
    /// <summary>
    /// 异步读文件
    /// </summary>
    public class FileTransfer
    {
        public delegate void ReturnEndEvent(object sender, ReturnEndReadEventargs args);
        public FileTransfer()
        {
            BufferSize = 2048;
        }
        public Stream InputStream { get; set; }
        /// <summary>
        /// 每次读取块的大小
        /// </summary>
        public int BufferSize { get; set; }
        /// <summary>
        /// 读取完成后的返回，返回结果中包含在ReturnEndReadEventargs中
        /// </summary>
        public event ReturnEndEvent IsReturnEvent;

        protected virtual void OnIsReturnEvent()
        {
            ReturnEndEvent handler = IsReturnEvent;
            if (handler != null)
                handler(this, new ReturnEndReadEventargs(true, ReadValue.ToString()));
        }
        /// <summary>
        /// 容纳接收数据的缓存
        /// </summary>
        private byte[] buffer;
        /// <summary>
        /// 读取文件的字符串
        /// </summary>
        public StringBuilder ReadValue;
        /// <summary>
        /// 通过异步方式读取文件
        /// </summary>
        /// <param name="fileName">文件全路径</param>
        /// <returns>读取内容</returns>
        public string ReadFileAsync(string fileName)
        {
            ReadValue = new StringBuilder();
            buffer = new byte[BufferSize];
            InputStream = new FileStream(fileName, FileMode.Open, FileAccess.Read,
                FileShare.Read, BufferSize, useAsync: true);
            
            return ReadValue.ToString();
        }

        public void OnCompleteRead(IAsyncResult asyncResult)
        {
            //异步读取一个快，接收数据
            int bytesRead = InputStream.EndRead(asyncResult);
            //如果没有任何字节，则流已达文件结尾
            if (bytesRead > 0)
            {
                //暂停以对模拟对数据块的处理
                Debug.WriteLine("异步线程：已读取一块内容");
                var datastr = Encoding.GetEncoding("gb2312").GetString(buffer, 0, buffer.Length);
                ReadValue.Append(datastr);
                //
                Thread.Sleep(TimeSpan.FromMilliseconds(20));
                //开始读取下一块
                InputStream.BeginRead(buffer, 0, buffer.Length, OnCompleteRead, null);
            }
            else
            {
                //操作结束
                Debug.WriteLine("   异步线程：读取文件结束");
                OnIsReturnEvent();
            }
        }
    }
}