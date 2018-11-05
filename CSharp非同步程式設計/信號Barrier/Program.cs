using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 信號Barrier
{
    class Program
    {
        private static Barrier MySyncEvent = new Barrier(4, x =>
        {
            Console.WriteLine("所有執行緒都執行完成，需要重新開始");
            
        });
        static void Main(string[] args)
        {
            for (int i = 1; i <= 4; i++)
            {
                ThreadPool.QueueUserWorkItem(MyMethod, i);
            }

            //Console.WriteLine("按下任一按鍵，送出收到訊號狀態");
            //Console.ReadKey();
            //MySyncEvent.
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void MyMethod(object state)
        {
            for (int i = 0; i < 5; i++)
            {
                //MySyncEvent.SignalAndWait();
                Console.WriteLine($"@ 工作執行緒{state} 準備等候訊號通知");
                Console.WriteLine($"@ 工作執行緒{state} 模擬需要執行");
                Thread.Sleep(1000);
                MySyncEvent.SignalAndWait();
            }
        }
    }
}
