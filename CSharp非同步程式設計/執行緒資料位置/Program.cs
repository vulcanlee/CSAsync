using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 執行緒資料位置
{
    // 您可使用 Managed 執行緒區域儲存區 (Thread Local Storage，TLS) 來儲存對執行緒及應用程式定義域來說是唯一的資料。
    // .NET Framework 提供兩種方法來使用 Managed TLS：執行緒相關的靜態欄位和資料位置。
    //
    // 在 .NET Framework 4 中，您可以使用 System.Threading.ThreadLocal<T> 類別來建立第一次取用物件時進行延遲初始化的執行緒區域物件。 
    public static class Program
    {
        // 提供資料的執行緒區域儲存區
        // https://msdn.microsoft.com/zh-tw/library/dd642243(v=vs.110).aspx
        public static ThreadLocal<int> _field =
        new ThreadLocal<int>(() =>
        {
            return Thread.CurrentThread.ManagedThreadId;
        });

        public static void Main()
        {
            new Thread(() =>
            {
                for (int x = 0; x < _field.Value; x++)
                {
                    Console.WriteLine("執行緒 A: {0}", x);
                }
            }).Start();
            new Thread(() =>
            {
                for (int x = 0; x < _field.Value; x++)
                {
                    Console.WriteLine("執行緒 B: {0}", x);
                }
            }).Start();
            Console.ReadKey();
        }
    }
}
