// 201901259:20 AM

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using TestFFT.ArrayDisplay.net;

namespace TestFFT {
  public class UdpWaveData : IDisposable
  {
    public UdpWaveData()
    {
      try
      {
        waveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        StartRcvEvent = new AutoResetEvent(false);
        IsStopRcved = false;
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }

    public void StartReceiveData(IPEndPoint ip)
    {
      Ip = ip;
      try
      {
        waveSocket.Bind(ip);
        IsBuilded = true;
      }
      catch (Exception e)
      {
        Console.WriteLine(@"创建UDP失败...错误为{0}", e);
        MessageBox.Show(@"创建UDP失败...");
      }
      if (Equals(ip, ConstUdpArg.Src_NormWaveIp))
      {
        WorktInit();
        WaveType = ConstUdpArg.WaveType.Normal;
      }
     
      waveDataproc = new Dataproc();
      waveDataproc.Init(WaveType);
      RcvThread.Start();
    }

    public void StopReceiveData()
    {
      StartRcvEvent.Reset();
      RcvThread.Abort();
    }

    public void RefreshReceiveData()
    {

      StartRcvEvent.Set();

    }
   
    void WorktInit()
    {
      waveSocket.ReceiveBufferSize = ConstUdpArg.WORK_FRAME_NUMS * ConstUdpArg.WORK_FRAME_LENGTH * 2;
      frameNums = ConstUdpArg.WORK_FRAME_NUMS * ConstUdpArg.WORK_FRAME_LENGTH;
      rcvBuf = new byte[ConstUdpArg.WORK_FRAME_LENGTH];
      RcvThread = new Thread(NormalThreadStart) { IsBackground = true, Priority = ThreadPriority.Highest, Name = "WorkWave" };
    }


    void NormalThreadStart()
    {
      IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
      EndPoint senderRemote = remote;
      Console.WriteLine(@"启动UDP线程...");
      while (true)
      {
        StartRcvEvent.WaitOne();
        if (IsStopRcved)
        {
          return;
        }
        int index = 0;
        IsRcving = true;
        while (index < frameNums)
        {
          if (waveSocket == null)
          {
            IsRcving = false;
            break;
          }

          int offset = 0;
          try
          {
            while (offset < rcvBuf.Length)
            {
              int ret = waveSocket.ReceiveFrom(rcvBuf, offset, rcvBuf.Length - offset, SocketFlags.None, ref senderRemote);
              offset += ret;
            }

            if (!Equals(senderRemote, ConstUdpArg.Dst_NormWaveIp))
            {
              Console.WriteLine("接收错误");
              Console.WriteLine(senderRemote.ToString());
              continue;
            }
          }
          catch (Exception e)
          {
            Console.WriteLine(e);
            break;
          }

          if (WorkSaveDataEventHandler != null)
          {
            WorkSaveDataEventHandler(null, rcvBuf);
          }
          PutWorkData(rcvBuf, index++);
          if (index >= frameNums)
          {
            index = 0;
          }
          waveDataproc.WorkBytesEvent.Set();
        }
      }
    }




    /// <summary>
    ///     将WorkWave数据导入Detect_Bytes
    /// </summary>
    /// <param name="buf">一帧数据，长度为1024，每4位表示1个探头数据</param>
    /// <param name="index">帧数</param>
    void PutWorkData(byte[] buf, int index)
    {
      var temp = new byte[4];
      if (!Equals(Ip, ConstUdpArg.Src_NormWaveIp))
      {
        return;
      }
      for (int i = 0; i < (buf.Length / 4); i++)
      {
        Array.Copy(buf, i * 4, temp, 0, temp.Length);
        Array.Copy(temp, 0, waveDataproc.WorkWaveBytes[i], index * 4, temp.Length);
      }
    }

    #region Field



    static readonly LinkedList<Array> linkbuffer = new LinkedList<Array>(); //缓存数据buff
    public static int frameNums; //一帧数据长度
    public static ConstUdpArg.WaveType waveType; //波形数据类型
    readonly int[] delaychannelOffsets = new int[8];
    readonly int[] origchannelOffsets = new int[64];
    byte[] rcvBuf; //接收数据缓存
    readonly Socket waveSocket;
    Dataproc waveDataproc;

    #endregion

    #region 属性

    public bool IsBuilded
    {
      get;
      set;
    }

    public bool IsRcving
    {
      get;
      set;
    }
    public AutoResetEvent StartRcvEvent
    {
      get;
      set;
    }

    public Thread RcvThread
    {
      get;
      private set;
    }

    public IPEndPoint Ip
    {
      get;
      private set;
    }

    public bool IsStopRcved
    {
      get;
      set;
    }

    /// <summary>
    ///     正常工作数据保存方法
    /// </summary>
    public static EventHandler<byte[]> WorkSaveDataEventHandler
    {
      get;
      set;
    }

    /// <summary>
    ///     原始数据保存方法
    /// </summary>
    public static EventHandler<byte[]> OrigSaveDataEventHandler
    {
      get;
      set;
    }

    /// <summary>
    ///     正在传输波形
    /// </summary>
    public ConstUdpArg.WaveType WaveType
    {
      get
      {
        return waveType;
      }
      set
      {
        waveType = value;
      }
    }

    #endregion


    #region IDisposable

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (waveSocket != null) waveSocket.Dispose();
        if (waveDataproc != null) waveDataproc.Dispose();
        if (StartRcvEvent != null) StartRcvEvent.Dispose();
        if (StartRcvEvent != null) StartRcvEvent.Dispose();
        if (RcvThread != null)
        {
          RcvThread.Abort();
          RcvThread = null;
        }
        IsBuilded = false;
        IsStopRcved = true;
        linkbuffer.Clear();
        Console.WriteLine(@"关闭UDP线程...");
      }

    }

    /// <inheritdoc />
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion
  }
}
