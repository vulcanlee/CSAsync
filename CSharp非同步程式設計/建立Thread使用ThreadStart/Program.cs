using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 建立Thread使用ThreadStart
{
    // 這個範例說明了，如何使用 ThreadStart 與 ParameterizedThreadStart 來指定一個委派的方法，
    // 並且啟動一個執行緒，使用非同步方法的來執行這個委派方法
    // 這是一個非常典型的使用 Thread 來做到非同步作業的範例
    public static class Program
    {
        #region 執行緒委派要用到的方法
        // 這個 Thread 委派方法，並不需要接收任何參數
        // 不同執行緒若都有執行到 Console.Write，則，當Console正在輸出的時候，其他執行緒必須等候。
        public static void 執行緒方法1()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("執行緒方法1: {0}", i);
                // Thread.Sleep(0) 告知作業系統，這個 Thread 可用片段時間已經處理完成所有工作，可以提早切換到其他Thread了
                // 執行緒會將其剩餘的時間配量讓與準備好要執行的任何同等優先權執行緒
                // https://msdn.microsoft.com/zh-tw/library/d00bd51t(v=vs.110).aspx
                Thread.Sleep(0);
            }
        }

        // 這個 Thread 委派方法，需要接收一個參數，而且，該參數的類型必須要 Object
        // https://msdn.microsoft.com/zh-tw/library/system.threading.parameterizedthreadstart(v=vs.110).aspx
        public static void 執行緒方法2(object fooObject)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("執行緒方法2({1}): {0}", i, fooObject.ToString());
                Thread.Sleep(0);
            }
        }
        #endregion

        public static void Main()
        {
            // 產生一個新的 Thread，當該Thread啟動之後，會執行 [執行緒方法1] 方法
            Thread t = new Thread(new ThreadStart(執行緒方法1));
            // 為Thread加入名稱，方便進行除錯
            // 要查看執行中的 Thread 請在功能表中選擇 偵錯 > 視窗 > 執行緒
            t.Name = "使用ThreadStart建立執行緒";
            t.Start();

            // 產生一個新的 Thread，當該Thread啟動之後，會執行 [執行緒方法2] 方法
            Thread t2 = new Thread(new ParameterizedThreadStart(執行緒方法2));
            // 為Thread加入名稱，方便進行除錯
            // 要查看執行中的 Thread 請在功能表中選擇 偵錯 > 視窗 > 執行緒
            t2.Name = "使用ParameterizedThreadStart建立執行緒";
            // 若要傳遞參數給執行緒委派方法，可以使用 Start 方法，將值傳遞過去
            t2.Start("啟動執行緒的參數");

            // 當 Thread 正在執行的同時，主要 Thread 也正在執行，也就是說，兩個 Thread同時在執行中，
            // 每個 Thread 在執行過程中，只能夠使用到片段的CPU時間，使用完後，就需要切換到別的Thread上，讓其他Thread可以來執行
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("主執行緒: 執行某些工作.");
                // Thread.Sleep(0) 告知作業系統，這個 Thread 可用片段時間已經處理完成所有工作，可以提早切換到其他Thread了
                // 執行緒會將其剩餘的時間配量讓與準備好要執行的任何同等優先權執行緒
                Thread.Sleep(0);
            }
            // 等候該Thread執行完畢，此時，主要執行緒將無法處理任何事情(因為被 阻擋 Block了)，只有等候
            // https://msdn.microsoft.com/zh-tw/library/95hbf2ta(v=vs.110).aspx

           // 請試著將這些程式碼解除註解，看看有何問題
           // ConsoleColor cc = Console.ForegroundColor;
           // Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("主執行緒: 執行完畢，進入等候中.");

            //請試著將這些程式碼解除註解，看看有何問題
            // Console.ForegroundColor = cc;

            t.Join();
            t2.Join();
            Console.ReadKey();
        }
    }
}
