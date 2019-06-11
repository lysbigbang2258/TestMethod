// 201901259:31 AM

using System.Windows;

namespace TestFFT {
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    namespace ArrayDisplay.net {
        public class UdpCommand : IDisposable {
            public UdpCommand() {
                rcvsocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                rcvsocket.ReceiveTimeout = 5000;
                sedsocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                sedsocket.SendTimeout = 5000;
                Init();
            }

            public void RcvThreadStart() {
                Thread thread = new Thread(Thread_Rcv);
                thread.Start();
                thread.IsBackground = true;

            }

            void Thread_Rcv() {
                EndPoint senderRemote = new IPEndPoint(IPAddress.Any, 0);
                var rcvUdpBuffer = new byte[100 * 4];
                int reflag = 0;
                while(true) {
                    int offset = 0;
                    try {
                        reflag = rcvsocket.ReceiveFrom(rcvUdpBuffer, offset, rcvUdpBuffer.Length - offset, SocketFlags.None, ref senderRemote);
                    }
                    catch(Exception e) {
                        Console.WriteLine(e);
                    }
                    if (reflag == 1) {
                        string commadid = Encoding.ASCII.GetString(rcvUdpBuffer, 0, 6);
                        var str = ConstUdpArg.Device_Id.ToString();
                        switch(commadid) { }
                    }
                }


            }

            /// <summary>
            ///     初始化Socket，绑定IP
            /// </summary>
            public void Init() {
                try {
                    SedIp = ConstUdpArg.Src_ComMsgIp;
                    RcvIp = ConstUdpArg.Src_ComDatIp;
                    rcvsocket.Bind(RcvIp);
                    sedsocket.Bind(SedIp);
                    isSocketInit = true;
                }
                catch(Exception e) {
                    Console.WriteLine(@"创建UDP失败...错误为{0}", e);
                    MessageBox.Show(@"创建UDP失败...");
                }
            }

            /// <summary>///切换功能窗口（波形或命令）/// </summary>
            public void SwitchWindow(byte[] cmdBytes) {
                if (!isSocketInit) {
                    //                Init();
                }
                var tempBytes = new byte[8];
                cmdBytes.CopyTo(tempBytes, 0);
                //            Console.WriteLine("切换窗口:{0}", BitConverter.ToString(tempBytes));
                Send(tempBytes, ConstUdpArg.Dst_ComMsgIp);
            }

            #region 数据发送

            /// <summary>
            ///     发送数据
            /// </summary>
            /// <param name="bytes">待发送byte[]数据</param>
            /// <param name="endPoint">下位机IP与端口号</param>
            /// <returns></returns>
            public void Send(byte[] bytes, IPEndPoint endPoint) {
                if (IsProcess) {
                    return;
                }
                try {
                    IsProcess = true;
                    var sendbytes = new byte[bytes.Length];
                    bytes.CopyTo(sendbytes, 0);
                    IPEndPoint sendip = endPoint;
                    EndPoint senderRemote = new IPEndPoint(IPAddress.Any, 0);
                    try {
                        sedsocket.SendTo(sendbytes, sendip);
                    }
                    catch(Exception e) {
                        Console.WriteLine(e);
                    }
                    Console.WriteLine("发送数据:{0}", BitConverter.ToString(sendbytes, 0, sendbytes.Length));

                    var rcvUdpBuffer = new byte[18];
                    try {
                        int offset = 0;
                        while(offset < rcvUdpBuffer.Length) {
                            try {
                                int ret = sedsocket.ReceiveFrom(rcvUdpBuffer, offset, rcvUdpBuffer.Length - offset, SocketFlags.None, ref senderRemote);
                                offset += ret;
                                IsProcess = false;
                            }
                            catch(Exception e) {
                                Console.WriteLine(e);
                            }
                        }
                        var flagBytes = new byte[8];
                        Array.Copy(rcvUdpBuffer, 0, flagBytes, 0, 8);
                        string temp = BitConverter.ToString(flagBytes);
                        //                     Console.WriteLine("接收回传数据:{0}", temp);
                    }
                    catch(Exception e) {
                        Console.WriteLine(e);
                    }
                }
                catch(Exception e) {
                    Console.WriteLine(e);
                    throw;
                }
                finally {
                    IsProcess = false;
                }
            }

            #endregion

            #region Property

            public IPEndPoint SedIp {
                get;
                set;
            }

            public IPEndPoint RcvIp {
                get;
                set;
            }

            bool IsProcess {
                get;
                set;
            }

            bool IsGetRedata {
                get;
                set;
            }

            #endregion

            #region Field

            readonly Socket rcvsocket;
            readonly Socket sedsocket;
            public bool isSocketInit;

            #endregion

            #region IDisposable

            protected virtual void Dispose(bool disposing) {
                if (disposing) {
                    if (rcvsocket != null) {
                        rcvsocket.Dispose();
                    }
                    if (sedsocket != null) {
                        sedsocket.Dispose();
                    }
                }
            }

            /// <inheritdoc />
            public void Dispose() {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion
        }
    }
}

           