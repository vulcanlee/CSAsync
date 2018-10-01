using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 等候單一工作中發生例外狀況
{
    /// <summary>
    /// 這個專案針對單一非同步工作，若非同步工作內有異常發生的時候，使用 TPL方式與 await 關鍵字，其針對例外異常處理的方式
    /// 使用 task.Wait() 方式來 Block 等候非同步工作完成，此時，若非同步工作內有異常發生，則會丟出 AggregateException
    /// 使用 await task 方式來非同步等候工作完成，若此時非同步 工作內有異常發生，則會丟出當時非同步工作內的異常
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            #region 這裡使用 task.Wait() 方式來 Block 等候非同步工作完成，此時，若非同步工作內有異常發生，則會丟出 AggregateException
            Console.WriteLine("使用 task.Wait() 方式來 Block 等候非同步工作完成");
            var 使用非同步封鎖 = Task.Run(async () =>
            {
                await Task.Delay(1000);
                throw new Exception("我發生了例外異常");
            });

            try
            {
                使用非同步封鎖.Wait();
            }
            // 會攔截 System.AggregateException 例外狀況
            catch (AggregateException ex)
            {
                Console.WriteLine("訊息:{0}", ex.Message);
                foreach (var item in ex.InnerExceptions)
                {
                    Console.WriteLine("訊息:{0}", item.Message);
                }
                Console.WriteLine();
            }
            #endregion

            #region 這裡使用 await Task 方式來非同步等候工作完成，若此時非同步 工作內有異常發生，則會丟出當時非同步工作內的異常
            Console.WriteLine("使用 await Task 方式來非同步等候工作完成");
            var 使用非同步等候 = Task.Run(async () =>
            {
                await Task.Delay(1000);
                throw new ArgumentNullException();
            });

            try
            {
                await 使用非同步等候;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("訊息:{0}", ex.Message);
            }
            #endregion

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }
}
