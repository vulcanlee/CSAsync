using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 巢狀lock
{
    class Program
    {
        private static object locker = new object();
        public static void Main()
        {
            ThreadPool.QueueUserWorkItem(doSomething, "背景執行緒呼叫");
            Thread.Sleep(100);

            Console.WriteLine($"@ 主執行緒 準備進入到獨佔鎖定狀態 lock (locker)");
            lock (locker)
            {
                Console.WriteLine($"@ 主執行緒 要呼叫 doSomething()");
                doSomething("主執行緒呼叫");
                Console.WriteLine($"@ 主執行緒 模擬要執行5秒鐘的時間");
                Thread.Sleep(5000);
            }
            Console.WriteLine($"@ 主執行緒 鎖定正要結束");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }


        private static void doSomething(object state)
        {
            var from = state as string;
            Console.WriteLine($"# 工作執行緒({from}) 準備進入到獨佔鎖定狀態 lock (locker)");
            Thread.Sleep(300);
            lock (locker)
            {
                Console.WriteLine($"# 工作執行緒({from}) 模擬要執行3秒鐘的時間");
                Thread.Sleep(3000);
            }
            Console.WriteLine($"# 工作執行緒({from}) 鎖定正要結束");
        }
    }
}
