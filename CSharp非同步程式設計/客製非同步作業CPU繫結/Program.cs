using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 客製非同步作業CPU繫結
{
    /// <summary>
    /// 在這個範例，我們使用了 Task<TResult> 來代表外部的非同步作業。 
    /// 在這個範例中，我們建立一個CPU繫結非同步的方法，但是，需要依賴 Thread Pool與資料平行化的方式來進行處理，執行這個非同步方法
    /// </summary>
    class Program
    {
        static CancellationTokenSource cts ;
        static async Task Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem((x) =>
            {
                var foo = Console.ReadKey();
                cts.Cancel();
            });
            await 非同步計算();
            Console.WriteLine("按下任一按鍵，結束該執行緒執行");
            Console.ReadKey();
        }

        static async Task 非同步計算()
        {
            // 建立 CancellationTokenSource
            cts = new CancellationTokenSource();

            List<string> fooData = await 取得所有檔案("C:\\Windows\\System32\\", cts.Token);
            Console.WriteLine("非同步計算工作執行完畢，總共:{0}", fooData.Count);
        }

        // 產生一個工作，使用執行緒池 Thread Pool 內的執行緒
        static Task<List<string>> 取得所有檔案(string 比對路徑, CancellationToken token)
        {
            if (比對路徑 == null || 比對路徑.Length == 0)
            {
                // 若沒有指定要比對的路徑，則建立已成功完成且具有指定之結果的 Task<TResult>
                return Task.FromResult(new List<string>());
            }

            // 在此使用 Task.Run 直接定義出非同步工作方法，並且使用額外執行緒來處理
            return Task.Run(() =>
            {
                var files = new List<string>();
                string dir = 比對路徑;
                object obj = new Object();
                if (Directory.Exists(dir))
                {
                    foreach (var f in Directory.GetFiles(dir))
                    {
                        if (token.IsCancellationRequested)
                            break;

                        var fi = new FileInfo(f);
                        lock (obj)
                        {
                            files.Add(fi.Name);
                        }
                        Console.WriteLine($"{fi.Name}");
                    }
                    return files;
                }
                else
                {
                    return files;
                }
            }, token);
        }
    }
}
