using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 射後不理使用執行緒
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(OnDelegate);

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            ThreadPool.QueueUserWorkItem(OnDelegateWithException);

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void OnDelegateWithException(object state)
        {
            Thread.Sleep(1000);
            throw new Exception("有例外異常發生了");
        }

        private static void OnDelegate(object state)
        {
            Thread.Sleep(1000);
            Console.WriteLine("OnDelegate 方法結束執行了");
        }
    }
}
