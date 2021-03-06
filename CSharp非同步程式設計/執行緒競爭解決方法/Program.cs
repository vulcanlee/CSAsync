﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 執行緒競爭解決方法
{
    // 在多執行緒程式內，共用資源可以同時被多個執行緒存取，這樣會發生了執行緒的競賽條件
    // 當程式設計師沒有妥善的控制執行緒存取共用變數的執行順序，，這將導致不可預知結果，也就是共用變數的值會計算不正確
    // 由於程式師並沒有在其中共用的變數評估的工作執行緒的順序控制，這將導致不可預知的共用變數中的值的類型。
    //
    // 這個範例，將由兩個執行緒執行同樣的方法，計算迴圈共執行了幾次
    // 其中，counter共用變數，則是負責計算累計迴圈執行的次數
    // 當要存取 counter共用變數時候，需要透過lock方式來解決
    class Program
    {
        // counter 是一個共用資訊 不同執行緒的共用變數
        private static int counter = 0;
        private static object 共用鎖 = new object();
        public static void Main()
        {
            // 建立第一個執行緒，其會執行 DoWork1 方法
            Thread thread1 = new Thread(DoWork1);

            // 建立第二個執行緒，其會執行 DoWork1 方法
            Thread thread2 = new Thread(DoWork1);

            // 開啟啟動執行這兩個執行緒
            thread1.Start();
            thread2.Start();

            // 等候這兩個執行緒結束執行
            thread1.Join();
            thread2.Join();

            Console.WriteLine("已經處理完成...");
            Console.WriteLine("兩個執行緒聯合計算結果是:");
            Console.WriteLine(counter);
            Console.WriteLine("請按任一鍵，以結束執行");
            Console.ReadKey();
        }

        private static void DoWork1()
        {
            for (int index = 0; index < 10000000; index++)
            {
                // 使用 共用鎖 來避免不同執行緒可以同時執行底下程式碼
                lock (共用鎖)
                {
                    counter++;
                }
            }
        }
    }
}
