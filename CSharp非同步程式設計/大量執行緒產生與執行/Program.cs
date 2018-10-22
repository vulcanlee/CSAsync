using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 大量執行緒產生與執行
{
    // 在這個範例中，我們將會產生2000個執行緒，
    // 接著我們會觀察整體系統的 CPU使用率、Process數量、執行緒數量、可用記憶體數量、環境交換次數
    // 嘗試了解，當執行緒過多的時候，對於系統有何影響?

    // 請執行這個專案，並且為了要說明處理程序建立了大量的執行緒對系統有何影響，請先開啟效能監視器，觀察這些計數器
    // * 選擇物件為[System] > 計數器為[Context Switch / sec]
    //   是在電腦上所有處理器從一個執行緒切換到另一個執行緒的合併速率
    // * 物件為[System] > 計數器為[Processes]
    //   是在取樣時間中電腦上的處理程序數目
    // * 物件為[System] > 計數器為[Threads]
    //   是在取樣時間中電腦上的執行緒數目
    // * 物件為[Memory] > 計數器為[Available MBtyes]
    //   是指電腦上的可用實體記憶體的數量(以 MB 計算)，可立即配置給處理程序或供系統使用
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("按下任一按鍵，開始產生執行緒，並且執行");
            Console.ReadKey();

            for (int threadIdx = 0; threadIdx < 2000; threadIdx++)
            {
                // 產生一個新的 Thread並且立即啟動
                new Thread(() =>
                {
                    var threadId = Thread.CurrentThread.ManagedThreadId;
                    for (int i = 0; i < int.MaxValue; i++)
                    {
                        if((i%1000) ==0)
                        Console.WriteLine($"執行緒方法{threadId}: {i}");
                    }
                })
                { IsBackground = true }.Start();
            }

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }
}
