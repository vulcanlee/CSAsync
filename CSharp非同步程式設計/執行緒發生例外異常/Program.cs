using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 執行緒發生例外異常
{
    /// <summary>
    /// 在這個範例中，共啟動了4個執行緒，當執行緒內的執行方法發生了例外異常，是會造成整個處理程序意外終止
    /// 其中，在執行緒2內，我們有捕捉例外異常，並且還原內容與釋放資源，因此，執行緒2，不會造成處理程序意外終止與造成處理程序不穩定
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            #region 執行緒2 休息1秒後，會拋出例外異常，但會被 try...catch 捕捉起來
            Thread fooThread2 = new Thread(() =>
            {
                try
                {
                    Console.WriteLine("執行緒2 休息2秒後，會拋出例外異常，但會被 try...catch 捕捉起來");
                    Thread.Sleep(2000);
                    Console.WriteLine("執行緒2發生例外異常...");
                    throw new OverflowException();
                }
                catch (OverflowException)
                {
                    Console.WriteLine("執行緒2發生 當檢查內容中的算數、轉型 (Casting) 或轉換作業發生溢位時所擲回的例外狀況");
                }
                finally
                {
                    // 請在這裡將取得的資源釋放掉，並且還原已經處理過的程序內容
                }
            });
            fooThread2.Name = "執行緒2";
            #endregion

            #region 執行緒3 休息5秒後，會拋出例外異常 FormatException
            Thread fooThread3 = new Thread(() =>
            {
                Console.WriteLine("執行緒3 休息5秒後，會拋出例外異常 FormatException");
                Thread.Sleep(5000);
                Console.WriteLine("執行緒3發生例外異常...");
                throw new FormatException();
            });
            fooThread3.Name = "執行緒3";
            #endregion

            #region 執行緒4 休息8秒後，會拋出自訂例外異常
            Thread fooThread4 = new Thread(() =>
            {
                Console.WriteLine("執行緒4 休息8秒後，會拋出自訂例外異常");
                Thread.Sleep(8000);
                Console.WriteLine("執行緒4發生例外異常...");
                throw new Exception("自訂異常");
            });
            fooThread4.Name = "執行緒4";
            #endregion

            Console.WriteLine("啟動 3 個執行緒...");
            fooThread2.Start();
            fooThread3.Start();
            fooThread4.Start();

            Console.WriteLine("主執行緒休息 10 秒");
            Thread.Sleep(10000);
            Console.WriteLine("主執行緒發生例外異常...");
            throw new NullReferenceException();

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();

        }
    }
}
