using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 多執行緒
{
    // 這個範例展示了，如何使用 Thread 類別，建立兩個新的執行緒
    // 加上原先的主執行緒，因此，這個處理程序內，共有三個執行緒同時在執行
    // 執行緒1 執行的時候，會輸出 * 字元
    // 執行緒2 執行的時候，會輸出 - 字元
    // 主執行緒 執行的時候，會輸出 M 字元
    class Program
    {
        // Thread 使用的方法
        public static void 執行緒方法1()
        {
            for (int i = 0; i < 90000; i++)
            {
                if (i % 50 == 0)
                {
                    Console.Write("*");
                }
            }
            Console.WriteLine("\r\n執行緒方法1 已經執行完畢");
        }

        public static void 執行緒方法2(object state)
        {
            for (int i = 0; i < 90000; i++)
            {
                if (i % 50 == 0)
                {
                    Console.Write(state);
                }
            }
            Console.WriteLine("\r\n執行緒方法2 已經執行完畢");
        }
        public static void Main()
        {
            #region 定義與啟動第一個執行緒
            // 表示在 Thread 上執行的方法
            ThreadStart 執行緒委派1 = new ThreadStart(執行緒方法1);
            // 產生一個新的 Thread，當該Thread啟動之後，會執行 [ThreadMethod] 方法
            // 當建立 Managed 執行緒時，在此執行緒上執行的方法會由傳遞給 Thread 建構函式的 
            // ThreadStart 委派或 ParameterizedThreadStart 委派所表示。 此執行緒要等到呼叫 Thread.Start 方法之後，才會開始執行。
            Thread 執行緒1 = new Thread(new ThreadStart(執行緒委派1));
            // 為Thread加入名稱，方便進行除錯
            // 要查看執行中的 Thread 請在功能表中選擇 偵錯 > 視窗 > 執行緒
            執行緒1.Name = "多奇經驗分享執行緒1";
            執行緒1.Start();
            #endregion

            #region 定義與啟動第二個執行緒
            // 這裡使用的 ParameterizedThreadStart 來宣告一個委派方法
            Thread 執行緒2 = new Thread(執行緒方法2);
            執行緒2.Name = "多奇經驗分享執行緒2";
            // 在這裡傳入新執行緒的引數
            執行緒2.Start("-");
            #endregion

            // 當主要 Thread 正在執行， 新產生的兩個Thread 也正在執行，也就是說，三個 Thread同時在執行中

            for (int i = 0; i < 90000; i++)
            {
                if (i % 50 == 0)
                {
                    Console.Write("M");
                }
            }

            Console.WriteLine("請按任一鍵，以結束執行");

            // 當程式停在此中斷點時，查看 Visual Studio 的 [執行緒] 視窗內的內容
            // 功能表 偵錯 > 視窗 > 執行緒
            Console.ReadKey();
        }
    }
}
