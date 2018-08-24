using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 使用BackgroundWorker
{
    // 這個範例展示了 BackgroundWork 這個類別，讓您可以在不同執行緒上執行作業，也就是一個背景工作
    // 這個類別簡化了要進行多工非同步的程式設計複雜度，並且提供了多樣性的事件，可讓我們方便處理各項事務，
    // 這樣，讓我們可以不用再面對 Thread，也可以做到多工處理。
    // 這個 BackgroundWork 已經過時，不建議使用，您可以考慮使用 Task 來替代
    //
    // 請注意底下範例中，主執行緒與各式處理事件所用到的執行緒ID，為什麼會有這樣的情況發生呢?
    class Program
    {
        static void Main(string[] args)
        {
            #region 背景工作的定義
            BackgroundWorker fooBackgroundWorker = new BackgroundWorker();

            #region 定義背景工作開始執行的後，要做哪些事情
            fooBackgroundWorker.DoWork += (s, e) =>
            {
                // 這裡是 BackgroundWorker 的事件，發生於當 RunWorkerAsync 呼叫
                BackgroundWorker worker = s as BackgroundWorker;
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("BackgroundWorker 執行中 (現在執行緒ID {0})...", Thread.CurrentThread.ManagedThreadId);
                    // 回報背景工作的處理進度
                    worker.ReportProgress(i * 10);
                    Thread.Sleep(500);
                }
            };
            #endregion

            #region 定義背景工作期間，當有進度回報的時候，該做甚麼處理
            fooBackgroundWorker.ProgressChanged += (s, e) =>
            {
                // 這裡是 BackgroundWorker 的事件，發生於當 ReportProgress(System.Int32) 呼叫
                Console.WriteLine("BackgroundWorker 處理進度回報:{1}%  (現在執行緒ID {0})...", Thread.CurrentThread.ManagedThreadId, e.ProgressPercentage);
            };
            #endregion

            #region 定義背景工作結束的時候，要做哪些事情
            fooBackgroundWorker.RunWorkerCompleted += (s, e) =>
            {
                // 這裡是 BackgroundWorker 的事件，發生於背景作業已完成、 已取消，或引發例外狀況
                Console.WriteLine("BackgroundWorker執行完成(現在執行緒ID {0})...", Thread.CurrentThread.ManagedThreadId);
            };
            #endregion
            #endregion

            // 指出 System.ComponentModel.BackgroundWorker 是否可以報告進度更新
            fooBackgroundWorker.WorkerReportsProgress = true;
            fooBackgroundWorker.RunWorkerAsync();
            Console.WriteLine("主執行緒 ID {0},  按下任一按鍵，結束處理程序", Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();
        }
    }
}
