using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhenAll工作中發生例外狀況
{
    /// <summary>
    /// 這個範例透過查詢指令，建立起9個非同步工作，之後使用 Task.WhenAll，等候所有工作完成
    /// 不過，因為這群非同步工作中，有些會有例外異常發生，我們可以透過工作狀態來查詢是否有例外異常發生
    /// </summary>
    class Program
    {
        static List<int> 工作IDs = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        static async Task Main(string[] args)
        {
            Task[] asyncOps = (from 工作ID in 工作IDs select 非同步工作委派方法Async(工作ID)).ToArray();
            try
            {
                await Task.WhenAll(asyncOps);
            }
            catch (Exception exc)
            {
                // 當所有等候工作都執行結束後，可以檢查是否有執行失敗的工作
                foreach (Task faulted in asyncOps.Where(t => t.IsFaulted))
                {
                    Console.WriteLine(faulted.Exception.InnerException.Message);
                }
            }

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }

        static async Task 非同步工作委派方法Async(int id)
        {
            await Task.Delay(500+id*10);
            Console.WriteLine($"執行工作 {id}");
            if (id % 3 == 0)
            {
                throw new Exception(string.Format("發生異常了，工作ID是{0}", id));
            }
        }
    }
}
