using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 客製非同步作業IO繫結
{
    /// <summary>
    /// 在這個範例，我們使用了 Task<TResult> 來代表外部的非同步作業。 
    /// 在這個範例中，我們建立一個IO繫結非同步的方法，但是，不需要依賴 Thread Pool ，也就是不需要一直持續耗用一個以上 Thread 來執行這個非同步方法
    /// </summary>
    class Program
    {
        static CancellationTokenSource cts;
        static async Task Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem((x) =>
            {
                var foo = Console.ReadKey();
                cts.Cancel();
            });

            // 建立 CancellationTokenSource
            cts = new CancellationTokenSource();

            string fooString = await DownloadStringAsync(new Uri("http://www.microsoft.com"), cts.Token);
            Console.WriteLine("網頁長度: {0}", fooString.Length);

            Console.WriteLine("按下任一按鍵，結束該執行緒執行");
            Console.ReadKey();
        }

        /// <summary>
        /// 客製的非同步方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Task<string> DownloadStringAsync(Uri url, CancellationToken token)
        {
            // 建立與指名工作已經被開始與準備好了
            var tcs = new TaskCompletionSource<string>();
            var wc = new WebClient();
            wc.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error != null) tcs.TrySetException(e.Error); // 發生錯誤的時候，需要把異常設定到工作的 Exception 內
                else if (e.Cancelled) tcs.TrySetCanceled();  // 若收到取消請情，要設定該工作已經被取消了
                else tcs.TrySetResult(e.Result); // 正常完成工作之後，將結果設定到工作內
            };

            token.Register(() => wc.CancelAsync()); // 訂閱 CancellationToken 的取消請求事件，並且要對 WebClient 發出取消請求

            // 開始啟動非同步的抓取網頁
            wc.DownloadStringAsync(url);
            // 將這個非同步工作傳回去
            return tcs.Task;
        }
    }
}
