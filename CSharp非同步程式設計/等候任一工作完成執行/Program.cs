using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 等候任一工作完成執行
{
    /// <summary>
    /// 這個範例展示了：等候任一提供的 Task 物件完成執行，而不需等到所有工作都完成才需要繼續接下來的工作
    /// 我們先啟動了3個非同步工作，接下來會等候，直到任一工作執行完成，並且將工作的執行結果輸出到Console上
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Task<int>[] tasks = new Task<int>[3];

            #region 建立與執行三個非同步工作
            tasks[0] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("1");
                return 1;
            });
            tasks[1] = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("2");
                return 2;
            });
            tasks[2] = Task.Run(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("3");
                return 3;
            }
            );
            #endregion

            // 檢查是否還有未完成的工作
            while (tasks.Length > 0)
            {
                // 取得已經完成的工作物件索引值
                int i = Task.WaitAny(tasks); // 此處會造成現在執行緒被鎖定(Block)，直到所有的工作都完成(也許是失敗、取消)
                Task<int> completedTask = tasks[i]; // 取得已經完成的工作物件 
                Console.WriteLine("執行結果:{0}", completedTask.Result);
                var temp = tasks.ToList();
                temp.RemoveAt(i);  // 將已經完成的工作，從清單中移除
                tasks = temp.ToArray();
            }

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }
}
