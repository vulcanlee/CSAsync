using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace 執行緒集區的使用與歸還
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task> tasks = new List<Task>();
            ShowThreadPoolInformation($"系統準備開始執行");

            for (int i = 1; i <= 50; i++)
            {
                int j = i;
                int k = i;
                tasks.Add(Task.Run(() =>
                {
                    DateTime beginTime = DateTime.Now;
                    ShowThreadPoolInformation($"*** 工作{j}(執行緒{Thread.CurrentThread.ManagedThreadId})正在執行中");
                    Thread.Sleep(30000);
                    DateTime completeTime = DateTime.Now;
                    TimeSpan timeSpan = completeTime - beginTime;
                    ShowThreadPoolInformation($"---->{timeSpan.ToString("c")}<<- 工作{j} 即將結束執行(執行緒{Thread.CurrentThread.ManagedThreadId})");
                }));
                //tasks.Add(Task.Run(async () =>
                //{
                //    DateTime beginTime = DateTime.Now;
                //    ShowThreadPoolInformation($"*** 工作{j}(執行緒{Thread.CurrentThread.ManagedThreadId})正在執行中");
                //    await Task.Delay(10000);
                //    DateTime completeTime = DateTime.Now;
                //    TimeSpan timeSpan = completeTime - beginTime;
                //    ShowThreadPoolInformation($"---->{timeSpan.ToString("c")}<<- 工作{j} 即將結束執行(執行緒{Thread.CurrentThread.ManagedThreadId})");
                //}));
                Thread.Sleep(50);
                ShowThreadPoolInformation($"已經建立新工作{k}");
            }
            Task.WhenAll(tasks).Wait();
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        static void ShowThreadPoolInformation(string message)
        {
            int workerThreads;
            int completionPortThreads;
            Console.WriteLine($"{message}");
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"   可用背景工作執行緒的數目 : {workerThreads} / 可用非同步 I/O 執行緒的數目 : { completionPortThreads}");
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"   執行緒集區中的背景工作執行緒最大數目 : {workerThreads} / 執行緒集區中的非同步 I/O 執行緒最大數目 : { completionPortThreads}");
            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"   需要建立的背景工作執行緒最小數目 : {workerThreads} / 需要建立的非同步 I/O 執行緒最小數目 : { completionPortThreads}");
            Console.WriteLine($"");
        }
    }
}
