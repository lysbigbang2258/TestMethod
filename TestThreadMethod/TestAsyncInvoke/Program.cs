using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace TestAsyncInvoke {
    class Program {
        delegate string MyDelegate(string name);

        static void Main(string[] args) {
            ThreadMessage("Main Thread");

//            TestMethod2();
//            TestMethod();
//            TestMethod1();
            TestMethod3();
            Console.ReadKey();
        }

        static void TestMethod3() {
            //建立委托
            MyDelegate myDelegate = new MyDelegate(Hello);
            
            //建立Person对象
            Person person = new Person();
            person.Name = "Elva";
            person.Age = 27;
            
            //异步调用委托，输入参数对象person, 获取计算结果
            myDelegate.BeginInvoke("Leslie", new AsyncCallback(NewCompleted), person);            
          
            //在启动异步线程后，主线程可以继续工作而不需要等待
            for (int n = 0; n < 6; n++)
                Console.WriteLine("  Main thread do work!");
            Console.WriteLine("");

            Console.ReadKey();
        }

        static void TestMethod2() {
            //建立委托
            MyDelegate myDelegate = Hello;
            myDelegate.BeginInvoke("Leslie", Completed, null);
            //在启动异步线程后，主线程可以继续工作而不需要等待
            for(int n = 0; n < 6; n++) {
                Console.WriteLine("  Main thread do work!");
            }
            Console.ReadKey();
        }

        static void TestMethod() {
            //建立委托
            MyDelegate myDelegate = Hello;
            //异步调用委托，获取计算结果
            IAsyncResult result = myDelegate.BeginInvoke("Leslie", null, null);
            //完成主线程其他工作
            //  IsCompleted判断方法
//            CheckMethod0(result);
            //  AsyncWaitHandle.WaitOne()判断方法
            CheckMethod1(result);

            string data = myDelegate.EndInvoke(result);
            Console.WriteLine(data);
        }

        static void TestMethod1() {
            //建立委托
            MyDelegate myDelegate = Hello;
            MyDelegate myDelegateNew = HelloNew;
            //异步调用委托，获取计算结果
            IAsyncResult result = myDelegate.BeginInvoke("Leslie", null, null);
            IAsyncResult resultNew = myDelegateNew.BeginInvoke("lili", null, null);
            //WaitHandle[] 判断方法 
            //此处可加入多个检测对象
            WaitHandle[] waitHandleList = {result.AsyncWaitHandle, resultNew.AsyncWaitHandle};
            while(!WaitHandle.WaitAll(waitHandleList, 200))
                Console.WriteLine("Main thead do work!");
//            等待异步方法完成，调用EndInvoke(IAsyncResult)获取运行结果

            string data = myDelegate.EndInvoke(result);
            string datanew = myDelegateNew.EndInvoke(resultNew);

            Console.WriteLine(data);
            Console.WriteLine(datanew);
        }

        static void CheckMethod0(IAsyncResult result) {
            //            IsCompleted判断方法
            while(!result.IsCompleted) {
                Thread.Sleep(200); //虚拟操作
                Console.WriteLine("Main thead do work!");
            }
        }

        static void CheckMethod1(IAsyncResult result)
        {
            //AsyncWaitHandle.WaitOne判断方法
            while (!result.AsyncWaitHandle.WaitOne(200))
            {
                
                Console.WriteLine("Main thead do work!");
            } 
        }
        static string Hello(string name) {
            ThreadMessage("Async Thread");
            Thread.Sleep(2000); //虚拟异步工作
            return "Hello " + name;
        }

        static string HelloNew(string name) {
            ThreadMessage("New Async Thread");
            Thread.Sleep(4000); //虚拟异步工作
            return "Hello New" + name;
        }

        //回调函数
        static void Completed(IAsyncResult result) {
            ThreadMessage("Async Completed");

            //获取委托对象，调用EndInvoke方法获取运行结果
            AsyncResult _result = (AsyncResult) result;
            MyDelegate myDelegate = (MyDelegate) _result.AsyncDelegate;
            string data = myDelegate.EndInvoke(_result);
            Console.WriteLine(data);
        }
        static void NewCompleted(IAsyncResult result)
        {
            ThreadMessage("Async Completed");

            //获取委托对象，调用EndInvoke方法获取运行结果
            AsyncResult _result = (AsyncResult)result;
            MyDelegate myDelegate = (MyDelegate)_result.AsyncDelegate;
            string data = myDelegate.EndInvoke(_result);
            //获取Person对象
            Person person = (Person)result.AsyncState;//获取用户定义对象
            string message = person.Name + "'s age is " + person.Age.ToString();

            Console.WriteLine(data + "\n" + message);
        }

        //显示当前线程
        static void ThreadMessage(string data) {
            string message = string.Format("{0}\n  ThreadId is:{1}", data, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(message);
        }

        public class Person
        {
            public string Name;
            public int Age;
        }
    }
}
