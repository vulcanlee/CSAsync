using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 信號ManualResetEvent
{
    class Program
    {
        private static ManualResetEvent MySyncEvent = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            for (int i = 1; i <= 4; i++)
            {
                ThreadPool.QueueUserWorkItem(MyMethod, i);
            }

            Console.WriteLine("按下任一按鍵，送出收到訊號狀態");
            Console.ReadKey();
            MySyncEvent.Set();
            Console.WriteLine("按下任一按鍵，把ManualResetEvent設定為未收到信號的狀態");
            Console.ReadKey();
            MySyncEvent.Reset();
            Console.WriteLine("按下任一按鍵，送出收到訊號狀態");
            Console.ReadKey();
            MySyncEvent.Set();
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void MyMethod(object state)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"@ 工作執行緒{state} 準備等候訊號通知");
                MySyncEvent.WaitOne();
                Console.WriteLine($"@ 工作執行緒{state} 模擬需要執行");
                Thread.Sleep(1000);
            }
        }
    }
}
