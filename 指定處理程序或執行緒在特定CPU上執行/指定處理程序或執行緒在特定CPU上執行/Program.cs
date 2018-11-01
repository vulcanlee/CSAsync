using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 指定處理程序或執行緒在特定CPU上執行
{
    /// <summary>
    /// 這個範例程式將會使用 Process.GetCurrentProcess().ProcessorAffinity 來限制處理程序中所有執行緒可用的 CPU 核心數量，
    /// 我們將透過工作管理員的 CPU 使用率來觀察，限制使用不同數量的 CPU 核心，其多執行緒執行效能
    /// 當process由第一個processor遷徙至第二個processor時，必須將資料填入第二個processor的cache memory，
    /// 並且將清除第一個processor的cache memory，導致效能產生多餘的耗損
    /// </summary>
    class Program
    {
        // counter 是一個共用資訊 不同執行緒的共用變數
        private static long counter = 0;
        public static void Main()
        {
            Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)2;

            // 建立第一個執行緒，其會執行 非同步方法 方法
            Thread thread1 = new Thread(MyMethod);

            // 建立第二個執行緒，其會執行 非同步方法 方法
            Thread thread2 = new Thread(MyMethod);

            // 開啟啟動執行這兩個執行緒
            thread1.Start();
            thread2.Start();

            //foreach (ProcessThread thread in Process.GetCurrentProcess().Threads)
            //{
            //    //設定1個處理器，第1顆核心上處理
            //    thread.IdealProcessor = 0;
            //    thread.ProcessorAffinity = (IntPtr)2;
            //}

            Stopwatch sw = new Stopwatch();
            sw.Start();

            // 等候這兩個執行緒結束執行，這個時候，主執行緒是在 封鎖 狀態下，也就是無法繼續執行任何程式碼
            thread1.Join();
            thread2.Join();

            sw.Stop();

            Console.WriteLine("已經處理完成...");
            Console.WriteLine("兩個執行緒聯合計算結果是:");
            Console.WriteLine($"Sum is {counter}");
            Console.WriteLine($"Total is {sw.Elapsed.TotalSeconds} Sec");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void MyMethod()
        {
            for (int index = 0; index < int.MaxValue; index++)
            {
                Interlocked.Increment(ref counter);
            }
        }
    }
}
