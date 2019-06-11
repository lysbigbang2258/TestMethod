using System;
using System.Reflection;

namespace TestWorkThreadByDelegate
{
    class Program
    {
        delegate void MyDelegate();
        static void Main(string[] args)
        {
            MyDelegate delegate1 = new MyDelegate(myMethod);
            //显示委托类的几个方法成员     
            var methods = delegate1.GetType().GetMethods();
            if (methods != null)
                foreach (var info in methods)
                    Console.WriteLine(info.Name);
            Console.ReadKey(); 
        }

        static void myMethod() {
            Console.WriteLine("Fuck");
        }
    }
   
}
