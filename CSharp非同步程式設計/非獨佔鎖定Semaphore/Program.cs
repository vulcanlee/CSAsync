using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 非獨佔鎖定Semaphore
{
    /// <summary>
    /// 下列程式碼範例會建立號誌的三個最大計數與初始的計數為零。 此範例會啟動五個執行緒，封鎖等候號誌
    /// </summary>
    class Program
    {
        private static Semaphore MySyncEvent = new Semaphore(0, 3);
        static void Main(string[] args)
        {
            for (int i = 1; i <= 5; i++)
            {
                ThreadPool.QueueUserWorkItem(MyMethod, i);
            }

            Console.WriteLine("按下任一按鍵，送出收到訊號狀態");
            Console.ReadKey();
            MySyncEvent.Release(3);
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void MyMethod(object state)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"@ 工作執行緒{state} 準備等候訊號通知 {i}");
                MySyncEvent.WaitOne();
                Console.WriteLine($"@ 工作執行緒{state} 模擬需要執行 {i}");
                Thread.Sleep(1000);
                Console.WriteLine($"   @ 工作執行緒{state} 釋放訊號 {i}");
                MySyncEvent.Release();
            }
        }
    }
}
