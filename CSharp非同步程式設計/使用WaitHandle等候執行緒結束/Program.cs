using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 使用WaitHandle等候執行緒結束
{
    /// <summary>
    /// 在這個範例中，在主執行緒使用 WaitHandle 類別的靜態 WaitAny 和 WaitAll 方法等候工作完成時，兩個執行緒要執行背景工作的方式。
    /// 這個類別一般做為同步物件 (Synchronization Object) 的基底類別 (Base Class) 使用。 衍生自 WaitHandle 的類別會定義一項信號機制，
    /// 表示取得或者釋出共用資源的存取權，但在等候存取共用資源的期間，則會使用繼承的 WaitHandle 方法來加以封鎖。
    /// </summary>
    class Program
    {
        // 定義陣列，儲存了兩個 two AutoResetEvent WaitHandles.
        // WaitHandle : 將等候共用資源獨佔存取權限的特定作業系統物件封裝起來
        // AutoResetEvent : 通知等待中的執行緒事件已發生。
        static WaitHandle[] 事件等候控制代碼 = new WaitHandle[]
        {
            new AutoResetEvent(false),
            new AutoResetEvent(false)
        };

        // 定義隨機亂數用於測試之用
        static Random 隨機亂數 = new Random();

        static void Main()
        {
            // 使用 ThreadPool.QueueUserWorkItem 產生兩個執行緒，並且等候所有的執行緒都結束
            DateTime dt = DateTime.Now;
            Console.WriteLine("主執行緒正在等候這兩個執行緒都已經執行完畢.");
            ThreadPool.QueueUserWorkItem(new WaitCallback(執行緒委派方法), 事件等候控制代碼[0]);
            ThreadPool.QueueUserWorkItem(new WaitCallback(執行緒委派方法), 事件等候控制代碼[1]);
            WaitHandle.WaitAll(事件等候控制代碼);
            Console.WriteLine("兩個工作都完成 (等候時間={0}毫秒)", (DateTime.Now - dt).TotalMilliseconds);

            // 使用 ThreadPool.QueueUserWorkItem 產生兩個執行緒，並且等候任意一個執行緒結束
            dt = DateTime.Now;
            Console.WriteLine();
            Console.WriteLine("主執行緒正在等候任何一個執行緒執行完畢.");
            ThreadPool.QueueUserWorkItem(new WaitCallback(執行緒委派方法), 事件等候控制代碼[0]);
            ThreadPool.QueueUserWorkItem(new WaitCallback(執行緒委派方法), 事件等候控制代碼[1]);
            int index = WaitHandle.WaitAny(事件等候控制代碼);
            Console.WriteLine("第工作 {0} 首先完成 (等候時間={1}毫秒)", index + 1, (DateTime.Now - dt).TotalMilliseconds);

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }

        static void 執行緒委派方法(Object state)
        {
            // 向等候的執行緒通知發生事件
            AutoResetEvent are = (AutoResetEvent)state;
            int time = 1000 * 隨機亂數.Next(2, 10);
            Console.WriteLine("執行這些工作需要 {0} 毫秒.", time);
            Thread.Sleep(time);
            // 設定作業已經成功
            are.Set();
        }
    }
}
