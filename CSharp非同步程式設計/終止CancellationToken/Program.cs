using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 終止CancellationToken
{
    // 要停止一個執行緒的執行，可以使用 取消許可證來源 CancellationTokenSource 物件，向 CancellationToken 發出訊號，表示應該將它取消
    // CancellationTokenSource : https://docs.microsoft.com/zh-tw/dotnet/api/system.threading.cancellationtokensource?view=netframework-4.7.2
    // CancellationToken  : https://docs.microsoft.com/zh-tw/dotnet/api/system.threading.cancellationtoken?view=netframework-4.7.2
    public static class Program
    {
        public static void Main()
        {
            //產生 CancellationTokenSource 物件
            CancellationTokenSource fooCTS = new CancellationTokenSource();
            Thread t = new Thread(new ThreadStart(() =>
            {
                // 判斷 取消許可證來源 的 [許可證 Token] 是否已經有發出取消請求
                while (fooCTS.Token.IsCancellationRequested == false)
                {
                    Console.WriteLine("執行緒執行中...");
                    Thread.Sleep(1000);
                }
            }));

            t.Start();
            Console.WriteLine("按下任一按鍵，結束該執行緒執行");
            Console.ReadKey();
            // 發出取消請求
            fooCTS.Cancel();
            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }
}
