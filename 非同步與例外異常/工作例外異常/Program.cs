using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 工作例外異常
{
    // 當要執行這個範例程式碼，請不要使用 Visual Studio 2017 來進行執行，
    // 因為，這樣會有可能得到 fooTask 物件的 Status 狀態值為 WaitingForActivation
    // 所以，請在命令提示字元視窗下來執行
    class Program
    {
        static void Main(string[] args)
        {
            var fooTask = Task.Run( () =>
            {
                throw new Exception("發生了例外異常");
            });

            Thread.Sleep(800);
            Console.WriteLine($"Status : {fooTask.Status}");
            Console.WriteLine($"IsCompleted : {fooTask.IsCompleted}");
            Console.WriteLine($"IsCanceled : {fooTask.IsCanceled}");
            Console.WriteLine($"IsFaulted : {fooTask.IsFaulted}");
            var exceptionStatusX = (fooTask.Exception == null) ? "沒有 AggregateException 物件" : "有 AggregateException 物件";
            Console.WriteLine($"Exception : {exceptionStatusX}");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
