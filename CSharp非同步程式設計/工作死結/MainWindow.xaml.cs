using System;
using System.Collections.Generic;
using System.Linq;
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

namespace 工作死結
{
    /// <summary>
    /// 底下範例展示了各種工作呼叫方式與執行緒的同步處理內容(SynchronizationContext)的應用
    /// 不同的呼叫會產生不同的結果，甚至可能會產生 App 凍結或者死結
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        #region 按鈕事件
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Wait 是一種同步處理方法會造成呼叫的執行緒等候，直到目前的工作已完成。 如果目前的工作尚未啟動執行，等候方法嘗試移除工作排程器從並執行它內嵌在目前的執行緒上 
            使用工作但無回傳值Async().Wait();
            Console.WriteLine("Continue Button_Click");
            //never gets past this point
            btnDeadlock.Content = "Running";
        }

        private async void Button1_Click(object sender, RoutedEventArgs e)
        {
            await 使用工作但無回傳值_使用了接續封送處理回原始擷取的內容Async();
            btnDeadlock1.Content = "I'm no deadlock";
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            使用工作但無回傳值_使用了接續封送處理回原始擷取的內容Async().Wait();
            Console.WriteLine("Continue Button2_Click");
            btnDeadlock2.Content = "I'm no deadlock";
        }

        private async void Button3_Click(object sender, RoutedEventArgs e)
        {
            DateTime fooDT1 = DateTime.UtcNow;
            DateTime fooDT2 = DateTime.UtcNow;
            var doSomething = 休息且回到UI執行緒Async();
            Thread.Sleep(1000);
            await doSomething;
            fooDT2 = DateTime.UtcNow;

            btnDeadlock3.Content = "花費時間" + (fooDT2 - fooDT1);
        }

        private async void Button4_Click(object sender, RoutedEventArgs e)
        {
            DateTime fooDT1 = DateTime.UtcNow;
            DateTime fooDT2 = DateTime.UtcNow;
            var doSomething = 休息但不會回到UI執行緒_回到另外一個執行緒Async();
            Thread.Sleep(1000);
            await doSomething;
            fooDT2 = DateTime.UtcNow;

            btnDeadlock4.Content = "花費時間" + (fooDT2 - fooDT1);
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            // 沒有使用 await 等候工作，造成不會等候而繼續執行之後敘述
            var fooTask = 使用工作且有回傳值Async();
            Console.WriteLine("Continue Button5_Click");
            // 這裡會使用封鎖方式來等候非同步執行結果
            Console.WriteLine(fooTask.Result);
        }
        #endregion

        /// <summary>
        /// 這個方法執行了暫停一秒的非同步工作，之後要回到 UI 執行緒
        /// </summary>
        /// <returns></returns>
        private async Task 使用工作但無回傳值Async()
        {
            await Task.Delay(1000);
            // 這行永遠執行不到，因為，...
            Console.WriteLine("完成非同步的工作");
        }

        /// <summary>
        /// 這個方法執行了暫停一秒的非同步工作，之後要回到 UI 執行緒
        /// </summary>
        /// <returns></returns>
        private async Task<string> 使用工作且有回傳值Async()
        {
            await Task.Delay(1000);
            // 這行永遠執行不到，因為，...
            Console.WriteLine("完成非同步的工作");
            return "無";
        }

        /// <summary>
        /// 這個方法執行了暫停一秒的非同步工作，之後使用別的執行緒來繼續執行，而不會回到 UI 執行緒來執行
        /// </summary>
        /// <returns></returns>
        private async Task 使用工作但無回傳值_使用了接續封送處理回原始擷取的內容Async()
        {
            await Task.Delay(1000).ConfigureAwait(false);
            // 為什麼這行可以執行到呢?
            // 解釋：因為上層方法有使用 fooTask.Result 封鎖執行緒，等候這個方法的執行結果
            //       可是，當休息 1 秒鐘之後，卻因為 UI 執行緒被封鎖，無法使用 Post 來請求回到 UI 執行緒來執行
            Console.WriteLine("完成非同步的工作");
        }

        private async Task 休息且回到UI執行緒Async()
        {
            // 使用 Task.Delay ，做到暫停執行 1 秒鐘，但不會造成現在 UI 執行緒被封鎖(Block)
            // 為什麼呢? 因為這裡使用了 await 
            await Task.Delay(1000);

            // 在執行完工作等待 await 之後(暫停1秒鐘)，接著，會回到 UI 執行緒，
            // 此時所執行的執行緒休息(Thread.Sleep)方法，會暫停 UI 執行緒5秒鐘，也就是會造成整個App凍結住了
            Thread.Sleep(5000);
        }

        private async Task 休息但不會回到UI執行緒_回到另外一個執行緒Async()
        {
            // 使用 Task.Delay ，做到暫停執行 1 秒鐘，但不會造成現在 UI 執行緒被封鎖(Block)
            // 為什麼呢? 因為這裡使用了 await 
            await Task.Delay(1).ConfigureAwait(continueOnCapturedContext: false);

            // 在執行完工作等待 await 之後(暫停1秒鐘)，接著，不會回到 UI 執行緒，因為，上面執行了 .ConfigureAwait(continueOnCapturedContext: false)
            // 此時所執行的執行緒休息(Thread.Sleep)方法，會新的執行緒5秒鐘，當然，整個App是不會被凍結住，因為 UI 執行緒可以正常運作
            Thread.Sleep(5000);
        }
    }
}
