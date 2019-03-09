using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 封鎖應用程式執行AsyncWaitHandle
{
    // 使用 AsyncWaitHandle 封鎖應用程式執行
    // https://msdn.microsoft.com/zh-tw/library/ms228962(v=vs.110).aspx
    //
    // 下列程式碼範例會示範使用 HttpWebRequest 來非同步方法存取網路服務。 
    // 且會示範使用與非同步作業關聯的 WaitHandle 來進行封鎖。 
    // 請注意，由於在使用此處理方法 (BeginGetResponse) 時不需要 回呼 callback 委派方法與 狀態 state 物件，對於這兩個參數都會傳遞 null。
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // 這個網址服務將會提供將兩個整數相加起來，不過，可以指定要暫停幾秒鐘
                string host = "https://lobworkshop.azurewebsites.net";
                string path = "/api/RemoteSource/Add/15/43/5";
                string url = $"{host}{path}";

                // 使用 WebRequest.Create 工廠方法建立一個 HttpWebrequest 物件
                HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(url);

                // 呼叫 BeginXXX 啟動非同步工作
                Console.WriteLine($"啟動非同步方法 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
                IAsyncResult asyncResult =
                  (IAsyncResult)myHttpWebRequest1.BeginGetResponse(null, null);

                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine($"   處理其他事情 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
                    Thread.Sleep(1000);
                }
                // 等候，直到整個處理程序完成
                // 當執行下面程式碼時候，整個 Thread 會被 Block，這個Thread要能繼續執行，必須等候到非同步工作完成
                Console.WriteLine($"等候非同步方法完成 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
                asyncResult.AsyncWaitHandle.WaitOne();

                // 當非同步處理程序完成後，就可以執行底下程式碼，我們就開始處理結果.
                // 由於非同步工作已經完成了，所以，我們在此呼叫了 EndXXX 方法，取得非同步工作的處理結果
                Console.WriteLine($"取得非同步方法結果 (Thread={Thread.CurrentThread.ManagedThreadId}):{DateTime.Now}");
                WebResponse webResponse = myHttpWebRequest1.EndGetResponse(asyncResult);

                Stream ReceiveStream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(ReceiveStream);
                string result = reader.ReadToEnd();

                Console.WriteLine($"網頁執行結果:{result}");

                Console.WriteLine("Press any key for continuing...");
                Console.ReadKey();
            }
            catch (WebException e)
            {
                Console.WriteLine("\nException raised!");
                Console.WriteLine("\nMessage:{0}", e.Message);
                Console.WriteLine("\nStatus:{0}", e.Status);
                Console.WriteLine("Press any key to continue..........");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException raised!");
                Console.WriteLine("Source :{0} ", e.Source);
                Console.WriteLine("Message :{0} ", e.Message);
                Console.WriteLine("Press any key to continue..........");
                Console.Read();
            }
        }
    }
}
