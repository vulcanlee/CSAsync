﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 使用await與TaskCompletionSource
{
    // 請試著說出程式碼執行順序，也就是 Checkpoint 與 執行緒 ID 的變化
    class Program
    {
        static async Task Main(string[] args)
        {
            WriteThreadId(1);
            await LogAsync("Program started");

            WriteThreadId(6);
            Console.WriteLine($"模擬主程式正在忙碌中，約3秒鐘");
            //模擬主程式正在忙碌中
            Thread.Sleep(3000);
            WriteThreadId(7);

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static Task LogAsync(string @event)
        {
            WriteThreadId(2);
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            Task.Run(() =>
            {
                WriteThreadId(3);
                Console.WriteLine($"模擬寫到資料庫內，約1秒鐘");
                Thread.Sleep(1000); // 模擬寫到資料庫內
                WriteThreadId(4);

                Console.WriteLine($"模擬寫到檔案內，約1秒鐘");
                Thread.Sleep(1000); // 模擬寫到檔案內

                tcs.SetResult(null);
            });
            WriteThreadId(5);
            return tcs.Task;
        }

        private static void WriteThreadId(int checkpoint)
        {
            Console.WriteLine("Checkpoint: {0}, thread: {1}", checkpoint, Thread.CurrentThread.ManagedThreadId);
        }
    }
}
