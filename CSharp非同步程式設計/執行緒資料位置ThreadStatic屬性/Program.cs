using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 執行緒資料位置ThreadStatic屬性
{
    // 在這個範例中，使用了 ThreadStaticAttribute 屬性，用來指示每個執行緒的靜態欄位值是唯一的
    class Program
    {
        class ThreadData
        {
            /// <summary>
            /// 指示每個執行緒的靜態欄位值是唯一的
            /// </summary>
            [ThreadStatic]
            static int 執行緒的靜態欄位值是唯一的;

            public static void 執行緒委派方法()
            {
                // 儲存該執行緒的 Managed執行緒代號
                // 隨然 [執行緒的靜態欄位值是唯一的] 變數是靜態變數，不過，在每個執行緒內，都具有唯一性
                執行緒的靜態欄位值是唯一的 = Thread.CurrentThread.ManagedThreadId;

                // 為了要允許其他執行緒也可以執行同樣的委派方法，所以，本身執行緒將睡眠1秒
                Thread.Sleep(1000);

                // 顯示該靜態變數的值，雖然此時該變數已經被許多執行緒設定過，
                // 但該靜態變數在每個執行緒內都具有唯一性，所以，不會產生錯亂
                Console.WriteLine("現在執行緒的ID {0}:   儲存在靜態變數內的ID:{1}",
                    Thread.CurrentThread.ManagedThreadId, 執行緒的靜態欄位值是唯一的);
            }
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread newThread = new Thread(ThreadData.執行緒委派方法);
                newThread.Start();
            }
            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }
}
