using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 執行緒的集區
{
    // 這個範例展示了如何透過 ThreadPool 來做到多執行緒的多工處理
    class Program
    {
        public static void Main()
        {
            // 從 執行緒的集區 取得一個 執行緒來執行委派工作
            ThreadPool.QueueUserWorkItem(new WaitCallback(執行緒要執行的委派方法));
            // 從 執行緒的集區 取得一個 執行緒來執行委派工作，並且傳遞參數到委派方法內
            ThreadPool.QueueUserWorkItem(new WaitCallback(執行緒要執行的委派方法), "這是從主執行緒傳遞過來的資料");

            // 底下是在主執行緒下來執行
            for (int i = 0; i < 300; i++)
            {
                Console.Write("MT:{0} ", i);
            }

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }

        static void 執行緒要執行的委派方法(Object stateInfo)
        {
            string fooStr = "";
            if (stateInfo != null)
            // 當沒有指定包含這個方法所要使用之資料的物件，則該物件 stateInfo 為 null
            {
                fooStr = stateInfo as string;
                for (int i = 0; i < 300; i++)
                {
                    Console.Write("T1:{0}____[{1}] ", i, fooStr);
                }
            }
            else
            {
                for (int i = 0; i < 300; i++)
                {
                    Console.Write("T2:{0} ", i);
                }
            }
        }
    }
}
