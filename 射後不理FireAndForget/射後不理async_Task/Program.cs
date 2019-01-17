using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 射後不理async_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            OnDelegateAsync();

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();

            var fooTask = OnDelegateWithExceptionAsync();

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();

            Console.WriteLine($"Status : {fooTask.Status}");
            Console.WriteLine($"IsCompleted : {fooTask.IsCompleted}");
            Console.WriteLine($"IsCanceled : {fooTask.IsCanceled}");
            Console.WriteLine($"IsFaulted : {fooTask.IsFaulted}");
            var exceptionStatusX = (fooTask.Exception == null) ? "沒有 AggregateException 物件" : "有 AggregateException 物件";
            Console.WriteLine($"Exception : {exceptionStatusX}");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        static async Task OnDelegateAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("OnDelegateAsync 方法結束執行了");
        }
        static async Task OnDelegateWithExceptionAsync()
        {
            Console.WriteLine("OnDelegateWithExceptionAsync 開始執行了");
            await Task.Delay(1000);
            throw new Exception("有例外異常發生了");
        }
    }
}
