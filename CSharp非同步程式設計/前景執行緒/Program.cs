using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 前景執行緒
{
    // 啟動 Thread，並且指定皆為 前景執行緒
    // 在 Main 方法中，只是啟動該執行緒，之後 主執行緒 就會結束了，因此，App會等候所有前景執行緒執行完畢後，才會結束執行
    class Program
    {
        public static void 執行緒方法()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("前景執行緒方法: {0}", i);
                Thread.Sleep(1000);
            }
        }

        public static void Main()
        {
            Thread t1 = new Thread(new ThreadStart(執行緒方法));
            // 指定該執行緒為前景執行緒
            //t1.IsBackground = false;
            // 啟動該執行緒
            t1.Start();

            // 由於 t1 執行緒為前景執行緒
            // 在底下主執行緒程式碼執行完畢後，也必須等後前景執行緒執行完畢，
            // 才算整個結束執行
            Console.ReadKey();
            Console.WriteLine("主執行緒要結束執行了");
        }
    }
}
