using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCompletionSource的工作狀態追蹤
{
    class Program
    {
        static Task<object> fooTask;
        static async Task Main(string[] args)
        {
            string lastStatus = "";

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            ThreadPool.QueueUserWorkItem((x) =>
            {
                Task.Delay(TimeSpan.FromMilliseconds(3000)).Wait();
                tcs.SetResult(null);
            });

            fooTask = tcs.Task;
            var fooTaskX = Task.Run(() =>
            {
                while (true)
                {
                    var tmpStatus = fooTask.Status;
                    if (tmpStatus.ToString() != lastStatus)
                    {
                        Console.WriteLine($"Status : {fooTask.Status}");
                        Console.WriteLine($"IsCompleted : {fooTask.IsCompleted}");
                        Console.WriteLine($"IsCanceled : {fooTask.IsCanceled}");
                        Console.WriteLine($"IsFaulted : {fooTask.IsFaulted}");
                        var exceptionStatusX = (fooTask.Exception == null) ? "沒有 AggregateException 物件" : "有 AggregateException 物件";
                        Console.WriteLine($"Exception : {exceptionStatusX}");
                        Console.WriteLine();
                        lastStatus = tmpStatus.ToString();
                    }
                }
            });

            Task.Delay(TimeSpan.FromMilliseconds(1000)).Wait();

            var foo = await fooTask;


            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
