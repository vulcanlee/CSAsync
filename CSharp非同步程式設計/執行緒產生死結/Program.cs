using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 執行緒產生死結
{
    // 這個範例展示了執行緒互相打死結的範例
    // 執行緒1 & 執行緒2 互相鎖定，導致無法解開
    class Program
    {
        static void Main(string[] args)
        {
            object lockA = new object();
            object lockB = new object();

            // 建立第一個執行緒，其會執行 DoWork1 方法
            Thread thread1 = new Thread(() =>
            {
                lock (lockA)
                {
                    Console.WriteLine("執行緒1 鎖定了 lockA");
                    Thread.Sleep(2000);
                    lock (lockB)
                    {
                        Console.WriteLine("執行緒1 鎖定了 lockA 接著鎖定了 lockB");
                    }
                }
            });

            // 建立第二個執行緒，其會執行 DoWork1 方法
            Thread thread2 = new Thread(() =>
            {
                lock (lockB)
                {
                    Console.WriteLine("執行緒2 鎖定了 lockB");
                    lock (lockA)
                    {
                        Console.WriteLine("執行緒2 鎖定了 lockB 接著鎖定了 lockA");
                    }
                }
            });

            // 開始執行執行緒的委派方法
            thread1.Start();
            thread2.Start();

            Console.WriteLine("請按任一鍵，以結束執行");
            Console.ReadKey();

            // 底下的程式碼為使用 Task 物件來時做出死結現象
            //var up = Task.Run(() =>
            //{
            //    lock (lockA)
            //    {
            //        Thread.Sleep(1000);
            //        lock (lockB)
            //        {
            //            Console.WriteLine("Locked A and B");
            //        }
            //    }
            //});
            //lock (lockB)
            //{
            //    lock (lockA)
            //    {
            //        Console.WriteLine("Locked B and A");
            //    }
            //}
            //up.Wait();
        }
    }
}
