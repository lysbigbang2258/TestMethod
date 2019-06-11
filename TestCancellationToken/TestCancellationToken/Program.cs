using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TestCancellationToken {
    class Program {
        public static void Main() {
            // Define the cancellation token.
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            Random rnd = new Random();
            object lockObj = new object();

            var tasks = new List<Task<int[]>>();
            TaskFactory factory = new TaskFactory(token);
            for(int taskCtr = 0; taskCtr <= 10; taskCtr++) {
                int iteration = taskCtr + 1;
                tasks.Add(factory.StartNew(() => {
                                               int value;
                                               var values = new int[10];
                                               for(int ctr = 1; ctr <= 10; ctr++) {
                                                   lock(lockObj) {
                                                       value = rnd.Next(1, 101);
                                                   }
                                                   if (value == 0) {
                                                       source.Cancel();
                                                       Console.WriteLine("Cancelling at task {0}", iteration);
                                                       break;
                                                   }
                                                   values[ctr - 1] = value;
                                               }
                                               return values;
                                           }, token));
            }
            try {
                Task<double> fTask = factory.ContinueWhenAll(tasks.ToArray(), x => {
                                                                                  Console.WriteLine("Calculating overall mean...");
                                                                                  long sum = 0;
                                                                                  int n = 0;
                                                                                  foreach(var t in x) {
                                                                                      foreach(int r in t.Result) {
                                                                                          sum += r;
                                                                                          n++;
                                                                                      }
                                                                                  }
                                                                                  return sum / (double) n;
                                                                              }, token);
                Console.WriteLine("The mean is {0}.", fTask.Result);
            }
            catch(AggregateException ae) {
                foreach(Exception e in ae.InnerExceptions) {
                    if (e is TaskCanceledException) {
                        Console.WriteLine("Unable to compute mean: {0}", ((TaskCanceledException) e).Message);
                    }
                    else {
                        Console.WriteLine("Exception: " + e.GetType().Name);
                    }
                }
            }
            finally {
                source.Dispose();
            }
            Console.ReadKey();
        }
    }
}
