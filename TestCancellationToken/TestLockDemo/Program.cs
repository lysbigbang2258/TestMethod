using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLockDemo
{
    class Program
    {
        static void Main(string[] args) {
            AccountTest test = new AccountTest();
            test.Main();
            Console.ReadKey();
        }
    }
}
