using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 執行緒集區的使用與歸還
{
    class Program
    {
        static int ITERATIONS = 1000;
        static CountdownEvent done = new CountdownEvent(ITERATIONS);
        static DateTime startTime = DateTime.Now;
        static TimeSpan totalLatency = TimeSpan.FromSeconds(0);
        static TimeSpan totalCompletedLatency = TimeSpan.FromSeconds(0);
        static int AvailableWorkerThreads = 0;
        static int maxRunningWorkThreads = 0;


        static void Main(string[] args)
        {
            //Lab10使用執行緒集區內的執行緒();
            //Lab11使用Task_Run的工作();
            //Lab20使用執行緒集區內的執行緒並調高執行緒集區最少可用數量();
            //Lab21使用Task_Run的工作並調高執行緒集區最少可用數量();
            //Lab22使用Task_Factory_StartNew的LongRunning工作();
            Lab31使用Task_Run的await工作非同步方法呼叫();

            done.Wait();

            Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}");
            System.Console.WriteLine("平均開始執行延遲 = {0}", TimeSpan.FromMilliseconds(totalLatency.TotalMilliseconds / ITERATIONS));
            System.Console.WriteLine("平均結束執行延遲 = {0}", TimeSpan.FromMilliseconds(totalCompletedLatency.TotalMilliseconds / ITERATIONS));
            ShowThreadPoolInformation();
            Console.WriteLine($"此次測試，最多啟動了 {maxRunningWorkThreads} 個背景執行緒");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        static void Lab10使用執行緒集區內的執行緒()
        {
            ShowThreadPoolInformation();
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            for (int i = 1; i <= ITERATIONS; i++)
            {
                int j = i;
                DateTime beginTime = DateTime.Now;
                ThreadPool.QueueUserWorkItem(x =>
                {
                    OnTaskStart(j, beginTime);
                    Thread.Sleep(500);
                    OnTaskEnd(j, beginTime);
                });
                Console.WriteLine($">> 成功建立背景執行緒 : {j}");
                Thread.Sleep(10);
            }
        }
        static void Lab11使用Task_Run的工作()
        {
            ShowThreadPoolInformation();
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            for (int i = 1; i <= ITERATIONS; i++)
            {
                int j = i;
                DateTime beginTime = DateTime.Now;
                Task.Run(() =>
                {
                    OnTaskStart(j, beginTime);
                    Thread.Sleep(500);
                    OnTaskEnd(j, beginTime);
                });
                Console.WriteLine($">> 成功建立背景工作 : {j}");
                Thread.Sleep(10);
            }
        }
        static void Lab20使用執行緒集區內的執行緒並調高執行緒集區最少可用數量()
        {
            //ThreadPool.SetMaxThreads(50, 50);
            ThreadPool.SetMinThreads(16, 16);
            ShowThreadPoolInformation();
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            for (int i = 1; i <= ITERATIONS; i++)
            {
                int j = i;
                DateTime beginTime = DateTime.Now;
                ThreadPool.QueueUserWorkItem(x =>
                {
                    OnTaskStart(j, beginTime);
                    Thread.Sleep(500);
                    OnTaskEnd(j, beginTime);
                });
                Console.WriteLine($">> 成功建立背景執行緒 : {j}");
                Thread.Sleep(10);
            }
        }
        static void Lab21使用Task_Run的工作並調高執行緒集區最少可用數量()
        {
            //ThreadPool.SetMaxThreads(50, 50);
            ThreadPool.SetMinThreads(16, 16);
            ShowThreadPoolInformation();
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            for (int i = 1; i <= ITERATIONS; i++)
            {
                int j = i;
                DateTime beginTime = DateTime.Now;
                Task.Run(() =>
                {
                    OnTaskStart(j, beginTime);
                    Thread.Sleep(500);
                    OnTaskEnd(j, beginTime);
                });
                Console.WriteLine($">> 成功建立背景執行緒 : {j}");
                Thread.Sleep(10);
            }
        }
        static void Lab22使用Task_Factory_StartNew的LongRunning工作()
        {
            //ThreadPool.SetMaxThreads(50, 50);
            //ThreadPool.SetMinThreads(16, 16);
            ShowThreadPoolInformation();
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            for (int i = 1; i <= ITERATIONS; i++)
            {
                int j = i;
                DateTime beginTime = DateTime.Now;
                Task.Factory.StartNew(() =>
                {
                    OnTaskStart(j, beginTime);
                    Thread.Sleep(500);
                    OnTaskEnd(j, beginTime);
                }, TaskCreationOptions.LongRunning);
                Console.WriteLine($">> 成功建立背景執行緒 : {j}");
                Thread.Sleep(10);
            }
        }
        static void Lab31使用Task_Run的await工作非同步方法呼叫()
        {
            //ThreadPool.SetMaxThreads(50, 50);
            //ThreadPool.SetMinThreads(16, 16);
            ShowThreadPoolInformation();
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            for (int i = 1; i <= ITERATIONS; i++)
            {
                int j = i;
                DateTime beginTime = DateTime.Now;
                Task.Run(async () =>
                {
                    OnTaskStart(j, beginTime);
                    await Task.Delay(500);
                    OnTaskEnd(j, beginTime);
                });
                Console.WriteLine($">> 成功建立背景執行緒 : {j}");
                Thread.Sleep(10);
            }
        }

        static void OnTaskStart(int id, DateTime queueTime)
        {
            var latency = DateTime.Now - queueTime;
            lock (done) totalLatency += latency;
            ThreadExecuteLog(id, queueTime, "開始執行");
            int workerThreads;
            int completionPortThreads;
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            int tmpThreads = AvailableWorkerThreads - workerThreads;
            if (tmpThreads > maxRunningWorkThreads) maxRunningWorkThreads = tmpThreads;
        }

        static void OnTaskEnd(int id, DateTime queueTime)
        {
            var latency = DateTime.Now - queueTime;
            lock (done) totalCompletedLatency += latency;
            ThreadExecuteLog(id, queueTime, "準備結束");
            done.Signal();
        }

        static void ThreadExecuteLog(int id, DateTime queueTime, string action)
        {
            var now = DateTime.Now;
            var timestamp = now - startTime;
            var latency = now - queueTime;
            var msg = string.Format("{0}: {1} {2,3}, latency = {3}",
              timestamp, action, id, latency);
            System.Console.WriteLine(msg);
            int workerThreads;
            int completionPortThreads;
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"    可用背景工作執行緒的數目 : {workerThreads} / 可用非同步 I/O 執行緒的數目 : { completionPortThreads}");
        }

        static void ShowThreadPoolInformation()
        {
            int workerThreads;
            int completionPortThreads;
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            AvailableWorkerThreads = workerThreads;
            Console.WriteLine($"   可用背景工作執行緒的數目 : {workerThreads} / 可用非同步 I/O 執行緒的數目 : { completionPortThreads}");
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"   執行緒集區中的背景工作執行緒最大數目 : {workerThreads} / 執行緒集區中的非同步 I/O 執行緒最大數目 : { completionPortThreads}");
            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"   需要建立的背景工作執行緒最小數目 : {workerThreads} / 需要建立的非同步 I/O 執行緒最小數目 : { completionPortThreads}");
            Console.WriteLine($"");
        }
    }
}
