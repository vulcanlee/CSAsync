using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 非獨佔鎖定SemaphoreSlim
{
    /// <summary>
    /// 下列程式碼範例會建立號誌的三個最大計數與初始的計數為零。 此範例會啟動五個執行緒，封鎖等候號誌
    /// </summary>
    class Program
    {
        private static SemaphoreSlim MySyncEvent = new SemaphoreSlim(0, 3);
        private static CancellationTokenSource cts = new CancellationTokenSource();
        static void Main(string[] args)
        {
            for (int i = 1; i <= 5; i++)
            {
                ThreadPool.QueueUserWorkItem(MyMethod, i);
            }

            while (true)
            {
                Console.WriteLine("按下任一按鍵，送出收到訊號狀態， q 按鍵則取消剩下工作");
                var key = Console.ReadKey();
                if (key.KeyChar == 'q')
                {
                    cts.Cancel();
                    break;
                }
                else
                    MySyncEvent.Release(3);
                Console.WriteLine();
            }
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void MyMethod(object state)
        {
            bool cancel = false;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"@ 工作執行緒{state} 準備等候訊號通知 {i}");
                try
                {
                    MySyncEvent.Wait(cts.Token);
                }
                catch(OperationCanceledException oce)
                {
                    Console.WriteLine($"@ 工作執行緒{state} 使用者取消剩下作業 {i}");
                    cancel = true;
                }
                catch(Exception oce)
                {
                    Console.WriteLine($"@ 工作執行緒{state} 發生例外異常 {i}");
                    cancel = true;
                }

                if (cancel == true) return;

                Console.WriteLine($"@ 工作執行緒{state} 模擬需要執行 {i}");
                Thread.Sleep(1000);
                Console.WriteLine($"   @ 工作執行緒{state} 釋放訊號 {i}");
            }
        }
    }
}
