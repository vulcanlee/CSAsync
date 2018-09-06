using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace 封鎖應用程式執行AsyncWaitHandle
{
    // 使用 AsyncWaitHandle 封鎖應用程式執行
    // https://msdn.microsoft.com/zh-tw/library/ms228962(v=vs.110).aspx
    //
    // 下列程式碼範例會示範在 DNS 類別中使用非同步方法，以擷取使用者所指定之電腦的網域名稱系統資訊。 
    // 且會示範使用與非同步作業關聯的 WaitHandle 來進行封鎖。 
    // 請注意，由於在使用此處理方法時不需要 BeginGetHostByNamerequestCallback 和 stateObject 參數，
    // 對於這兩個參數都會傳遞 null。
    public class WaitUntilOperationCompletes
    {
        public static void Main(string[] args)
        {
            string queryFQDN = "www.microsoft.com";

            // 開始請求非同步呼叫，查詢DNS資訊

            // 呼叫 BeginXXX 啟動非同步工作
            IAsyncResult result = Dns.BeginGetHostEntry(queryFQDN, null, null);
            Console.WriteLine("正在取得與處理DNS的資訊...");
            // 等候，直到整個處理程序完成
            // 當執行下面程式碼時候，整個 Thread 會被 Block，這個Thread要能繼續執行，必須等候到非同步工作完成
            result.AsyncWaitHandle.WaitOne();

            // 當非同步處理程序完成後，就可以執行底下程式碼，我們就開始處理結果.
            try
            {
                // Get the results.
                // 由於非同步工作已經完成了，所以，我們在此呼叫了 EndXXX 方法，取得非同步工作的處理結果
                IPHostEntry host = Dns.EndGetHostEntry(result);
                string[] aliases = host.Aliases;
                IPAddress[] addresses = host.AddressList;
                if (aliases.Length > 0)
                {
                    Console.WriteLine("別名");
                    for (int i = 0; i < aliases.Length; i++)
                    {
                        Console.WriteLine("{0}", aliases[i]);
                    }
                }
                if (addresses.Length > 0)
                {
                    Console.WriteLine("位址");
                    for (int i = 0; i < addresses.Length; i++)
                    {
                        Console.WriteLine("{0}", addresses[i].ToString());
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("當處理請求的時候，異常發生了: {0}", e.Message);
            }
            Console.ReadKey();
        }
    }
}
