using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APM非同步程式設計模型
{
    //關於 [APM非同步程式設計模型] 簡單說明
    //使用 IAsyncResult 設計模式的非同步作業會被實作成兩個方法，
    //名稱為 BeginOperationName 及 EndOperationName ，
    //分別負責開始和結束非同步作業 OperationName。 
    //
    //例如，FileStream 類別會提供 BeginRead 和 EndRead 方法，以非同步方式從檔案讀取位元組。 這些方法會實作 Read 方法的非同步版本。
    //
    //在呼叫 BeginOperationName 之後，應用程式可以繼續對呼叫執行緒執行指令，而非同步作業會在不同的執行緒上執行。 
    //對於每個 BeginOperationName 呼叫，應用程式也應該呼叫 EndOperationName 以取得作業的結果。
    // https://msdn.microsoft.com/zh-tw/library/ms228963(v=vs.110).aspx

    /// <summary>
    /// 這是 APM Asynchronous Programming Model 的使用範例
    /// 在這裡使用了 HttpRequest 來進行讀取遠端網頁(www.microsoft.com)上的內容
    /// APM 的特色就是 使用 BeginXXX 方法啟動非同步的呼叫，完成後，使用 EndXXX結束非同步呼叫並且取得結果內容
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"呼叫非同步方法前的執行緒 ID {Thread.CurrentThread.ManagedThreadId}");
            string queryFQDN = "http://www.microsoft.com/";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(queryFQDN);
            // 這裡呼叫了 BeginGetResponse (BeginXXX ) 開始對網際網路資源的非同步要求
            // 呼叫的時候，也指定了 System.AsyncCallback 委派，要來指派當非同步工作完成的後，需要呼叫的方法
            // 回傳值為 IAsyncResult 類型，參考回應的非同步要求
            IAsyncResult ar = request.BeginGetResponse(new AsyncCallback(ResponseCallback), request);

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        /// <summary>
        /// HttpWebRequest的非同步處理完成後的事件 (回呼 callback)
        /// </summary>
        /// <param name="ar"></param>
        static void ResponseCallback(IAsyncResult ar)
        {
            Console.WriteLine($"已經完成非同步方法執行的 回呼 callback 的執行緒 ID {Thread.CurrentThread.ManagedThreadId}");
            // 在此回呼 callback 方法中，使用參數 IAsyncResult ar ，取得 HttpWebRequest 物件
            HttpWebRequest request = ar.AsyncState as HttpWebRequest;
            HttpWebResponse response = request.EndGetResponse(ar) as HttpWebResponse;

            Encoding enc = System.Text.Encoding.UTF8;
            StreamReader loResponseStream = new
              StreamReader(response.GetResponseStream(), enc);

            string Response = loResponseStream.ReadToEnd();
            Console.WriteLine(Response);

            loResponseStream.Close();
            response.Close();
        }
    }
 }
