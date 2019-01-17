using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 射後不理async_void
{
    class Program
    {
        static void Main(string[] args)
        {
            OnDelegateAsync();

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();

            OnDelegateWithExceptionAsync();

            // 為何底下這行會發生錯誤
            //var fooTask = OnDelegateWithExceptionAsync();

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        static async void OnDelegateAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("OnDelegateAsync 方法結束執行了");
        }
        static async void OnDelegateWithExceptionAsync()
        {
            Console.WriteLine("OnDelegateWithExceptionAsync 開始執行了");
            await Task.Delay(1000);
            throw new Exception("有例外異常發生了");
        }
    }
}
