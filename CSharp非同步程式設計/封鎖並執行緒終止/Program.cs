using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 封鎖並執行緒終止
{
    /// <summary>
    /// 在這個範例中，我們將會看到如何使用Thread(前景) & ThreadPool(背景)兩個類別所產生的執行緒已經正常結束了
    /// 請使用這個方法來確保執行緒已經結束。 如果執行緒不結束，呼叫端會無限期封鎖。 如果呼叫 Join 時執行緒已經終止，此方法就會立即傳回。
    /// </summary>
    class Program
    {
        static void Main()
        {
            // 建立 向等候的執行緒通知發生事件
            // AutoResetEvent 類別代表在釋出單一等候執行緒後，收到信號時會自動重設的本機等候控制代碼事件。
            // 在單一等候執行緒釋出之後，系統自動會將 AutoResetEvent 物件重設為未收到信號。 如果沒有執行緒在等候，事件物件的狀態會維持已收到信號。
            // https://docs.microsoft.com/zh-tw/dotnet/standard/threading/autoresetevent
            AutoResetEvent autoEvent = new AutoResetEvent(false);

            #region 使用 Thread 類別，產生一個新的執行緒 (產生一個前景執行緒 Thread.IsBackground)
            Thread regularThread = new Thread(new ThreadStart(Thread委派方法));
            // 開啟執行該執行緒
            regularThread.Start();
            #endregion

            #region 使用 ThreadPool 來請求一個新的執行緒，但是傳送 AutoResetEvent 物件給該執行緒，且為背景執行緒
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPool委派方法), autoEvent);
            #endregion

            // 等候前景執行緒結束(這個執行緒是由 Thread 類別產生的)
            regularThread.Join();
            Console.WriteLine("我知道了，執行緒1，已經執行完成");

            // 等候背景執行緒結束
            autoEvent.WaitOne();
            Console.WriteLine("我知道了，執行緒2，已經執行完成");

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }

        static void Thread委派方法()
        {
            Console.WriteLine("執行緒1，執行Thread類別產生的執行緒");
            Thread.Sleep(2000);
            Console.WriteLine("執行緒1，完成");
        }

        static void ThreadPool委派方法(object stateInfo)
        {
            Console.WriteLine("執行緒2，執行由ThreadPool類別內取得的執行緒");
            Thread.Sleep(1500);
            Console.WriteLine("執行緒2，完成");

            // 發出訊號，通知該執行緒已經成功完成執行了
            ((AutoResetEvent)stateInfo).Set();
        }
    }
}
