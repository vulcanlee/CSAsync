using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 獨佔鎖定Monitor
{
    /// <summary>
    /// 這個範例將會說明當使用獨佔鎖定 Monitor ，如何使用 Monitor.Wait / Monitor.PulseAll 在不同執行緒間的同步執行工作
    /// </summary>
    class Program
    {
        static object locker = new object();
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(MyMethod);
            // 這裡需要等候一段時間，讓工作執行緒已經準備與開始執行了
            Thread.Sleep(300);
            Console.WriteLine($"主執行緒 準備進入到獨佔鎖定狀態 lock (locker)");
            lock (locker) 
            {
                Console.WriteLine($"主執行緒 發出 PulseAll 訊號  Monitor.PulseAll(locker)");
                Monitor.PulseAll(locker);
                Console.WriteLine($"主執行緒 使用 Wait 等候訊號通知  Monitor.Wait(locker)");
                Monitor.Wait(locker);
                Console.WriteLine($"主執行緒 收到來自工作執行緒訊號通知 ");
            }

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void MyMethod(object state)
        {
            Console.WriteLine($"@ 工作執行緒 準備進入獨佔鎖定狀態 lock (locker)");
            lock (locker) 
            {
                while (true)
                {
                    Console.WriteLine($"@ 工作執行緒 等候 主執行緒 的訊號通知 Monitor.Wait(locker)");
                    Monitor.Wait(locker); 
                    Console.WriteLine($"@ 工作執行緒 模擬需要執行");
                    Console.WriteLine($"@ 工作執行緒 發出 PulseAll 訊號 Monitor.PulseAll(locker)");
                    Monitor.PulseAll(locker); 
                }
            }
        }
    }
}
