// 201901279:18 AM
namespace TestFFT {
  using System;
  using System.Collections.Concurrent;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Threading;
  using System.Threading.Tasks;
  using ArrayDisplay.net;

  namespace ArrayDisplay.DataFile
  {
    public class DataFile : IDisposable
    {
      readonly List<byte[]> workSavelist; //保存接收数据
      readonly List<byte[]> origSavelist; //保存接收数据
      ConcurrentQueue<byte[]> origRcvQueue;
      ConcurrentQueue<byte[]> origReadQueue;
      readonly string filepath;
      readonly Semaphore sem_work;
      readonly Semaphore sem_orig;
      readonly AutoResetEvent origResetEvent;
      BinaryWriter br_work;
      BinaryWriter br_orig;
      FileStream fs_work;
      FileStream fs_orig;
      ConcurrentQueue<byte[]> workRcvQueue;
      ConcurrentQueue<byte[]> workReadQueue;
      AutoResetEvent workResetEvent;

      public DataFile()
      {
        origRcvQueue = new ConcurrentQueue<byte[]>();
        origReadQueue = new ConcurrentQueue<byte[]>();

        origResetEvent = new AutoResetEvent(false);
        workRcvQueue = new ConcurrentQueue<byte[]>();
        workReadQueue = new ConcurrentQueue<byte[]>();

        workResetEvent = new AutoResetEvent(false);
        Thread hthread = new Thread(Thread_WorkDataSave) { IsBackground = true };
        hthread.Start();
        string path = Environment.CurrentDirectory;
        filepath = path + "\\";
        Task.Factory.StartNew(Thread_OrigDataSave);

      }

      void Thread_WorkDataSave()
      {
        while (true)
        {
          workResetEvent.WaitOne();
          DateTime dt = DateTime.Now;
          StringBuilder sb = new StringBuilder();
          sb.Append("WorkFile_");
          sb.Append(dt.Year.ToString("d4"));
          sb.Append("-");
          sb.Append(dt.Month.ToString("d2"));
          sb.Append("-");
          sb.Append(dt.Day.ToString("d2"));
          sb.Append("-");
          sb.Append(dt.Hour.ToString("d2"));
          sb.Append("-");
          sb.Append(dt.Minute.ToString("d2"));
          sb.Append("-");
          sb.Append(dt.Second.ToString("d2"));
          sb.Append(".bin");
          var str = sb.ToString();
          if (workRcvQueue.Count >= 1024 * 100 * 4)
          {
              try
              {
                  using (FileStream fs = new FileStream(filepath + str, FileMode.CreateNew, FileAccess.Write))
                  {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                      byte[] temp;
                      for (int i = 0; i <= 1024 * 100 * 4; i++)
                      {
                        workRcvQueue.TryDequeue(out temp);
                        sw.Write(temp);
                      }
                      workResetEvent.Set();
                    }
                  }
              }
              catch (Exception e)
              {
                Console.WriteLine(e);
                throw;
              }
            
            
          }
//          if (workRcvQueue.Count >= 1024 * 100 * 4)
//          {
//            try
//            {
//              fs_work = new FileStream(filepath + str, FileMode.CreateNew, FileAccess.Write);
//              br_work = new BinaryWriter(fs_work);
//            }
//            catch (Exception e)
//            {
//              Console.WriteLine(e);
//              throw;
//            }
//            byte[] temp;
//            for (int i = 0; i <= 1024 * 100 * 4; i++)
//            {
//              workRcvQueue.TryDequeue(out temp);
//              br_work.Write(temp);
//            }
//            workResetEvent.Set();
//          }
        }
      }

      void Thread_OrigDataSave()
      {
        while (true)
        {
          origResetEvent.WaitOne();
          DateTime dt = DateTime.Now;
          StringBuilder sb = new StringBuilder();
          sb.Append("Origfile_");
          sb.Append(dt.Year.ToString("d4"));
          sb.Append("-");
          sb.Append(dt.Month.ToString("d2"));
          sb.Append("-");
          sb.Append(dt.Day.ToString("d2"));
          sb.Append("-");
          sb.Append(dt.Hour.ToString("d2"));
          sb.Append("-");
          sb.Append(dt.Minute.ToString("d2"));
          sb.Append("-");
          sb.Append(dt.Second.ToString("d2"));
          sb.Append(".bin");
          var str = sb.ToString();
          if (origRcvQueue.Count >= 1024 * 10)
          {
            try
            {
              
              fs_orig = new FileStream(filepath + str, FileMode.CreateNew, FileAccess.Write);
              br_orig = new BinaryWriter(fs_orig);
            }
            catch (Exception e)
            {
              Console.WriteLine(e);
              throw;
            }

            byte[] temp;
            for (int i = 0; i <= 1024 * 10; i++)
            {
              origRcvQueue.TryDequeue(out temp);
              br_orig.Write(temp);
            }
            origResetEvent.Set();
          }
        }
      }

      public void EnableWorkSaveFile()
      {
        UdpWaveData.WorkSaveDataEventHandler += WriteSaveData;
      }
      public void EnableOrigSaveFile()
      {
        UdpWaveData.OrigSaveDataEventHandler += WriteOrigData;
      }

      public void DisableSaveFile()
      {
        
      }

      void WriteSaveData(object sender, byte[] data)
      {
        workRcvQueue.Enqueue(data);
        workResetEvent.Set();
      }

      void WriteOrigData(object sender, byte[] data)
      {
        origRcvQueue.Enqueue(data);
        origResetEvent.Set();
      }

      #region IDisposable

      protected virtual void Dispose(bool disposing)
      {
        if (disposing)
        {
          if (fs_work != null) fs_work.Dispose();
          if (br_work != null) br_work.Dispose();
          if (sem_work != null) sem_work.Dispose();
          if (fs_orig != null) fs_orig.Dispose();
          if (br_orig != null) br_orig.Dispose();
          if (sem_orig != null) sem_orig.Dispose();
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

}
