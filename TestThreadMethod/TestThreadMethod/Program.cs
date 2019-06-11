using System;
using System.Threading;

namespace TestThreadMethod {
    class Program {
        static void Main(string[] args) {
            Thread t = new Thread(DoWork);
            t.Name = "八戒";
            t.Start();
            Thread.Sleep(millisecondsTimeout: 10000);
            Console.WriteLine("悟空:八戒，该起床了");
            t.Abort();
        }

        static void DoWork() {
            try {
                while(true) {
                    Console.WriteLine(Thread.CurrentThread.Name + ":呼呼~~~~~");
                    Thread.Sleep(millisecondsTimeout: 1000);
                }
            }
            catch(ThreadAbortException e) {
                Console.WriteLine(Thread.CurrentThread.Name + ":还早呢，我还要再睡会");
                Thread.ResetAbort();
            }
            for(int i = 0; i < 10; i++) {
                Console.WriteLine(Thread.CurrentThread.Name + ":呼呼~~~~~");
                Thread.Sleep(millisecondsTimeout: 1000);
            }
        }
    }
}
