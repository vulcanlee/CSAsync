using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 客製化同步內容運作機制
{
    /// <summary>
    /// 客製化同步內容
    /// </summary>
    class MySynchronizationContext : SynchronizationContext
    {
        /// <summary>
        /// 待執行的訊息工作佇列
        /// </summary>
        private readonly Queue<Action> messagesQueue = new Queue<Action>();
        /// <summary>
        /// 用於同步處理之鎖定的物件
        /// </summary>
        private readonly object syncHandle = new object();
        /// <summary>
        /// 是否正在執行中
        /// </summary>
        private bool isRunning = true;

        // 將同步訊息分派至同步處理內容
        public override void Send(SendOrPostCallback codeToRun, object state)
        {
            throw new NotImplementedException();
        }

        // 將非同步訊息分派至同步處理內容
        public override void Post(SendOrPostCallback codeToRun, object state)
        {
            lock (syncHandle)
            {
                // 將要處理的訊息工作，加入佇列中
                messagesQueue.Enqueue(() => codeToRun(state));
                SignalContinue();
            }
        }

        /// <summary>
        /// 進入訊息處理的無窮迴圈，等候其他執行緒傳入的委派執行方法
        /// </summary>
        public void RunMessagePump()
        {
            while (CanContinue())
            {
                Console.Write(".");
                RetriveItem()?.Invoke();

                // 上面那行程式碼是精簡的寫法，與底下三行敘述做到同樣的結果
                //Action nextToRun = RetriveItem();
                //if (nextToRun != null)
                //    nextToRun();

            }
        }

        /// <summary>
        /// 取出待處理的訊息工作項目
        /// </summary>
        /// <returns></returns>
        private Action RetriveItem()
        {
            lock (syncHandle)
            {
                while (CanContinue() && messagesQueue.Count == 0)
                {
                    Monitor.Wait(syncHandle);
                }
                if (isRunning == true)
                {
                    return messagesQueue.Dequeue();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 是否可以繼續執行
        /// </summary>
        /// <returns></returns>
        private bool CanContinue()
        {
            lock (syncHandle)
            {
                return isRunning;
            }
        }

        /// <summary>
        /// 停止 訊息處理的無窮迴圈 執行
        /// </summary>
        public void Cancel()
        {
            lock (syncHandle)
            {
                isRunning = false;
                SignalContinue();
            }
        }

        /// <summary>
        /// 解除鎖定，可以繼續執行
        /// </summary>
        private void SignalContinue()
        {
            Monitor.Pulse(syncHandle);
        }
    }

    class Program
    {
        static MySynchronizationContext ctx = new MySynchronizationContext();
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Main Thread No {0}", Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine($"設定自製 SynchronizationContext 之前的 SynchronizationContext.Current : {SynchronizationContext.Current?.ToString()}");

            // 建立與設定我們自己設計的 同步處理的內容 synchronisation context
            SynchronizationContext.SetSynchronizationContext(ctx);

            Console.WriteLine($"設定自製 SynchronizationContext 之後的 SynchronizationContext.Current : {SynchronizationContext.Current?.ToString()}");

            // 建立與啟動執行一個新的執行緒，在這個執行緒中，會透過同步內容物件，請求一個非同步執行委派方法
            Thread workerThread = new Thread(new ThreadStart(Run));
            workerThread.Start();

            // 建立與設定另外一個執行緒，接收來自使用者的鍵盤輸入指令，看看是否有結束程式執行或者重新下載網頁資料
            Thread againThread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    var foo = Console.ReadKey();
                    if (foo.KeyChar == 'a')
                    {
                        Run();
                    }
                    else if (foo.KeyChar == 'q')
                    {
                        ctx.Cancel();
                        break;
                    };
                }
            }));
            againThread.Start();

            // 主執行緒開始進行 訊息處理的無窮迴圈
            ctx.RunMessagePump();
        }

        // 這個方法將會由新建立的執行緒下來執行(不是在主執行緒)
        private static void Run()
        {
            Console.Out.WriteLine("Current New Thread No {0}", Thread.CurrentThread.ManagedThreadId);

            // 將要處理的委派方法項目，送入 訊息處理的無窮迴圈 執行
            ctx.Post(RunDownloadBlogger, null);
        }

        // 該方法將會要求在同步內容的執行緒來執行，而不是現在正在執行的執行緒下
        private static void RunDownloadBlogger(object state)
        {
            DownloadBlogger();
        }

        // 進行非同步讀取網頁資料
        static async void DownloadBlogger()
        {
            Console.Out.WriteLine("MainProgram 下載前 on Thread No {0}", Thread.CurrentThread.ManagedThreadId);
            var client = new System.Net.WebClient();
            var webContentHomePage = await client.DownloadStringTaskAsync("https://mylabtw.blogspot.com/");
            Console.Out.WriteLine("下載 {0} 字元", webContentHomePage.Length);
            Console.Out.WriteLine("MainProgram 下載完後 on Thread No {0} ", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
