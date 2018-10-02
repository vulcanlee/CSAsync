using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console_Await接續封送處理測試
{
    /// <summary>
    /// 在這個範例中，因為使用 Console 範本製作範例程式，不屬於 STA（單執行緒套間 Single Threaded Apartment） https://msdn.microsoft.com/en-us/library/system.threading.apartmentstate(v=vs.110).aspx
    /// 在主執行緒內沒有定義 執行緒的同步處理內容(SynchronizationContext) 
    /// 我們可以在呼叫非同步方法後，也就是在 await 之後，嘗試將接續封送處理回原始擷取的內容
    /// 若執行 .ConfigureAwait(true) 表示【要】接續封送處理回原始擷取的內容，在此，是使用 ThreadPool 作為SynchronizationContext
    /// 若執行 .ConfigureAwait(false) 表示【不要】接續封送處理回原始擷取的內容
    /// 
    /// 詳情，請參考: https://msdn.microsoft.com/zh-tw/library/hh873173(v=vs.110).aspx
    /// </summary>
    public static class Program
    {
        public static void Main()
        {
            if (SynchronizationContext.Current == null)
            {
                Console.WriteLine("這個 App 的SynchronizationContext(執行緒的同步處理內容)沒有發現");
            }
            Console.WriteLine("在 Main 內，呼叫 DownloadContent 前 : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
            string result = DownloadContent().Result;
            Console.WriteLine("在 Main 內，呼叫 DownloadContent 後 : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();
        }
        public static async Task<string> DownloadContent()
        {
            using (HttpClient client = new HttpClient())
            {
                Console.WriteLine("在 DownloadContent 內，呼叫 Await 前 (ConfigureAwait(true)) : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
                string 非同步工作結果1 = await client.GetStringAsync("http://www.microsoft.com").ConfigureAwait(true);
                Console.WriteLine("在 DownloadContent 內，呼叫 Await 後 (ConfigureAwait(true))  : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId, 非同步工作結果1);
            }

            using (HttpClient client = new HttpClient())
            {
                Console.WriteLine("在 DownloadContent 內，呼叫 Await 前 (ConfigureAwait(false) : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
                string 非同步工作結果2 = await client.GetStringAsync("http://www.microsoft.com").ConfigureAwait(false);
                Console.WriteLine("在 DownloadContent 內，呼叫 Await 後 (ConfigureAwait(false) : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId, 非同步工作結果2);
            }
            return "";
        }
    }
}
