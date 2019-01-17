using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 射後不理TaskCompletionSource
{
    class Program
    {
        static async Task Main(string[] args)
        {
            OnDelegateAsync();

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();

            // 為何這行不會造成應用程式崩潰
            OnDelegateWithExceptionAsync();

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        public static Task OnDelegateAsync()
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("OnDelegateAsync 方法結束執行了");
                tcs.SetResult(null);
            });
            return tcs.Task;
        }
        public static Task OnDelegateWithExceptionAsync()
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            Task.Run(() =>
            {
                // 這樣寫法不好，因該用 try...catch 捕捉例外異常，把捕到的例外異常呼叫 tcs.SetException()

                Console.WriteLine("OnDelegateWithExceptionAsync 開始執行了");
                Thread.Sleep(1000);
                throw new Exception("有例外異常發生了");
            });
            return tcs.Task;
        }
    }
}
