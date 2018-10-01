using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 建立和起始有參數非同步工作
{
    // 這個範例展示了各種非同步工作的建立與啟動方法
    // 這個範例中，有示範如何傳入任何參數到委派方法內
    // 
    // 可以嘗試將 MyTask有參數() 內的 Thread.Sleep(500) 移除，看看執行結果會如何?
    class Program
    {
        static void Main(string[] args)
        {
            // 方法1 : 使用執行緒的集區   .NET 2 開始提供
            // 建立完成後，該非同步工作，就會自動啟動
            ThreadPool.QueueUserWorkItem(new WaitCallback(我的工作有參數), Guid.NewGuid());

            // 方法2 : 使用 Task 類別，在建立物件的時候，傳入非同步的委派方法   .NET 4 開始提供
            // 產生完 Task 物件之後，需要呼叫 Start() 方法，來啟動這個非同步委派方法
            var t1 = new Task(我的工作有參數, Guid.NewGuid());
            t1.Start();
            var t2 = new Task(() => 我的工作有參數(Guid.NewGuid()));
            t2.Start();

            // 方法3 - 使用 Task.Factory 靜態方法產生工作，這些 Factory 方法用於建立及設定 Task 和 Task<TResult> 執行個體。
            //         等同於建立藉由呼叫無參數的類別 TaskFactory.TaskFactory() 建構函式
            // 使用 Task.Factory 靜態方法產生的非同步工作，會立即開始執行
            Task.Factory.StartNew(我的工作有參數, Guid.NewGuid());
            Task.Factory.StartNew(() => 我的工作有參數(Guid.NewGuid()));

            // 方法4 - 將指定在 ThreadPool 執行工作排入佇列，並傳回該工作的工作控制代碼    .NET 4.5 的 Task 類別新增了靜態方法 Run。
            //         Run 方法可讓您建立及執行工作的單一方法呼叫中，簡單的替代方案 StartNew 方法。 
            //         它會建立工作以下列的預設值：
            //               其取消語彙基元是 CancellationToken.None。
            //               其 CreationOptions 屬性值是 TaskCreationOptions.DenyChildAttach。
            //               它會使用預設工作排程器。
            // TaskTask.Run 執行後，會立即開始執行非同的委派工作
            Task.Run(() => 我的工作有參數(Guid.NewGuid()));

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadLine();
        }

        static void 我的工作()
        {
            Console.WriteLine("執行 沒有 傳入參數的工作");
            Console.WriteLine("工作執行緒 #{0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(500);
        }
        static void 我的工作有參數(object state)
        {
            Console.WriteLine($"執行 -- 有 -- 傳入參數的工作，參數:{state.ToString()}");
            Console.WriteLine("工作執行緒 #{0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(500);
        }
    }
}
