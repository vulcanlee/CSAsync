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

namespace WPF執行緒同步處理內容
{
    /// <summary>
    /// 這個範例，展示了 執行緒的同步處理內容 的應用方法
    /// 這個共有兩個功能展示區塊，左半部的開始、取消與右半部的開始與取消，左半部有使用[執行緒的同步處理內容]功能，右半部則沒有使用[執行緒的同步處理內容]功能
    /// 當我們綁定取消事件( cts.Token.Register )，若指定使用了 [執行緒的同步處理內容] 則當該綁定取消事件被呼叫的時候，就會使用UI執行緒來執行這個事件方法
    /// 當我們綁定取消事件( cts.Token.Register )，若指定不會使用了 [執行緒的同步處理內容] 則當該綁定取消事件被呼叫的時候，就不會使用UI執行緒來執行這個事件方法
    ///     在這種情況下，因為綁定取消事件不在UI執行緒上執行，就會造成App掛掉
    /// </summary>
    public partial class MainWindow : Window
    {
        public CancellationTokenSource cts;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region UI 按鈕事件
        private void btn開始_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            // 因為是在 WPF 下以 STA 模式來執行，所以，底下兩種方法都可以使用
            cts.Token.Register(取消Callback1, true);
            //cts.Token.Register(取消Callback2, true);

            if (SynchronizationContext.Current == null)
            {
                Console.WriteLine("SynchronizationContext 不存在");
            }

            Console.WriteLine("MainWindow 的 Thread ID : {0}", Thread.CurrentThread.ManagedThreadId);

            #region 關閉不需要的控制項
            btn開始.IsEnabled = false;
            btn取消.IsEnabled = true;
            btn開始_不同步執行緒的同步處理內容.IsEnabled = false;
            btn取消_不同步執行緒的同步處理內容.IsEnabled = false;
            #endregion

            // 從執行緒區域儲存區產生一個執行緒，並且開始該執行緒方法
            ThreadPool.QueueUserWorkItem(new WaitCallback(執行緒委派方法), cts.Token);
        }


        private void btn開始_不同步執行緒的同步處理內容_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            cts.Token.Register(取消Callback2, false);

            if (SynchronizationContext.Current == null)
            {
                Console.WriteLine("SynchronizationContext 不存在");
            }

            Console.WriteLine("MainWindow 的 Thread ID : {0}", Thread.CurrentThread.ManagedThreadId);

            #region 關閉不需要的控制項
            btn開始.IsEnabled = false;
            btn取消.IsEnabled = false;
            btn開始_不同步執行緒的同步處理內容.IsEnabled = false;
            btn取消_不同步執行緒的同步處理內容.IsEnabled = true;
            #endregion

            // 從執行緒區域儲存區產生一個執行緒，並且開始該執行緒方法
            ThreadPool.QueueUserWorkItem(new WaitCallback(執行緒委派方法), cts.Token);
        }

        private void btn取消_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem((s) =>
            {
                // 在此發出取消請求，不過，此時，呼叫 Cancel() 方法不是在 UI 執行緒，
                // 因此，若一開始綁定取消事件沒有指定要使用 [執行緒的同步處理內容] 設定，
                // 則，執行取消事件將不會在 UI執行緒 上執行
                cts.Cancel();
                Console.WriteLine("\r\n已經發出取消命令...Thread ID : {0}", Thread.CurrentThread.ManagedThreadId);
            });
        }
        #endregion

        void 執行緒委派方法(object state)
        {
            CancellationToken fooCancellationToken = (CancellationToken)state;

            Console.WriteLine("\r\n執行緒委派方法 內的 Thread ID : {0}", Thread.CurrentThread.ManagedThreadId);
            for (int i = 0; i < 100000; i++)
            {
                Console.Write(".");

                if (i == 10)
                {
                    Console.WriteLine("\r\n已經自動發出取消命令...");
                    cts.Cancel();
                }

                if (fooCancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("已經接收到取消請求...");
                    break;
                }
                Thread.Sleep(500);
            }
        }

        #region 發出取消請求
        /// <summary>
        /// 適用於 CancellationToken.Register 方法 (Action, Boolean)
        /// </summary>
        private void 取消Callback2()
        {
            Console.WriteLine("\r\n取消 [取消Callback2] 內的 Thread ID : {0}", Thread.CurrentThread.ManagedThreadId);

            #region 關閉不需要的控制項
            btn開始.IsEnabled = true;
            btn取消.IsEnabled = false;
            btn開始_不同步執行緒的同步處理內容.IsEnabled = true;
            btn取消_不同步執行緒的同步處理內容.IsEnabled = false;
            #endregion
        }

        /// <summary>
        /// CancellationToken.Register 方法 (Action<Object>, Object, Boolean)
        /// </summary>
        private void 取消Callback1()
        {
            Console.WriteLine("\r\n取消 [取消Callback1] 內的 Thread ID : {0}", Thread.CurrentThread.ManagedThreadId);

            #region 關閉不需要的控制項
            btn開始.IsEnabled = true;
            btn取消.IsEnabled = false;
            btn開始_不同步執行緒的同步處理內容.IsEnabled = true;
            btn取消_不同步執行緒的同步處理內容.IsEnabled = false;
            #endregion
        }
        #endregion
    }
}
