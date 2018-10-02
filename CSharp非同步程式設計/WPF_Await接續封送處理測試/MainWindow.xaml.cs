using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Await接續封送處理測試
{
    /// <summary>
    /// 在這個範例中，因為使用 WPF 製作範例程式，屬於  STA（單執行緒套間 Single Threaded Apartment） https://msdn.microsoft.com/en-us/library/system.threading.apartmentstate(v=vs.110).aspx
    /// 其中， UI 執行緒內有定義 執行緒的同步處理內容(SynchronizationContext) 
    /// 我們可以在呼叫非同步方法後，也就是在 await 之後，嘗試將接續封送處理回原始擷取的內容
    /// 若執行 .ConfigureAwait(true) 表示【要】接續封送處理回原始擷取的內容，在此，SynchronizationContext 指的就是 UI 執行緒
    /// 若執行 .ConfigureAwait(false) 表示【不要】接續封送處理回原始擷取的內容
    /// 
    /// 詳情，請參考: https://msdn.microsoft.com/zh-tw/library/hh873173(v=vs.110).aspx
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 這裡使用了 await 關鍵字來呼叫非同步方法，且在非同步方法內，當完成 HttpClient 非同步呼叫後，會回到 UI 執行緒
        /// 請注意 Console 輸出結果視窗內的內容，您會看到當 HttpClinet呼叫完成後，所使用的執行緒ID，是UI執行緒的ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn有await_非同步工作且會回到UI執行緒_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("呼叫 btn有await_非同步工作且會回到UI執行緒_Click 前 : (這是UI執行緒) Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
            await 非同步工作且會回到UI執行緒();
            // 因為【有】使用了 await ，所以，底下這行程式碼，【會】等到上面的非同步呼叫完全處理完畢之後，才會開始執行
            Console.WriteLine("呼叫 btn有await_非同步工作且會回到UI執行緒_Click 後 : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
        }

        /// <summary>
        /// 這裡使用了 await 關鍵字來呼叫非同步方法，且在非同步方法內，當完成 HttpClient 非同步呼叫後，會回到 UI 執行緒
        /// 請注意 Console 輸出結果視窗內的內容，您會看到當 HttpClinet呼叫完成後，所使用的執行緒ID，不是UI執行緒的ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn有await_非同步工作且不會回到UI執行緒_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("呼叫 btn有await_非同步工作且不會回到UI執行緒_Click 前 : (這是UI執行緒) Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
            await 非同步工作且不會回到UI執行緒();
            // 因為【有】使用了 await ，所以，底下這行程式碼，【會】等到上面的非同步呼叫完全處理完畢之後，才會開始執行
            Console.WriteLine("呼叫 btn有await_非同步工作且不會回到UI執行緒_Click 後 : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
        }

        /// <summary>
        /// 這裡【沒有】使用了 await 關鍵字來呼叫非同步方法，且在非同步方法內，當完成 HttpClient 非同步呼叫後，會回到 UI 執行緒
        /// 請注意 Console 輸出結果視窗內的內容，您會看到當 HttpClinet呼叫完成後，所使用的執行緒ID，是UI執行緒的ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn沒有await_非同步工作且會回到UI執行緒_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("呼叫 btn沒有await_非同步工作且會回到UI執行緒_Click 前 : (這是UI執行緒) Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
            非同步工作且會回到UI執行緒();
            // 因為 【沒有】 使用了 await(所以，不會有等候效果) ，所以，底下這行程式碼，【不會】等到上面的非同步呼叫完全處理完畢之後，才會開始執行
            // 而是會立即執行，請觀察Console輸出結果
            Console.WriteLine("呼叫 btn沒有await_非同步工作且會回到UI執行緒_Click 後 : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
        }

        /// <summary>
        /// 這裡【沒有】使用了 await 關鍵字來呼叫非同步方法，且在非同步方法內，當完成 HttpClient 非同步呼叫後，會回到 UI 執行緒
        /// 請注意 Console 輸出結果視窗內的內容，您會看到當 HttpClinet呼叫完成後，所使用的執行緒ID，不是UI執行緒的ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn沒有await_非同步工作且不會回到UI執行緒_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("呼叫 btn沒有await_非同步工作且不會回到UI執行緒_Click 前 : (這是UI執行緒) Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
            非同步工作且不會回到UI執行緒();
            // 因為 【沒有】 使用了 await(所以，不會有等候效果) ，所以，底下這行程式碼，【不會】等到上面的非同步呼叫完全處理完畢之後，才會開始執行
            // 而是會立即執行，請觀察Console輸出結果
            Console.WriteLine("呼叫 btn沒有await_非同步工作且不會回到UI執行緒_Click 後 : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
        }

        /// <summary>
        /// 因為在按下該按鈕之後，呼叫了 非同步工作且會回到UI執行緒()方法後，接著要取得 Result
        /// 也就是把非同步的方法，當作同步方式來呼叫，除了會造成 UI執行緒被凍結之外，也有可能造成死結
        /// 
        /// 這是因為要取得 Result 的值之後，UI執行緒才能夠繼續執行下去，可是，這個時候，Result的值，必須要等到 非同步工作且會回到UI執行緒() 執行完成之後才能夠取得
        /// *** 所以，這個時候，UI執行緒被封鎖了，直到 Result 有值之後，才能夠解除UI執行緒的封鎖狀態
        /// 當 非同步工作且會回到UI執行緒() 執行完成之後，切換到 UI執行緒，卻無法做任何事情，因為，UI執行緒被封鎖了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn把非同步方法_當作同步方式呼叫_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("呼叫 btn把非同步方法_當作同步方式呼叫_Click 前 : (這是UI執行緒) Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
            string result = 非同步工作且會回到UI執行緒().Result;
            Console.WriteLine("呼叫 btn把非同步方法_當作同步方式呼叫_Click 後 : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
        }

        /// <summary>
        /// 因為在按下該按鈕之後，呼叫了 非同步工作且會回到UI執行緒()方法後，接著要取得 Result
        /// 也就是把非同步的方法，當作同步方式來呼叫，除了會造成 UI執行緒被凍結之外，也有可能造成死結
        /// 
        /// 這是因為要取得 Result 的值之後，UI執行緒才能夠繼續執行下去，可是，這個時候，Result的值，必須要等到 非同步工作且不會回到UI執行緒() 執行完成之後才能夠取得
        /// *** 所以，這個時候，UI執行緒被封鎖了，直到 Result 有值之後，才能夠解除UI執行緒的封鎖狀態
        /// 當 非同步工作且不會回到UI執行緒() 執行完成之後，並不會切換到 UI執行緒，而是使用執行緒池內的任一執行緒來執行，
        /// 所以，可以順利將 Result 回傳回去，所以，UI執行緒也就會解除封鎖了，程式也就不會被凍結了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn把非同步方法_當作同步方式呼叫_不會凍結解法_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("呼叫 btn把非同步方法_當作同步方式呼叫_不會凍結解法_Click 前 : (這是UI執行緒) Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
            string result = 非同步工作且不會回到UI執行緒().Result;
            Console.WriteLine("呼叫 btn把非同步方法_當作同步方式呼叫_不會凍結解法_Click 後 : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
        }

        /// <summary>
        /// 這裡雖然使用 await 來等候非同步工作，但是，之後有個無窮迴圈，導致 UI執行緒 無法處理任何事情，造成 App 被凍結了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnUI執行緒忙碌中_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("呼叫 btnUI執行緒忙碌中_Click 前 : (這是UI執行緒) Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
            await 非同步工作且會回到UI執行緒();
            while (true)
            {
                Thread.Sleep(100);
            }
            Console.WriteLine("呼叫 btnUI執行緒忙碌中_Click 後 : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
        }

        #region 非同步工作，且決定是否要呼叫 .ConfigureAwait(false)
        public static async Task<string> 非同步工作且不會回到UI執行緒()
        {
            using (HttpClient client = new HttpClient())
            {
                Console.WriteLine("    在 DownloadContent 內，呼叫 Await 前 (ConfigureAwait(false) : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
                string 非同步工作結果2 = await client.GetStringAsync("http://www.microsoft.com").ConfigureAwait(false);
                Console.WriteLine("    在 DownloadContent 內，呼叫 Await 後 (ConfigureAwait(false) : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId, 非同步工作結果2);
            }
            return "";
        }

        public static async Task<string> 非同步工作且會回到UI執行緒()
        {
            using (HttpClient client = new HttpClient())
            {
                Console.WriteLine("    在 DownloadContent 內，呼叫 Await 前  : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId);
                // 若沒有指定，則預設為呼叫 ConfigureAwait(true)
                string 非同步工作結果1 = await client.GetStringAsync("http://www.microsoft.com");
                Console.WriteLine("    在 DownloadContent 內，呼叫 Await 後  : Thread ID :{0}", Thread.CurrentThread.ManagedThreadId, 非同步工作結果1);
            }

            return "";
        }

        #endregion
    }
}
