using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 工作取消例外異常
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var fooTask = Task.Run(() =>
            {
                //await Task.Delay(100);
                for (int i = 0; i < int.MaxValue; i++)
                {
                    token.ThrowIfCancellationRequested();
                    // 請試著解除註解底下程式碼，並把上述程式碼解除註解，看看有何結果產生
                    //if (token.IsCancellationRequested == true)
                    //{
                    //    throw new Exception("發生了例外異常");
                    //}
                }
            }, token).ContinueWith(x =>
            {
                Console.WriteLine($"Status : {x.Status}");
                Console.WriteLine($"IsCompleted : {x.IsCompleted}");
                Console.WriteLine($"IsCanceled : {x.IsCanceled}");
                Console.WriteLine($"IsFaulted : {x.IsFaulted}");
                var exceptionStatusX = (x.Exception == null) ? "沒有 AggregateException 物件" : "有 AggregateException 物件";
                Console.WriteLine($"Exception : {exceptionStatusX}");
            });

            var barTask = Task.Run(async () =>
            {
                Console.WriteLine("取消工作開始倒數 1 秒鐘");
                await Task.Delay(1000);
                Console.WriteLine("送出取消工作的訊號");
                cts.Cancel();
            });

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
