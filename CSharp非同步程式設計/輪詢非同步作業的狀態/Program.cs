using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace 輪詢非同步作業的狀態
{
    // 輪詢非同步作業的狀態，以便得知該非同步作業是否已經完成了
    // 
    // 下列程式碼範例會示範使用 Dns 類別中的非同步方法，以擷取使用者指定之電腦的網域名稱系統資訊。 
    // 這個範例會啟動非同步作業，然後會在作業完成之前在主控台列印句號 (".")。 
    public class PollUntilOperationCompletes
    {
        static void UpdateUserInterface()
        {
            // 我們會使用 . 句號輸出，表示這個應用程式還在執行中，並沒有封鎖起來
            Console.Write(".");
        }

        public static void Main(string[] args)
        {
            string queryFQDN = "www.microsoft.com";

            // 開始請求非同步呼叫，查詢DNS資訊

            // 呼叫 BeginXXX 啟動非同步工作
            IAsyncResult result = Dns.BeginGetHostEntry(queryFQDN, null, null);
            Console.WriteLine("正在取得與處理DNS的資訊...");

            // 開始進行輪詢非同步作業的狀態，看看是否已經完成
            // 我們會使用 . 句號輸出，表示這個應用程式還在執行中，並沒有封鎖起來

            // 在這個回圈內，會不斷地查看非同步工作是否已經完成，透過 IsCompleted 成員
            // 這樣做法雖然不會造成 Thread Block，可以，可以看得出來，這樣做法會耗用大量的 CPU 資源
            // 因為，我們需要不斷的察看非同步工作是否已經完成
            while (result.IsCompleted != true)
            {
                UpdateUserInterface();
            }

            // 若程式已經可以執行到這裡，那就表示非同步工作已經完成了

            Console.WriteLine();
            try
            {
                // 透過 EndXXX 告知非同步工作已經完成，並且取得非同步工作的最後執行結果內容
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

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
