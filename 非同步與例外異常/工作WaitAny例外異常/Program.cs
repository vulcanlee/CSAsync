using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 工作WaitAny例外異常
{
    class Program
    {
        static List<int> 工作IDs = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        static void Main(string[] args)
        {
            Task[] allDelegateTasks = (from 工作ID in 工作IDs select 非同步工作委派方法Async(工作ID)).ToArray();
            var fooIndex = Task.WaitAny(allDelegateTasks);
            var fooTask = allDelegateTasks[fooIndex];

            Console.WriteLine($"Status : {fooTask.Status}");
            Console.WriteLine($"IsCompleted : {fooTask.IsCompleted}");
            Console.WriteLine($"IsCanceled : {fooTask.IsCanceled}");
            Console.WriteLine($"IsFaulted : {fooTask.IsFaulted}");
            var exceptionStatusX = (fooTask.Exception == null) ? "沒有 AggregateException 物件" : "有 AggregateException 物件";
            Console.WriteLine($"Exception : {exceptionStatusX}");

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }

        static async Task 非同步工作委派方法Async(int id)
        {
            await Task.Delay(500 + id * 100);
            Console.WriteLine($"執行工作 {id}");
            if (id % 9 == 1)
            {
                Console.WriteLine($"工作{id} 將要產生例外異常");
                throw new Exception(string.Format("發生異常了，工作ID是{0}", id));
            }
        }
    }
}
