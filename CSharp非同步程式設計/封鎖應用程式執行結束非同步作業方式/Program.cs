using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 封鎖應用程式執行結束非同步作業方式
{
    // 以結束非同步作業的方式封鎖應用程式執行
    // 無法在等候非同步作業的結果時繼續執行其他工作的應用程式必須封鎖，直到作業完成為止。 
    // 請使用下列其中一個選項，在等候非同步作業完成時，封鎖應用程式的主執行緒
    //     呼叫非同步作業的 EndOperationName方法。 
    //     請使用非同步作業的BeginOperationName 方法所傳回之 IAsyncResult 的 AsyncWaitHandle 屬性。 
    // https://msdn.microsoft.com/zh-tw/library/ms228967(v=vs.110).aspx
    // 
    // 下列程式碼範例會示範使用 Dns 類別中的非同步方法，以擷取使用者指定之電腦的網域名稱系統資訊。 
    // 請注意，null  會傳遞給 BeginGetHostByNamerequestCallback 和 stateObject 參數，
    // 因為在使用這個方法時，並不需要這些引數。
    class Program
    {
        public class BlockUntilOperationCompletes
        {
            public static void Main(string[] args)
            {

                string queryFQDN = "www.microsoft.com";

                // 開始請求非同步呼叫，查詢DNS資訊

                // 呼叫 BeginXXX 啟動非同步工作
                IAsyncResult result = Dns.BeginGetHostEntry(queryFQDN, null, null);
                Console.WriteLine("正在取得與處理DNS的資訊...");

                // 因為執行緒沒有被封鎖住，您可以在這裡處理其他工作

                try
                {
                    // 直接呼叫 EndXXX 方法，直接等候非同步的處理最後結果(也許是成功、也許是失敗)
                    // 不過此時的 Thread 是被鎖定的，也就是無法繼續執行其他工作
                    // 一旦，執行緒解除鎖定，也就是非同步處理作業完成了(也許成功，也許失敗)，就可以繼續執行
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
}
