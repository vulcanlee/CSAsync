using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 多工之計算非同步
{
    /// <summary>
    /// 這裡使用了 15 個執行緒，非同步的進行計算迴圈數量，最後進行加總累計
    /// 這是一個簡單的非同步運算的範例
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            object lockA = new object();
            long 合計次數 = 0;
            for (int fooThreadIdx = 0; fooThreadIdx < 15; fooThreadIdx++)
            {
                new Thread(() =>
                {
                    long fooTemp = 0;
                    for (long fooI = 0; fooI < 10000000; fooI++)
                    {
                        fooTemp++;
                    }

                    // 關於 lock 陳述式 請參考 https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/lock-statement
                    lock (lockA)
                    {
                        合計次數 += fooTemp;
                    }
                    Console.WriteLine("執行緒{0} 執行完成，合計次數為 {1}", Thread.CurrentThread.ManagedThreadId, 合計次數);
                    return;
                }).Start();
            }


            Console.WriteLine("請按任一鍵，以結束執行");
            Console.ReadKey();
        }
    }
}
