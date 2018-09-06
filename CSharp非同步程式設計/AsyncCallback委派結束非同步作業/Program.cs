using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCallback委派結束非同步作業
{
    // 使用 AsyncCallback 委派結束非同步作業
    //
    // 下列程式碼範例會示範使用 Dns 類別中的非同步方法，以擷取使用者指定之電腦的網域名稱系統 (DNS) 資訊。 
    // 此範例會建立參考 ProcessDnsInformation 方法的 AsyncCallback 委派。
    // 這個方法會對 DNS 資訊的每個非同步要求都呼叫一次。
    class Program
    {
        public class UseDelegateForAsyncCallback
        {
            static int requestCounter;
            static ArrayList hostData = new ArrayList();
            static StringCollection hostNames = new StringCollection();
            static void UpdateUserInterface()
            {
                // 我們會輸出底下訊息，表示這個應用程式還在執行中，並沒有封鎖起來
                Console.WriteLine("尚有 {0} 個請求", requestCounter);
            }
            public static void Main()
            {
                // 針對非同步請求，產生委派方法，用於處理非同步工作執行完成後的結果
                AsyncCallback callBack = new AsyncCallback(ProcessDnsInformation);

                string host;
                // 在這個迴圈內，我們可以輸入多個 URL，程式會立即啟動非同步工作取得該 URL 的內容
                do
                {
                    Console.Write(" 請輸入主機名稱，或直接按下 <enter> 按鈕表示完成所有輸入作業: ");
                    host = Console.ReadLine();
                    if (host.Length > 0)
                    {
                        // 使用安全執行緒技巧，增加執行緒要使用的共用變數 [requestCounter] 的值，確保不會因為競賽，導致該變數的內容走味

                        //＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
                        // 遞增特定變數並將結果儲存起來，成為不可部分完成的作業。
                        // 在進行多執行緒存取分享資源的時候，尤其要特別注意
                        //＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊

                        Interlocked.Increment(ref requestCounter);
                        Console.WriteLine("正在取得與處理DNS的資訊...");

                        // 呼叫 BeginXXX 啟動非同步工作
                        Dns.BeginGetHostEntry(host, callBack, host);
                    }
                } while (host.Length > 0);

                //＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
                // 每次非同步作業完成後， requestCounter的就會減少一，由此判斷所有的非同步作業是否都已經完成
                // 這樣的迴圈，也會造成系統資訊濫用
                //＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊

                // 該迴圈的用意，僅是當所有非同步工作都處理完成之後，才會繼續執行該迴圈後的程式碼，
                // 也就是列出所有非同步執行結果的內容
                // 即
                // 若有任何一個非同步工作尚未完成的話，程式會一直在該迴圈內跑
                while (requestCounter > 0)
                {
                    UpdateUserInterface();
                }

                // 若程式已經可以執行到這裡，那就表示所有非同步工作已經完成了
                for (int i = 0; i < hostNames.Count; i++)
                {
                    object data = hostData[i];
                    string message = data as string;
                    // 一個 SocketException 已經產生了
                    if (message != null)
                    {
                        Console.WriteLine("{0} 請求的傳回訊息: {1}",
                            hostNames[i], message);
                        continue;
                    }

                    IPHostEntry h = (IPHostEntry)data;
                    string[] aliases = h.Aliases;
                    IPAddress[] addresses = h.AddressList;
                    if (aliases.Length > 0)
                    {
                        Console.WriteLine("別名 {0}", hostNames[i]);
                        for (int j = 0; j < aliases.Length; j++)
                        {
                            Console.WriteLine("{0}", aliases[j]);
                        }
                    }
                    if (addresses.Length > 0)
                    {
                        Console.WriteLine("位址 {0}", hostNames[i]);
                        for (int k = 0; k < addresses.Length; k++)
                        {
                            Console.WriteLine("{0}", addresses[k].ToString());
                        }
                    }
                }

                Console.WriteLine("Press any key for continuing...");
                Console.ReadKey();
            }

            // 這個方法委派給非同步工作，當非同步工作完成之後，就會來執行這個委派方法
            static void ProcessDnsInformation(IAsyncResult result)
            {
                // 當這段程式碼開始被執行到的時候，就表示某個非同步工作已經完成了
                string hostName = (string)result.AsyncState;
                hostNames.Add(hostName);
                try
                {
                    // 我們需要透過呼叫 EndXXX 來取得非同步處理工作的完成結果內容
                    IPHostEntry host = Dns.EndGetHostEntry(result);
                    hostData.Add(host);
                }
                // 儲存異常訊息.
                catch (SocketException e)
                {
                    hostData.Add(e.Message);
                }
                finally
                {
                    // 減少執行緒共用變數的值，但為了確保執行緒安全性，所以，在此，使用了 Interlocked進行共用變數存取時候的鎖定功能
                    Interlocked.Decrement(ref requestCounter);
                }
            }
        }
    }
}
