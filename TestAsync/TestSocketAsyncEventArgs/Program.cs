using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestSocketAsyncEventArgs {
    public static class Program {
        public static void Main(string[] args) {
            var addressList = Dns.GetHostEntry(Environment.MachineName).AddressList;
            new TcpListener().Listen(new IPEndPoint(addressList[addressList.Length - 1], 9900));

            Console.ReadKey();
        }
    }

    public class TcpListener {
        SocketAsyncEventArgs Args;
        StringBuilder buffers;
        Socket ListenerSocket;

        public void Listen(EndPoint e) {
            //buffer
            buffers = new StringBuilder();
            //socket
            ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ListenerSocket.Bind(e);
            ListenerSocket.Listen(10);
            //异步socket事件
            Args = new SocketAsyncEventArgs();
            Args.Completed += ProcessAccept;
            BeginAccept(Args);
            Console.WriteLine("server run at {0}", e);
        }

        //开始接受
        void BeginAccept(SocketAsyncEventArgs e) {
            e.AcceptSocket = null;
            if (!ListenerSocket.AcceptAsync(e)) {
                ProcessAccept(ListenerSocket, e);
            }
        }

        //接受完毕 开始接收和发送
        void ProcessAccept(object sender, SocketAsyncEventArgs e) {
            Socket s = e.AcceptSocket;
            e.AcceptSocket = null;

            int bufferSize = 10; //1000 * 1024;
            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.Completed += OnIOCompleted;
            args.SetBuffer(new byte[bufferSize], 0, bufferSize);
            args.AcceptSocket = s;
            if (!s.ReceiveAsync(args)) {
                ProcessReceive(args);
            }

            BeginAccept(e);
        }

        //IOCP回调
        void OnIOCompleted(object sender, SocketAsyncEventArgs e) {
            switch(e.LastOperation) {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }

        //接收完毕
        void ProcessReceive(SocketAsyncEventArgs e) {
            if (e.BytesTransferred > 0) {
                if (e.SocketError == SocketError.Success) {
                    //读取
                    string data = Encoding.ASCII.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                    buffers.Append(data);
                    Console.WriteLine("Received:{0}", data);

                    if (e.AcceptSocket.Available == 0) {
                        //读取完毕
                        Console.WriteLine("Receive Complete.Data:{0}", buffers);
                        //重置
                        buffers = new StringBuilder();
                        //发送反馈
                        var sendBuffer = Encoding.ASCII.GetBytes("result from server");
                        e.SetBuffer(sendBuffer, 0, sendBuffer.Length);
                        if (!e.AcceptSocket.SendAsync(e)) {
                            ProcessSend(e);
                        }
                    }
                    else if (!e.AcceptSocket.ReceiveAsync(e)) {
                        ProcessReceive(e);
                    }
                }
            }
        }

        //发送完毕
        void ProcessSend(SocketAsyncEventArgs e) {
            if (e.SocketError == SocketError.Success) {
                if (!e.AcceptSocket.ReceiveAsync(e)) {
                    ProcessReceive(e);
                }
            }
        }
    }
}
