using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 工作等候結束
{
    /// <summary>
    /// 這個範例展示了 您可建立當前項藉由呼叫 Task.ContinueWith 方法完成時執行的接續
    /// 另外，也展示了如何使用 await 關鍵字來等候與繼續工作
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            #region 使用 TPL
            // 建立一個工作，該工作會回傳 42
            Task<int> 新的工作TPL = Task.Run(() =>
            {
                return 42;
            }).ContinueWith((完成的工作) =>
            {
                // 當工作完成後，繼續執行相關運算，並且將運作值回傳，作為最終工作的回傳值
                return 完成的工作.Result * 2;
            });
            新的工作TPL.Wait();
            Console.WriteLine(新的工作TPL.Result);
            #endregion

            使用await();
            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }

        private static async Task 使用await()
        {
            #region 使用 await
            // 建立一個工作，該工作會回傳 42
            Task<int> 新的工作await = Task.Run(() =>
            {
                return 42;
            });
            int foo最後結果 = await 新的工作await;
            foo最後結果 = foo最後結果 * 2;
            Console.WriteLine(foo最後結果);
            #endregion

        }
    }
}
