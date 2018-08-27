using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 終止使用變數
{
    // 要停止一個執行緒的執行，可以使用一個變數旗標來控制，該執行緒是否需要繼續執行
    public static class Program
    {
        public static void Main()
        {
            // 控制產生執行緒是否要繼續執行
            bool stopped = false;
            Thread t = new Thread(new ThreadStart(() =>
            {
                // 採用輪詢 Polling 方式，在執行緒內隨時檢查是否要結束執行
                while (!stopped)
                {
                    Console.WriteLine("執行緒執行中...");
                    Thread.Sleep(1000);
                }
            }));
            t.Start();

            Console.WriteLine("按下任一按鍵，結束該執行緒執行");
            Console.ReadKey();

            // 發出取消請求
            stopped = true;

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }
}
