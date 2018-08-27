using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 強制終止Abort
{
    // 要停止一個執行緒的執行，可以使用Thread.Abort方法
    // 然而，因為 Abort 方法是在另外一個執行緒上執行(我們是從主執行緒呼叫該方法)
    // Thread.Abort方法 於被叫用的所在執行緒中引發 ThreadAbortException，開始處理執行緒的結束作業。 呼叫這個方法通常會結束執行緒。
    public static class Program
    {
        public static void Main()
        {
            #region 我們可以使用 AppDomain.CurrentDomain.UnhandledException 事件來捕捉到任何 .NET 環境中沒有被應用程式捕捉到的例外異常事件
            // 表示應用程式定義域，也就是應用程式執行的獨立環境
            // https://docs.microsoft.com/en-us/dotnet/api/system.appdomain?view=netframework-4.7.2
            AppDomain currentDomain = AppDomain.CurrentDomain;
            // 訂閱發生於未攔截到例外狀況事件，可以捕捉到這裡訊息
            currentDomain.UnhandledException += (s, e) =>
            {
                // ExceptionObject 屬性是 UnhandledExceptionEventArgs 類別中定義，表示 未處理的例外狀況物件
                Exception ex = (Exception)e.ExceptionObject;
                Console.WriteLine("異常訊息 : " + ex.Message);
                // IsTerminating 屬性是 UnhandledExceptionEventArgs 類別中定義，表示 Common Language Runtime 是否已終止
                Console.WriteLine("Common Language Runtime 是否已終止: {0}", e.IsTerminating);
            };
            #endregion

            // 建立一個執行緒
            Thread t = new Thread(new ThreadStart(() =>
            {
                #region 執行緒委派方法內，沒有進行例外異常捕捉
                while (true)
                {
                    Console.WriteLine("執行緒執行中...");
                    Thread.Sleep(500);
                    // 若在執行緒內發生了例外異常，我們可以透過 UnhandledException 捕捉到嗎
                    //throw new Exception("aaa");
                }
                #endregion
                #region 執行緒委派方法內，自己有進行例外異常捕捉
                //try
                //{
                //    while (true)
                //    {
                //        Console.WriteLine("執行緒執行中...");
                //        Thread.Sleep(500);
                //        // 若在執行緒內發生了例外異常，我們可以透過 UnhandledException 捕捉到嗎
                //        //throw new Exception("aaa");
                //    }
                //}
                //catch (ThreadAbortException ex)
                //{
                //    Console.WriteLine("有異常發生:" + ex.Message);
                //    // 因為該執行緒已經強制終止，所以，在此做些清除的工作
                //}
                ////catch (Exception ex)
                ////{
                ////    Console.WriteLine("有異常發生:" + ex.Message);
                ////    // 因為該執行緒已經強制終止，所以，在此做些清除的工作
                ////}
                #endregion
            }));
            t.Start();
            Console.WriteLine("按下任一按鍵，結束該執行緒執行");
            Console.ReadKey();

            // 發出終止請求
            // 於被叫用的所在執行緒中引發 ThreadAbortException，開始處理執行緒的結束作業
            t.Abort();

            Console.WriteLine("真的要結束處理程序?");
            Console.ReadKey();
            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }
}
