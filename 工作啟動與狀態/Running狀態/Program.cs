using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Running狀態
{
    class Program
    {
        static void Main(string[] args)
        {
            Task fooTask = new Task(MyDelegate);
            fooTask.Start();

            Thread.Sleep(300);

            Console.WriteLine($"Status : {fooTask.Status}");
            Console.WriteLine($"IsCompleted : {fooTask.IsCompleted}");
            Console.WriteLine($"IsCanceled : {fooTask.IsCanceled}");
            Console.WriteLine($"IsFaulted : {fooTask.IsFaulted}");
            var exceptionStatusX = (fooTask.Exception == null) ? "沒有 AggregateException 物件" : "有 AggregateException 物件";
            Console.WriteLine($"Exception : {exceptionStatusX}");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        public static void MyDelegate()
        {
            Thread.Sleep(1000);
        }
    }
}
