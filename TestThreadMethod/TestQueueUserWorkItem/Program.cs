using System;
using System.Threading;

namespace TestQueueUserWorkItem {
    class Program {
        static void Main(string[] args) {
            TestQueueUserWorkItemsWithNoParm();
//            TestQueueUserWorkItemsWithParam();
        }

        static void TestQueueUserWorkItemsWithParam() {
            ThreadPool.SetMaxThreads(1000, 1000);
            ThreadMessage("Start");
            ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncCallback), "Hello Elva");
            Console.ReadKey();
        }

        static void TestQueueUserWorkItemsWithNoParm() {
            //把CLR线程池的最大值设置为1000
            ThreadPool.SetMaxThreads(workerThreads: 1000, completionPortThreads: 1000);
            //显示主线程启动时线程池信息
            ThreadMessage("Start");
            //启动工作者线程
            ThreadPool.QueueUserWorkItem(AsyncCallbackNoParam);
            Console.ReadKey();
        }

        static void AsyncCallback(object state) {
            Thread.Sleep(millisecondsTimeout: 200);
            ThreadMessage("AsyncCallback");
            string data = (string)state;
            Console.WriteLine("Async thread do work!\n" + data);
        }
        static void AsyncCallbackNoParam(object state)
        {
            Thread.Sleep(millisecondsTimeout: 200);
            ThreadMessage("AsyncCallback");
            
            Console.WriteLine("Async thread do work!");
        }

        //显示线程现状
        static void ThreadMessage(string data) {
            string message = string.Format("{0}\n  CurrentThreadId is {1}", data, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(message);
        }
    }
}
