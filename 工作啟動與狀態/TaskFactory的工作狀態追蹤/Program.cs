using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskFactory的工作狀態追蹤
{
    class Program
    {
        static Task fooTask;
        static void Main(string[] args)
        {
            string lastStatus = "";
            fooTask = Task.Factory.StartNew(() =>
            {
                Task.Delay(1000).Wait();
            });

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

        }
    }
}
